using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentasOnline.Models
{
    public class Proveedores
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(150)]
        public string Correo { get; set; }

        // Propiedad de navegación: Un proveedor tiene muchos productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}