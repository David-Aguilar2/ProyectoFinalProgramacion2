using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(255)]
        public string Descripcion { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        [MaxLength(255)]
        public string ImagenUrl { get; set; }
        public DateTime FechaPublicacion { get; set; }

        // Propiedad de navegación de colección
        public List<DetallePedido> DetallesPedidos { get; set; } = new();
    }
}
