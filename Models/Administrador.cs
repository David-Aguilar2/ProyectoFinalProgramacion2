using System;
using System.Collections.Generic;
using System.Text;

namespace VentasOnline.Models
{
    public class Administrador
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
