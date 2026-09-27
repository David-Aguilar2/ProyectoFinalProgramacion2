using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int IdVendedor { get; set; }
        public Vendedor Vendedor { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        [MaxLength(20)]
        public string Estado { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CostoEnvio { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        // Propiedades de navegación de colección
        public List<DetallePedido> DetallesPedidos { get; set; } = new();
        public List<Pago> Pagos { get; set; } = new();
        public List<Envio> Envios { get; set; } = new();
    }
}
