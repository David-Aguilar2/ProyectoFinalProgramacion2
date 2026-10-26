using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public class CategoriaService
    {
        // READ
        
        //Obtener todas las categorías ordenadas
        public List<Categoria> ObtenerTodas()
        {
            // Un contexto por operación
            using var ctx = new AppDbContext();
            return ctx.Categorias.AsNoTracking().OrderBy(c => c.Nombre).ToList();
        }

        //Obtener una categoría por su Id
        public Categoria? ObtenerPorId(int id)
        {
            using var ctx = new AppDbContext();
            return ctx.Categorias.Find(id);
        }

        // CREATE: Insertar una nueva categoría
        public void Crear(Categoria categoria)
        {
            ValidarCategoria(categoria);

            using var ctx = new AppDbContext();

            // Validar que no exista otra categoría con el mismo nombre
            bool existe = ctx.Categorias.Any(c => c.Nombre.ToLower() == categoria.Nombre.ToLower());
            if (existe)
            {
                throw new InvalidOperationException("Ya existe una categoría con ese mismo nombre.");
            }

            ctx.Categorias.Add(categoria);
            ctx.SaveChanges();
        }

        // UPDATE: Actualizar una categoría existente
        public void Actualizar(Categoria categoria)
        {
            ValidarCategoria(categoria);

            using var ctx = new AppDbContext();

            var categoriaDb = ctx.Categorias.Find(categoria.Id);
            if (categoriaDb == null)
            {
                throw new InvalidOperationException("La categoría que intenta actualizar no existe.");
            }

            // Validar duplicidad de nombre excluyendo el registro actual
            bool existeNombre = ctx.Categorias.Any(c => c.Nombre.ToLower() == categoria.Nombre.ToLower() && c.Id != categoria.Id);
            if (existeNombre)
            {
                throw new InvalidOperationException("Ya existe otra categoría registrada con ese nombre.");
            }

            categoriaDb.Nombre = categoria.Nombre;
            categoriaDb.Descripcion = categoria.Descripcion;

            ctx.SaveChanges();
        }

        // DELETE: Eliminar una categoría con validación de relación
        public void Eliminar(int id)
        {
            using var ctx = new AppDbContext();

            var categoria = ctx.Categorias.Find(id);
            if (categoria == null)
            {
                throw new InvalidOperationException("La categoría no existe o ya fue eliminada.");
            }

            bool tieneProductos = ctx.Productos.Any(p => p.IdCategoria == id);
            if (tieneProductos)
            {
                throw new InvalidOperationException("No se puede eliminar la categoría porque tiene productos vinculados.");
            }

            ctx.Categorias.Remove(categoria);
            ctx.SaveChanges();
        }

        // Reglas de validación para la categoría
        private void ValidarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                throw new ArgumentException("El nombre de la categoría es obligatorio.");
            }

            if (categoria.Nombre.Length > 50)
            {
                throw new ArgumentException("El nombre no debe exceder los 50 caracteres.");
            }

            if (!string.IsNullOrEmpty(categoria.Descripcion) && categoria.Descripcion.Length > 300)
            {
                throw new ArgumentException("La descripción no puede exceder los 300 caracteres.");
            }
        }
    }
}