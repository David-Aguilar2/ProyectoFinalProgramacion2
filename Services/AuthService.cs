using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public static class AuthService
    {
        // Tiempo del algoritmo: cada +1 duplica el tiempo de cálculo (11 es el estándar)
        private const int FactorTrabajo = 11;

        // Convierte una contraseña legible en un hash irreversible (incluye la sal)
        public static string GenerarHash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password, FactorTrabajo);

        // Devuelve el usuario si las credenciales son válidas; null en caso contrario
        public static Usuario? IniciarSesion(string correo, string password)
        {
            using var db = new AppDbContext();

            var usuario = db.Usuarios
                            .AsNoTracking()          // solo lectura
                            .FirstOrDefault(u => u.Correo == correo);

            if (usuario == null || string.IsNullOrEmpty(usuario.Password))
                return null;

            // Verify extrae la sal del hash guardado, recalcula y compara
            bool valida = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
            return valida ? usuario : null;
        }
    }
}
