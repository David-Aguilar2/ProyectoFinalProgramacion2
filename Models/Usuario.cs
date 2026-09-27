using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VentasOnline.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Correo { get; set; }
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
        [Required]
        [MaxLength(15)]
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Propiedades de navegación (Relaciones 1 a 1 opcionales según el rol)
        public Administrador? Administrador { get; set; }
        public Cliente? Cliente { get; set; }
        public Vendedor? Vendedor { get; set; }
    }
}
