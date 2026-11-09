using System;

namespace VentasOnline.Dtos
{
    public class LineaPedido
    {
        // Propiedades de la línea de pedido
        // Estas propiedades representan los detalles de cada producto en un pedido
        public int ProductoId { get; set; }
        public string Producto { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}