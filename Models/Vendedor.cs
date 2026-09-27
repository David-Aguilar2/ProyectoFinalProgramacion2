using System;
using System.Collections.Generic;
using System.Text;

namespace VentasOnline.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        // Propiedades de navegación de colección
        public List<Producto> Productos { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();
    }
}
