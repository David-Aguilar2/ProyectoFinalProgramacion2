using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public class EnvioService
    {
        // Listar todos los envíos con sus relaciones cargadas
        public List<Envio> ListarTodos()
        {
            using var ctx = new AppDbContext();
            return ctx.Envios
                      .AsNoTracking()
                      .Include(e => e.Pedido)
                          .ThenInclude(p => p.Cliente)
                      .Include(e => e.Direccion)
                      .OrderByDescending(e => e.FechaEnvio)
                      .ToList();
        }

        // Obtener un envío por el ID del pedido
        public Envio? ObtenerPorPedido(int idPedido)
        {
            using var ctx = new AppDbContext();
            return ctx.Envios
                      .AsNoTracking()
                      .Include(e => e.Pedido)
                      .Include(e => e.Direccion)
                      .FirstOrDefault(e => e.IdPedido == idPedido);
        }

        // Crear un nuevo registro de envío generando la guía automáticamente (C807 Express por defecto)
        public int CrearEnvio(int idPedido, int idDireccion, string empresaTransporte = "C807 Express")
        {
            if (string.IsNullOrWhiteSpace(empresaTransporte))
                empresaTransporte = "C807 Express";

            using var ctx = new AppDbContext();

            var pedido = ctx.Pedidos.FirstOrDefault(p => p.Id == idPedido)
                ?? throw new InvalidOperationException("El pedido especificado no existe.");

            bool direccionExiste = ctx.Direcciones.Any(d => d.Id == idDireccion);
            if (!direccionExiste)
                throw new InvalidOperationException("La dirección seleccionada no existe.");

            bool yaExisteEnvio = ctx.Envios.Any(e => e.IdPedido == idPedido);
            if (yaExisteEnvio)
                throw new InvalidOperationException("El pedido ya cuenta con un registro de envío.");

            // Generación automática del código de guía (Ej: C820261110094707)
            string empresaLimpia = empresaTransporte.Trim();
            string prefijo = empresaLimpia.Length >= 2
                ? empresaLimpia.Substring(0, 2).ToUpper()
                : "C8";

            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string guiaGenerada = $"{prefijo}{timestamp}";

            var nuevoEnvio = new Envio
            {
                IdPedido = idPedido,
                IdDireccion = idDireccion,
                EmpresaTransporte = empresaLimpia,
                NumeroSeguimiento = guiaGenerada,
                FechaEnvio = DateTime.Now,
                Estado = "Pendiente"
            };

            ctx.Envios.Add(nuevoEnvio);
            ctx.SaveChanges();

            return nuevoEnvio.Id;
        }

        // Actualizar el estado del envío y devolver stock si se cancela
        public void ActualizarEstadoEnvio(int idEnvio, string nuevoEstado, string empresaTransporte = "")
        {
            if (string.IsNullOrWhiteSpace(nuevoEstado))
                throw new InvalidOperationException("El estado del envío no puede estar vacío.");

            using var ctx = new AppDbContext();

            // Incluimos el Pedido y sus Detalles para poder reponer el stock si se cancela
            var envioDb = ctx.Envios
                             .Include(e => e.Pedido)
                                 .ThenInclude(p => p.DetallesPedidos)
                             .FirstOrDefault(e => e.Id == idEnvio)
                ?? throw new InvalidOperationException("El registro de envío no existe.");

            // Regla de seguridad: no modificar entregados o cancelados
            if (envioDb.Estado == "Entregado" || envioDb.Estado == "Cancelado")
                throw new InvalidOperationException($"El envío ya se encuentra en estado '{envioDb.Estado}' y no se puede modificar.");

            // Si se cancela, devolver el stock de los productos del pedido
            if (nuevoEstado.Equals("Cancelado", StringComparison.OrdinalIgnoreCase))
            {
                if (envioDb.Pedido != null)
                {
                    foreach (var detalle in envioDb.Pedido.DetallesPedidos)
                    {
                        var producto = ctx.Productos.Find(detalle.IdProducto);
                        if (producto != null)
                        {
                            producto.Stock += detalle.Cantidad; // Se devuelve el stock
                        }
                    }

                    // Opcional: Actualizamos también el estado del pedido principal a Cancelado
                    envioDb.Pedido.Estado = "Cancelado";
                }
            }
            // Actualizamos el estado del envío y la empresa de transporte si se proporciona
            envioDb.Estado = nuevoEstado;
            if (!string.IsNullOrWhiteSpace(empresaTransporte))
            {
                envioDb.EmpresaTransporte = empresaTransporte.Trim();
            }

            ctx.SaveChanges();
        }
    }
}