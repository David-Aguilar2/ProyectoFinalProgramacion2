using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public class ProveedoresService
    {
        // READ

        // Obtener todos los proveedores ordenados por nombre
        public List<Proveedores> ObtenerTodas()
        {
            // Un contexto por operación
            using var ctx = new AppDbContext();
            return ctx.Proveedores.AsNoTracking().OrderBy(p => p.Nombre).ToList();
        }

        // Obtener un proveedor por su Id
        public Proveedores? ObtenerPorId(int id)
        {
            using var ctx = new AppDbContext();
            return ctx.Proveedores.Find(id);
        }

        // CREATE: Insertar un nuevo proveedor
        public void Crear(Proveedores proveedor)
        {
            ValidarProveedor(proveedor);

            using var ctx = new AppDbContext();

            // Validar que no exista otro proveedor con el mismo nombre o correo
            bool existeNombre = ctx.Proveedores.Any(p => p.Nombre.ToLower() == proveedor.Nombre.ToLower());
            if (existeNombre)
            {
                throw new InvalidOperationException("Ya existe un proveedor con ese mismo nombre.");
            }

            bool existeCorreo = ctx.Proveedores.Any(p => p.Correo.ToLower() == proveedor.Correo.ToLower());
            if (existeCorreo)
            {
                throw new InvalidOperationException("Ya existe un proveedor con ese mismo correo electrónico.");
            }

            ctx.Proveedores.Add(proveedor);
            ctx.SaveChanges();
        }

        // UPDATE: Actualizar un proveedor existente
        public void Actualizar(Proveedores proveedor)
        {
            ValidarProveedor(proveedor);

            using var ctx = new AppDbContext();

            var proveedorDb = ctx.Proveedores.Find(proveedor.Id);
            if (proveedorDb == null)
            {
                throw new InvalidOperationException("El proveedor que intenta actualizar no existe.");
            }

            // Validar duplicidad de nombre y correo excluyendo el registro actual
            bool existeNombre = ctx.Proveedores.Any(p => p.Nombre.ToLower() == proveedor.Nombre.ToLower() && p.Id != proveedor.Id);
            if (existeNombre)
            {
                throw new InvalidOperationException("Ya existe otro proveedor registrado con ese nombre.");
            }

            bool existeCorreo = ctx.Proveedores.Any(p => p.Correo.ToLower() == proveedor.Correo.ToLower() && p.Id != proveedor.Id);
            if (existeCorreo)
            {
                throw new InvalidOperationException("Ya existe otro proveedor registrado con ese correo electrónico.");
            }

            proveedorDb.Nombre = proveedor.Nombre;
            proveedorDb.Telefono = proveedor.Telefono;
            proveedorDb.Correo = proveedor.Correo;

            ctx.SaveChanges();
        }

        // DELETE: Eliminar un proveedor con validación de relación
        public void Eliminar(int id)
        {
            using var ctx = new AppDbContext();

            var proveedor = ctx.Proveedores.Find(id);
            if (proveedor == null)
            {
                throw new InvalidOperationException("El proveedor no existe o ya fue eliminado.");
            }

            bool tieneProductos = ctx.Productos.Any(p => p.IdProveedor == id);
            if (tieneProductos)
            {
                throw new InvalidOperationException("No se puede eliminar el proveedor porque tiene productos vinculados.");
            }

            ctx.Proveedores.Remove(proveedor);
            ctx.SaveChanges();
        }

        // Reglas de validación para el proveedor
        private void ValidarProveedor(Proveedores proveedor)
        {
            // Validar el nombre
            if (string.IsNullOrWhiteSpace(proveedor.Nombre))
            {
                throw new ArgumentException("El nombre del proveedor es obligatorio.");
            }

            if (proveedor.Nombre.Length > 100)
            {
                throw new ArgumentException("El nombre no debe exceder los 100 caracteres.");
            }

            // Validar el correo electrónico
            if (string.IsNullOrWhiteSpace(proveedor.Correo))
            {
                throw new ArgumentException("El correo electrónico es obligatorio.");
            }

            if (proveedor.Correo.Length > 150)
            {
                throw new ArgumentException("El correo no debe exceder los 150 caracteres.");
            }

            // Validar el teléfono (opcional pero con tope de longitud)
            if (!string.IsNullOrEmpty(proveedor.Telefono) && proveedor.Telefono.Length > 20)
            {
                throw new ArgumentException("El teléfono no debe exceder los 20 caracteres.");
            }
        }
    }
}
