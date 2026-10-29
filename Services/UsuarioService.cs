using System;
using System.Collections.Generic;
using System.Text;
using VentasOnline.Data;
using VentasOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace VentasOnline.Services
{
    public class UsuarioService
    {
        // READ

        //Obtener todas las categorías ordenadas
        public List<Usuario> ObtenerTodas()
        {
            // Un contexto por operación
            using var ctx = new AppDbContext();
            return ctx.Usuarios.AsNoTracking().OrderBy(u => u.Nombre).ToList();
        }

        //Obtener un usuario por su Id
        public Usuario? ObtenerPorId(int id)
        {
            using var ctx = new AppDbContext();
            return ctx.Usuarios.Find(id);
        }

        // CREATE: Insertar un nuevo usuario
        public void Crear(Usuario usuario)
        {
            ValidarUsuario(usuario);

            using var ctx = new AppDbContext();

            // Validar que no exista otro usuario con el mismo correo electrónico
            bool existe = ctx.Usuarios.Any(u => u.Correo.ToLower() == usuario.Correo.ToLower());
            if (existe)
            {
                throw new InvalidOperationException("Ya existe un usuario con ese mismo correo electrónico.");
            }

            ctx.Usuarios.Add(usuario);
            ctx.SaveChanges();
        }

        // UPDATE: Actualizar un usuario existente
        public void Actualizar(Usuario usuario)
        {
            ValidarUsuario(usuario);

            using var ctx = new AppDbContext();

            var usuarioDb = ctx.Usuarios.Find(usuario.Id);
            if (usuarioDb == null)
            {
                throw new InvalidOperationException("El usuario que intenta actualizar no existe.");
            }

            // Validar duplicidad de correo electrónico excluyendo el registro actual
            bool existeCorreo = ctx.Usuarios.Any(u => u.Correo.ToLower() == usuario.Correo.ToLower() && u.Id != usuario.Id);
            if (existeCorreo)
            {
                throw new InvalidOperationException("Ya existe otro usuario con ese mismo correo electrónico.");
            }

            usuarioDb.Nombre = usuario.Nombre;
            usuarioDb.Correo = usuario.Correo;

            ctx.SaveChanges();
        }

        // DELETE: Eliminar un usuario con validación de relación
        public void Eliminar(int id)
        {
            using var ctx = new AppDbContext();

            var usuario = ctx.Usuarios.Find(id);
            if (usuario == null)
            {
                throw new InvalidOperationException("El usuario no existe o ya fue eliminado.");
            }

            bool tienePedidos = ctx.Pedidos.Any(p => p.IdCliente == id);
            if (tienePedidos)
            {
                throw new InvalidOperationException("No se puede eliminar el usuario porque tiene pedidos vinculados.");
            }

            ctx.Usuarios.Remove(usuario);
            ctx.SaveChanges();
        }

        // Reglas de validación para el usuario
        private void ValidarUsuario(Usuario usuario)
        {
            //Validar el nombre del usuario
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                throw new ArgumentException("El nombre del usuario es obligatorio.");
            }

            if (usuario.Nombre.Length > 100)
            {
                throw new ArgumentException("El nombre no debe exceder los 100 caracteres.");
            }

            // Validar el correo electrónico
            if (string.IsNullOrWhiteSpace(usuario.Correo))
            {
                throw new ArgumentException("El correo electrónico es obligatorio.");
            }

            //Validar el teléfono
            if (string.IsNullOrWhiteSpace(usuario.Telefono))
            {
                throw new ArgumentException("El teléfono es obligatorio.");
            }
        }
    }
}
