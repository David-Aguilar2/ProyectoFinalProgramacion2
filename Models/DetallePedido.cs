using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }
    }
}
