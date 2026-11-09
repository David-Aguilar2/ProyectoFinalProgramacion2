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

        // Registrar un nuevo pedido, descontar stock y generar su pago pendiente asociado
        public int Registrar(int clienteId, int usuarioId, IEnumerable<LineaPedido> lineas)
        {
            var lista = lineas.ToList();
            if (lista.Count == 0)
                throw new InvalidOperationException("El pedido no tiene productos.");

            // Crear el pedido y sus detalles, descontando stock
            using var ctx = new AppDbContext();
            var pedido = new Pedido
            {
                Fecha = DateTime.Now,
                IdCliente = clienteId,
                IdUsuario = usuarioId,
                Estado = "Pendiente",
                CostoEnvio = 0.00m
            };

            // Se asignan los detalles una sola vez de forma correcta
            pedido.DetallesPedidos.AddRange(CrearDetalles(ctx, lista));
            pedido.Total = pedido.DetallesPedidos.Sum(d => d.SubTotal);

            ctx.Pedidos.Add(pedido);
            ctx.SaveChanges(); // Se guarda el pedido para que genere su ID

            // Generamos automáticamente el pago pendiente usando el contexto actual
            var pagoService = new PagoService();
            pagoService.RegistrarPagoPendiente(ctx, pedido.Id, pedido.Total);
            ctx.SaveChanges(); // Guardamos el registro de pago vinculado

            return pedido.Id;
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

            // Opcional: Si el total del pedido cambia al editarlo, podrías actualizar también el monto del pago pendiente asociado si lo deseas.
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

            // Nota: Si tienes configurada eliminación en cascada en la base de datos (Cascade Delete) 
            // para la relación Pedido-Pago, se borrará solo. Si no, recuerda eliminar el pago asociado aquí antes de borrar el pedido.
            ctx.DetallesPedidos.RemoveRange(pedido.DetallesPedidos);
            ctx.Pedidos.Remove(pedido);
            ctx.SaveChanges();
        }

        // Método auxiliar para crear detalles de pedido y actualizar stock
        private static List<DetallePedido> CrearDetalles(AppDbContext ctx, List<LineaPedido> lineas)
        {
            // Validar stock y crear detalles
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