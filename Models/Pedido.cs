using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Pedido
    {
        // Propiedades de la entidad Pedido
        public int Id { get; set; }
        [ForeignKey("Cliente")]// Llave foránea hacia la entidad Cliente
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        [ForeignKey("Usuario")]// Llave foránea hacia la entidad Usuario
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        [MaxLength(20)]
        public string Estado { get; set; }// Estado del pedido (por ejemplo: "Pendiente", "Enviado", "Entregado")
        [Required]
        [Column(TypeName = "decimal(10,2)")]// Tipo de dato decimal con precisión de 10 dígitos y 2 decimales
        public decimal CostoEnvio { get; set; } = 8.00m; // Costo del envío, valor por defecto 8.00
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        // Propiedades de navegación de colección
        // Relación uno a muchos con DetallePedido, Pago y Envio
        public List<DetallePedido> DetallesPedidos { get; set; } = new();
        public List<Pago> Pagos { get; set; } = new();
        public List<Envio> Envios { get; set; } = new();
    }
}
