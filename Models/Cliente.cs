using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace VentasOnline.Models
{
    public class Cliente
    {
        // Propiedades
        public int Id { get; set; }
        [Required]//Indica que el campo es obligatorio
        [MaxLength(100)]// La longitud máxima del campo es de 100 caracteres
        public string Nombre { get; set; }
        [Required]
        [MaxLength(15)]
        public string Telefono { get; set; }
        [Required]
        [EmailAddress]// Valida que el campo sea un correo electrónico válido
        public string Correo { get; set; }

        // Propiedades de navegación
        // Un cliente puede tener solo una dirección y varios pedidos
        public Direccion? Direccion { get; set; }
        public List<Pedido> Pedidos { get; set; } = new();
    }
}
