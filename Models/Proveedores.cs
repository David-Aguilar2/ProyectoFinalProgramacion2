using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentasOnline.Models
{
    public class Proveedores
    {
        //Propiedades de la entidad Proveedores
        [Key]//Llave primaria
        public int Id { get; set; }

        [Required]//Indica que el campo es obligatorio
        [MaxLength(100)]//Indica la longitud máxima del campo que es de 100 caracteres
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