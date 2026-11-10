using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Dtos;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public class PedidoService
    {
        // Listar clientes, usuarios y productos activos
        public List<Cliente> ListarClientes()
        {
            using var ctx = new AppDbContext();
            return ctx.Clientes.AsNoTracking().OrderBy(c => c.Nombre).ToList();
        }

        public List<Usuario> ListarUsuarios()
        {
            using var ctx = new AppDbContext();
            return ctx.Usuarios.AsNoTracking().OrderBy(u => u.Nombre).ToList();
        }

        public List<Producto> ListarProductosActivos()
        {
            using var ctx = new AppDbContext();
            return ctx.Productos.AsNoTracking()
                      .Where(p => p.Stock > 0)
                      .OrderBy(p => p.Nombre)
                      .ToList();
        }

        // Listar los pedidos principales
        public List<Pedido> ListarPedidos()
        {
            using var ctx = new AppDbContext();
            return ctx.Pedidos.AsNoTracking()
                      .Include(p => p.Cliente)
                      .OrderByDescending(p => p.Fecha)
                      .ToList();
        }

        // Obtener un pedido con sus detalles y productos asociados
        public Pedido? ObtenerConDetalle(int id)
        {
            using var ctx = new AppDbContext();
            return ctx.Pedidos.AsNoTracking()
                      .Include(p => p.DetallesPedidos)
                          .ThenInclude(d => d.Producto)
                      .FirstOrDefault(p => p.Id == id);
        }

        // Registrar un nuevo pedido, descontar stock, generar su pago pendiente y crear automáticamente el envío contra entrega con C807 Express.
        public int Registrar(int clienteId, int usuarioId, int direccionId, IEnumerable<LineaPedido> lineas)
        {
            var lista = lineas.ToList();
            if (lista.Count == 0)
                throw new InvalidOperationException("El pedido no tiene productos.");

            if (direccionId <= 0)
                throw new InvalidOperationException("Debe seleccionar una dirección válida para la entrega.");

            using var ctx = new AppDbContext();
            using var transaccion = ctx.Database.BeginTransaction();

            try
            {
                // 1) Crear el pedido y sus detalles, descontando stock
                var pedido = new Pedido
                {
                    Fecha = DateTime.Now,
                    IdCliente = clienteId,
                    IdUsuario = usuarioId,
                    Estado = "Pendiente",
                    Total = lineas.Sum(l => l.Subtotal),
                    CostoEnvio = 8.00m
                };

                pedido.DetallesPedidos.AddRange(CrearDetalles(ctx, lista));
                pedido.Total = pedido.DetallesPedidos.Sum(d => d.SubTotal);

                ctx.Pedidos.Add(pedido);
                ctx.SaveChanges(); // Genera pedido.Id

                // 2) Generar automáticamente el registro de Pago Pendiente
                var pagoService = new PagoService();
                pagoService.RegistrarPagoPendiente(ctx, pedido.Id, pedido.Total);

                // 3) Generar el Envío automático para C807 Express (Prefijo: C8)
                string transportista = "C807 Express";
                string prefijoGuia = "C8";
                // Generar un número de guía único basado en la fecha y hora actual
                string numeroGuia = $"{prefijoGuia}{DateTime.Now:yyyyMMddHHmmss}";

                var envio = new Envio
                {
                    IdPedido = pedido.Id,
                    IdDireccion = direccionId,
                    NumeroSeguimiento = numeroGuia,
                    EmpresaTransporte = transportista,
                    Estado = "Pendiente",
                    FechaEnvio = DateTime.Now
                };

                ctx.Envios.Add(envio);
                ctx.SaveChanges();

                // Confirmar todas las operaciones de forma atómica
                transaccion.Commit();

                return pedido.Id;
            }
            catch (Exception)
            {
                transaccion.Rollback();
                throw;
            }
        }

        // Actualizar un pedido existente (devuelve stock anterior y aplica el nuevo)
        public void Actualizar(int pedidoId, int clienteId, int usuarioId, IEnumerable<LineaPedido> lineas)
        {
            var lista = lineas.ToList();
            if (lista.Count == 0)
                throw new InvalidOperationException("El pedido no tiene productos.");

            using var ctx = new AppDbContext();
            Pedido pedido = ctx.Pedidos.Include(p => p.DetallesPedidos)
                                     .FirstOrDefault(p => p.Id == pedidoId)
                ?? throw new InvalidOperationException("El pedido ya no existe.");

            // 1) Devolver stock anterior al inventario
            foreach (var d in pedido.DetallesPedidos)
            {
                Producto producto = ctx.Productos.Find(d.IdProducto)!;
                if (producto != null) producto.Stock += d.Cantidad;
            }
            ctx.DetallesPedidos.RemoveRange(pedido.DetallesPedidos);

            // 2) Aplicar nuevas líneas y descontar stock actual
            var nuevos = CrearDetalles(ctx, lista);
            foreach (var d in nuevos) d.IdPedido = pedido.Id;
            ctx.DetallesPedidos.AddRange(nuevos);

            pedido.IdCliente = clienteId;
            pedido.IdUsuario = usuarioId;
            pedido.Total = nuevos.Sum(d => d.SubTotal);

            ctx.SaveChanges();
        }

        // Eliminar pedido y reponer inventario
        public void Eliminar(int pedidoId)
        {
            using var ctx = new AppDbContext();
            Pedido pedido = ctx.Pedidos.Include(p => p.DetallesPedidos)
                .FirstOrDefault(p => p.Id == pedidoId)
                ?? throw new InvalidOperationException("El pedido ya no existe.");

            foreach (var d in pedido.DetallesPedidos)
            {
                Producto? producto = ctx.Productos.Find(d.IdProducto);
                if (producto != null) producto.Stock += d.Cantidad;
            }

            ctx.DetallesPedidos.RemoveRange(pedido.DetallesPedidos);
            ctx.Pedidos.Remove(pedido);
            ctx.SaveChanges();
        }

        // Método auxiliar para crear detalles de pedido y actualizar stock
        private static List<DetallePedido> CrearDetalles(AppDbContext ctx, List<LineaPedido> lineas)
        {
            var detalles = new List<DetallePedido>();
            foreach (var linea in lineas)
            {
                Producto producto = ctx.Productos.Find(linea.ProductoId)
                    ?? throw new InvalidOperationException("Producto no encontrado.");

                if (producto.Stock < linea.Cantidad)
                    throw new InvalidOperationException(
                        $"Stock insuficiente de '{producto.Nombre}'. Disponible: {producto.Stock}.");

                producto.Stock -= linea.Cantidad;

                detalles.Add(new DetallePedido
                {
                    IdProducto = producto.Id,
                    Cantidad = linea.Cantidad,
                    PrecioUnitario = producto.Precio,
                    SubTotal = linea.Cantidad * producto.Precio
                });
            }
            return detalles;
        }
    }
}