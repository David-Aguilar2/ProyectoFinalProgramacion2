using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        [MaxLength(100)]
        public string Departamento { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ubicacion { get; set; }

        // Propiedad de navegación de colección
        public List<Envio> Envio { get; set; } = new();
    }
}
