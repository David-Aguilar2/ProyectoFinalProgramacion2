using System;
using System.Collections.Generic;
using System.Text;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public static class Sesion
    {
        // Propiedad que almacena el usuario actual de la sesión
        public static Usuario? UsuarioActual { get; private set; }

        // Propiedad que indica si hay un usuario autenticado en la sesión
        public static bool EsAdministrador => UsuarioActual?.Rol == "Administrador";

        // Método para iniciar sesión con un usuario específico
        public static void Iniciar(Usuario usuario) => UsuarioActual = usuario;

        // Método para cerrar la sesión actual
        public static void Cerrar() => UsuarioActual = null;
    }
}
