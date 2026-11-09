using VentasOnline.Models;
using VentasOnline.Services;
using VentasOnline.Models;

namespace VentasOnline.Data
{
    public static class Seguridad
    {
        // Contraseña temporal solo para el entorno de desarrollo
        private const string PasswordTemporal = "Test123!";

        public static void Ejecutar()
        {
            using var db = new AppDbContext();

            // 1) Si la tabla está vacía, crea un administrador para no quedarse sin acceso
            if (!db.Usuarios.Any())
            {
                db.Usuarios.Add(new Usuario
                {
                    Correo = "admin@localhost.com",
                    Rol = "Administrador",
                    // Genera un hash de la contraseña temporal para almacenarla de forma segura
                    Password = AuthService.GenerarHash(PasswordTemporal),
                    Nombre = "Administrador",
                    Telefono = "0000000000",
                    FechaRegistro = DateTime.Now
                });
            }

            // 2) Usuarios que existían antes de la migración (sin contraseña)
            var pendientes = db.Usuarios
                               .Where(u => u.Password == "")
                               .ToList();

            foreach (var usuario in pendientes)
                usuario.Password = AuthService.GenerarHash(PasswordTemporal);

            db.SaveChanges();   // un solo viaje a la base de datos
        }
    }
}
