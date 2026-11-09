using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Direccion
    {
        //Propiedades de la clase Direccion
        public int Id { get; set; }
        [ForeignKey("Cliente")]// Llave foránea hacia la clase Cliente
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        [Required]// Indica que la propiedad es obligatoria
        [MaxLength(100)]// Indica que la longitud máxima de la propiedad es de 100 caracteres
        public string Departamento { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ubicacion { get; set; }

        // Propiedad de navegación de colección que representa la relación uno a muchos con la clase Envio
        public List<Envio> Envio { get; set; } = new();
    }
}
