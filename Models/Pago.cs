using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        [Required]
        [MaxLength(50)]
        public string Metodo { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }
    }
}
