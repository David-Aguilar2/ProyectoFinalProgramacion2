using System;
using System.Collections.Generic;
using System.Text;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public static class Sesion
    {
        public static Usuario? UsuarioActual { get; private set; }

        public static bool EsAdministrador => UsuarioActual?.Rol == "Administrador";

        public static void Iniciar(Usuario usuario) => UsuarioActual = usuario;

        public static void Cerrar() => UsuarioActual = null;
    }
}
