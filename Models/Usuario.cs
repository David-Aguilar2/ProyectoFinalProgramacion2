using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VentasOnline.Models
{
    public class Usuario
    {
        //Propiedades de la clase Usuario
        public int Id { get; set; }
        [Required]// Indica que el campo es obligatorio
        [MaxLength(100)]// Indica la longitud máxima del campo que es de 100 caracteres
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]// Indica que el campo debe ser un correo electrónico válido
        [MaxLength(100)]
        public string Correo { get; set; }
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
        [Required]
        [MaxLength(15)]
        public string Telefono { get; set; }
        public string Rol { get; set; } = "Vendedor";// El rol por defecto es "Vendedor", pero puede ser cambiado a "Administrador" si es necesario
        public DateTime FechaRegistro { get; set; }

        // Propiedades de navegación
        // Un usuario puede tener múltiples pedidos asociados
        public List<Pedido> Pedidos { get; set; } = new();

    }
}
