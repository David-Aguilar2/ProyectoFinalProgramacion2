using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VentasOnline.Data;
using VentasOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace VentasOnline.Services
{
    public class UsuarioService
    {
        public List<Usuario> ObtenerTodas()
        {
            using var ctx = new AppDbContext();
            return ctx.Usuarios.AsNoTracking().OrderBy(u => u.Nombre).ToList();
        }

        public Usuario? ObtenerPorId(int id)
        {
            using var ctx = new AppDbContext();
            return ctx.Usuarios.Find(id);
        }

        public void Crear(Usuario usuario, string passwordEnTextoPlano)
        {
            ValidarUsuario(usuario);

            if (string.IsNullOrWhiteSpace(passwordEnTextoPlano))
            {
                throw new ArgumentException("La contraseña es obligatoria para un usuario nuevo.");
            }

            using var ctx = new AppDbContext();

            bool existe = ctx.Usuarios.Any(u => u.Correo.ToLower() == usuario.Correo.ToLower());
            if (existe)
            {
                throw new InvalidOperationException("Ya existe un usuario con ese mismo correo electrónico.");
            }

            // Hashear contraseña con BCrypt
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(passwordEnTextoPlano);
            usuario.FechaRegistro = DateTime.Now;

            ctx.Usuarios.Add(usuario);
            ctx.SaveChanges();
        }

        public void Actualizar(Usuario usuario, string? passwordEnTextoPlano = null)
        {
            ValidarUsuario(usuario);

            using var ctx = new AppDbContext();

            var usuarioDb = ctx.Usuarios.Find(usuario.Id);
            if (usuarioDb == null)
            {
                throw new InvalidOperationException("El usuario que intenta actualizar no existe.");
            }

            bool existeCorreo = ctx.Usuarios.Any(u => u.Correo.ToLower() == usuario.Correo.ToLower() && u.Id != usuario.Id);
            if (existeCorreo)
            {
                throw new InvalidOperationException("Ya existe otro usuario con ese mismo correo electrónico.");
            }

            usuarioDb.Nombre = usuario.Nombre;
            usuarioDb.Correo = usuario.Correo;
            usuarioDb.Telefono = usuario.Telefono;
            usuarioDb.Rol = usuario.Rol;

            // Si se ingresó una nueva contraseña, se hashea y se actualiza
            if (!string.IsNullOrWhiteSpace(passwordEnTextoPlano))
            {
                usuarioDb.Password = BCrypt.Net.BCrypt.HashPassword(passwordEnTextoPlano);
            }

            ctx.SaveChanges();
        }

        public void Eliminar(int id)
        {
            using var ctx = new AppDbContext();

            var usuario = ctx.Usuarios.Find(id);
            if (usuario == null)
            {
                throw new InvalidOperationException("El usuario no existe o ya fue eliminado.");
            }

            bool tienePedidos = ctx.Pedidos.Any(p => p.IdUsuario == id);
            if (tienePedidos)
            {
                throw new InvalidOperationException("No se puede eliminar el usuario porque tiene pedidos vinculados.");
            }

            ctx.Usuarios.Remove(usuario);
            ctx.SaveChanges();
        }

        // Reglas de validación para el usuario (sin restricción de prefijo telefónico)
        private void ValidarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es obligatorio.");

            if (usuario.Nombre.Length > 100)
                throw new ArgumentException("El nombre no debe exceder los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new ArgumentException("El correo electrónico es obligatorio.");

            // Solo valida que no esté vacío
            if (string.IsNullOrWhiteSpace(usuario.Telefono))
                throw new ArgumentException("El teléfono es obligatorio.");

            // Validar que el rol sea estrictamente Administrador o Vendedor
            if (string.IsNullOrWhiteSpace(usuario.Rol) ||
                (usuario.Rol != "Administrador" && usuario.Rol != "Vendedor"))
            {
                throw new ArgumentException("Debe seleccionar un rol válido (Administrador o Vendedor).");
            }
        }
    }
}