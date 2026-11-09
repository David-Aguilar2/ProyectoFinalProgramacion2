using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VentasOnline.Models
{
    public class Producto
    {
        //Propiedades de la clase Producto
        public int Id { get; set; }

        [ForeignKey("Categoria")]//Llave foranea de la tabla Categoria
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }

        [Required]//Indica que el campo es obligatorio
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]//Llave foranea de la tabla Proveedores
        public Proveedores Proveedor { get; set; }

        [Required]
        [MaxLength(100)]//Nombre del producto con un máximo de 100 caracteres
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
        public string ImagenUrl { get; set; }//URL de la imagen del producto
        public DateTime FechaPublicacion { get; set; }

        // Propiedad de navegación de colección, que representa la relación uno a muchos con la entidad DetallePedido
        public List<DetallePedido> DetallesPedidos { get; set; } = new();
    }
}
