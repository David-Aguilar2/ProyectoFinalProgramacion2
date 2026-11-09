using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class DetallePedido
    {
        //Propiedades
        public int Id { get; set; }
        [ForeignKey("Pedido")]//Indica que la propiedad IdPedido es una clave foránea que hace referencia a la entidad Pedido
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        [ForeignKey("Producto")]//Indica que la propiedad IdProducto es una clave foránea que hace referencia a la entidad Producto
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        [Required]//Indica que la propiedad es obligatoria y no puede ser nula
        public int Cantidad { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]//Indica que la propiedad es de tipo decimal con una precisión de 10 dígitos y 2 decimales
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }
    }
}
