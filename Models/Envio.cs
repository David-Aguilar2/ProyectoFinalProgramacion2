using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Envio
    {
        public int Id { get; set; }
        [ForeignKey("Pedido")]
        public int IdPedido { get; set; }
        [Required]
        [MaxLength(100)]
        public string EmpresaTransporte { get; set; }
        [MaxLength(100)]
        public string NumeroSeguimiento { get; set; }
        public Pedido Pedido { get; set; }
        [ForeignKey("Direccion")]
        public int IdDireccion { get; set; }
        public Direccion Direccion { get; set; }
        public DateTime FechaEnvio { get; set; }
        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }
    }
}
