using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Pago
    {
        // Propiedades de la clase Pago
        public int Id { get; set; }
        [ForeignKey("Pedido")]// Llave foránea que hace referencia a la clase Pedido
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        [Required]// Indica que el campo es obligatorio
        [MaxLength(50)]// Indica la longitud máxima del campo que es de 50 caracteres
        public string Metodo { get; set; }//Metodo de pago (tarjeta, transferencia, efectivo)
        [Required]
        [Column(TypeName = "decimal(10,2)")]// Indica que el campo es de tipo decimal con una precisión de 10 dígitos y 2 decimales
        public decimal Monto { get; set; }
        public DateTime? FechaPago { get; set; }
        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }// Estado del pago (pendiente, completado, cancelado)
    }
}
