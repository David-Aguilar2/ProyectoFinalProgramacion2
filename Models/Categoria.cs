using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VentasOnline.Models
{
    public class Categoria
    {
        // Propiedades de la entidad
        public int Id { get; set; }
        [Required]//Indica que la propiedad es requerida y no puede ser nula
        [MaxLength(50)]// Limita la longitud máxima del nombre a 50 caracteres
        public string Nombre { get; set; }
        [MaxLength(300)]// Limita la longitud máxima de la descripción a 300 caracteres
        public string? Descripcion { get; set; }// La propiedad Descripcion es opcional, por lo que se permite nulo (string?).

        // Propiedad de navegación de colección que representa la relación uno a muchos con la entidad Producto
        public List<Producto> Productos { get; set; } = new();
    }
}
