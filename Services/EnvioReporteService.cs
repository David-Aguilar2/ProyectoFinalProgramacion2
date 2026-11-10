using System.Linq;
using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Dtos;

namespace VentasOnline.Services
{
    public class EnvioReporteService
    {
        // Consulta y estructura la información completa de un envío, su pedido asociado, 
        // el cliente, la dirección, el usuario que autorizó y la lista de productos para el comprobante PDF.
        public EnvioDetalleReporteDto? ObtenerDetalleEnvioParaReporte(int idEnvio)
        {
            // 1. Inicializamos el contexto de la base de datos de manera segura con 'using'
            using var ctx = new AppDbContext();

            // 2. Consultamos el envío aplicando carga ansiosa (Eager Loading) mediante Include y ThenInclude
            // para traer todas las relaciones necesarias en una sola consulta optimizada.
            var envio = ctx.Envios
                .AsNoTracking() // Desactiva el seguimiento de cambios para mejorar el rendimiento de lectura
                .Include(e => e.Pedido)
                    .ThenInclude(p => p.Cliente) // Incluye los datos del cliente asociado al pedido
                .Include(e => e.Pedido)
                    .ThenInclude(p => p.Usuario) // Incluye el usuario/vendedor que registró el pedido
                .Include(e => e.Pedido)
                    .ThenInclude(p => p.DetallesPedidos)
                        .ThenInclude(dp => dp.Producto) // Incluye los productos y detalles que componen el pedido
                .Include(e => e.Direccion) // Incluye la dirección de envío
                .FirstOrDefault(e => e.Id == idEnvio); // Busca el envío específico por su ID

            // 3. Validación de seguridad: Si no existe el envío o el pedido relacionado, retornamos nulo.
            if (envio == null || envio.Pedido == null) return null;

            // 4. Mapeo de la entidad de base de datos hacia el DTO plano (EnvioDetalleReporteDto)
            return new EnvioDetalleReporteDto
            {
                // Datos generales del envío logístico (C807 Express)
                IdEnvio = envio.Id,
                EmpresaTransporte = envio.EmpresaTransporte,
                NumeroSeguimiento = envio.NumeroSeguimiento ?? "N/A",
                FechaEnvio = envio.FechaEnvio,
                EstadoEnvio = envio.Estado,

                // Datos financieros y generales del pedido
                IdPedido = envio.Pedido.Id,
                FechaPedido = envio.Pedido.Fecha,
                EstadoPedido = envio.Pedido.Estado,
                CostoEnvio = envio.Pedido.CostoEnvio,
                TotalPedido = envio.Pedido.Total,

                // Datos del cliente (con operadores de seguridad por si fuesen nulos)
                ClienteNombre = envio.Pedido.Cliente?.Nombre ?? "Sin Cliente",
                ClienteTelefono = envio.Pedido.Cliente?.Telefono ?? "N/A",
                ClienteCorreo = envio.Pedido.Cliente?.Correo ?? "N/A",
                UsuarioAutoriza = envio.Pedido.Usuario?.Nombre ?? "Sin Asignar",

                // Datos de la dirección de destino
                Departamento = envio.Direccion?.Departamento ?? "N/A",
                UbicacionExacta = envio.Direccion?.Ubicacion ?? "N/A",

                // Proyección de la lista de productos del detalle del pedido hacia la estructura del reporte
                Productos = envio.Pedido.DetallesPedidos.Select(dp => new ItemProductoDto
                {
                    NombreProducto = dp.Producto?.Nombre ?? "Producto desconocido",
                    Cantidad = dp.Cantidad,
                    PrecioUnitario = dp.PrecioUnitario,
                    SubTotal = dp.SubTotal
                }).ToList()
            };
        }
    }
}