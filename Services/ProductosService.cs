using System;
using System.Collections.Generic;
using System.Text;
using VentasOnline.Data;
using VentasOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace VentasOnline.Services
{
    public class ProductosService
    {
        // READ

        //Obtener todas los peoductos ordenados
        public List<Producto> ObtenerTodos()
        {
            using var ctx = new AppDbContext();
            return ctx.Productos
                .Include(p => p.Categoria)
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        // Obtener un producto por Id
        public Producto? ObtenerPorId(int id)
        {
            using var ctx = new AppDbContext();
            return ctx.Productos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);
        }

        // CREATE

        // Insertar un nuevo producto
        public void Crear(Producto producto)
        {
            ValidarProducto(producto);

            using var ctx = new AppDbContext();

            // Validar que exista la categoría asignada
            bool categoriaExiste = ctx.Categorias.Any(c => c.Id == producto.IdCategoria);
            if (!categoriaExiste)
            {
                throw new InvalidOperationException("La categoría seleccionada no existe.");
            }

            // Validar duplicidad de nombre del producto
            bool existeNombre = ctx.Productos.Any(p => p.Nombre.ToLower() == producto.Nombre.ToLower());
            if (existeNombre)
            {
                throw new InvalidOperationException("Ya existe un producto registrado con ese mismo nombre.");
            }

            ctx.Productos.Add(producto);
            ctx.SaveChanges();
        }

        // UPDATE

        // Actualizar un producto existente
        public void Actualizar(Producto producto)
        {
            ValidarProducto(producto);

            using var ctx = new AppDbContext();

            var productoDb = ctx.Productos.Find(producto.Id);
            if (productoDb == null)
            {
                throw new InvalidOperationException("El producto que intenta actualizar no existe.");
            }

            // Validar que la categoría exista
            bool categoriaExiste = ctx.Categorias.Any(c => c.Id == producto.IdCategoria);
            if (!categoriaExiste)
            {
                throw new InvalidOperationException("La categoría seleccionada no existe.");
            }

            // Validar duplicidad de nombre excluyendo el registro actual
            bool existeNombre = ctx.Productos.Any(p => p.Nombre.ToLower() == producto.Nombre.ToLower() && p.Id != producto.Id);
            if (existeNombre)
            {
                throw new InvalidOperationException("Ya existe otro producto registrado con ese nombre.");
            }

            // Actualización de campos
            productoDb.Nombre = producto.Nombre;
            productoDb.Descripcion = producto.Descripcion;
            productoDb.Precio = producto.Precio;
            productoDb.Stock = producto.Stock;
            productoDb.ImagenUrl = producto.ImagenUrl; // Ruta o base64 de la imagen según tu modelo
            productoDb.IdCategoria = producto.IdCategoria;

            ctx.SaveChanges();
        }

        // DELETE
        // Eliminar un producto con validación de historial/ventas
        public void Eliminar(int id)
        {
            using var ctx = new AppDbContext();

            var producto = ctx.Productos.Find(id);
            if (producto == null)
            {
                throw new InvalidOperationException("El producto no existe o ya fue eliminado.");
            }

            // Validar si el producto está ligado a detalles de pedidos antes de eliminar
            bool tieneVentas = ctx.DetallesPedidos.Any(dv => dv.IdProducto == id);
            if (tieneVentas)
            {
                throw new InvalidOperationException("No se puede eliminar el producto porque tiene historial de ventas asociado.");
            }

            ctx.Productos.Remove(producto);
            ctx.SaveChanges();
        }

        // Reglas de validación del producto
        private void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }

            if (producto.Nombre.Length > 100)
            {
                throw new ArgumentException("El nombre no debe exceder los 100 caracteres.");
            }

            if (producto.IdCategoria <= 0)
            {
                throw new ArgumentException("Debe seleccionar una categoría válida.");
            }

            if (producto.Precio < 0)
            {
                throw new ArgumentException("El precio no puede ser un valor negativo.");
            }

            if (producto.Stock < 0)
            {
                throw new ArgumentException("El stock no puede ser un valor negativo.");
            }

            if (!string.IsNullOrEmpty(producto.Descripcion) && producto.Descripcion.Length > 500)
            {
                throw new ArgumentException("La descripción no puede exceder los 500 caracteres.");
            }
        }
    }
}
