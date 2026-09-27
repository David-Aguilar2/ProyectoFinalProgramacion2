using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VentasOnline.Models
{
    public class Envio
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        public int IdDireccion { get; set; }
        public Direccion Direccion { get; set; }
        public DateTime FechaEnvio { get; set; }
        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }
    }
}
