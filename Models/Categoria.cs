using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VentasOnline.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(300)]
        public string Descripcion { get; set; }

        // Propiedad de navegación de colección
        public List<Producto> Productos { get; set; } = new();
    }
}
