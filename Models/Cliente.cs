using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace VentasOnline.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(15)]
        public string Telefono { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        // Propiedades de navegación
        public List<Direccion> Direcciones { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();
    }
}
