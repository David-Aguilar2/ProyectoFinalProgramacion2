using System;
using System.Collections.Generic;

namespace VentasOnline.Dtos
{
    public class EnvioDetalleReporteDto
    {
        // Datos del Envío
        public int IdEnvio { get; set; }
        public string EmpresaTransporte { get; set; } = string.Empty;
        public string NumeroSeguimiento { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; }
        public string EstadoEnvio { get; set; } = string.Empty;

        // Datos del Pedido
        public int IdPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string EstadoPedido { get; set; } = string.Empty;
        public decimal CostoEnvio { get; set; }
        public decimal TotalPedido { get; set; }

        // Datos Relacionados
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteTelefono { get; set; } = string.Empty;
        public string ClienteCorreo { get; set; } = string.Empty;
        public string UsuarioAutoriza { get; set; } = string.Empty; // Vendedor o Usuario que registró el pedido

        // Dirección de Entrega
        public string Departamento { get; set; } = string.Empty;
        public string UbicacionExacta { get; set; } = string.Empty;

        // Detalle de productos
        public List<ItemProductoDto> Productos { get; set; } = new();
    }

    public class ItemProductoDto
    {
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}