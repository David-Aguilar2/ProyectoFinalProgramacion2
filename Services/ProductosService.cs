using System;
using System.Collections.Generic;
using System.Linq;
using VentasOnline.Data;
using VentasOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace VentasOnline.Services
{
    public class ProductosService
    {
        public List<Producto> ObtenerTodos()
        {
            using var ctx = new AppDbContext();
            return ctx.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public Producto? ObtenerPorId(int id)
        {
            using var ctx = new AppDbContext();
            return ctx.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Crear(Producto producto)
        {
            ValidarProducto(producto);

            using var ctx = new AppDbContext();

            bool categoriaExiste = ctx.Categorias.Any(c => c.Id == producto.IdCategoria);
            if (!categoriaExiste)
            {
                throw new InvalidOperationException("La categoría seleccionada no existe.");
            }

            bool proveedorExiste = ctx.Proveedores.Any(p => p.Id == producto.IdProveedor);
            if (!proveedorExiste)
            {
                throw new InvalidOperationException("El proveedor seleccionado no existe.");
            }

            bool existeNombre = ctx.Productos.Any(p => p.Nombre.ToLower() == producto.Nombre.ToLower());
            if (existeNombre)
            {
                throw new InvalidOperationException("Ya existe un producto registrado con ese mismo nombre.");
            }

            ctx.Productos.Add(producto);
            ctx.SaveChanges();
        }

        public void Actualizar(Producto producto)
        {
            ValidarProducto(producto);

            using var ctx = new AppDbContext();

            var productoDb = ctx.Productos.Find(producto.Id);
            if (productoDb == null)
            {
                throw new InvalidOperationException("El producto que intenta actualizar no existe.");
            }

            bool categoriaExiste = ctx.Categorias.Any(c => c.Id == producto.IdCategoria);
            if (!categoriaExiste)
            {
                throw new InvalidOperationException("La categoría seleccionada no existe.");
            }

            bool proveedorExiste = ctx.Proveedores.Any(p => p.Id == producto.IdProveedor);
            if (!proveedorExiste)
            {
                throw new InvalidOperationException("El proveedor seleccionado no existe.");
            }

            bool existeNombre = ctx.Productos.Any(p => p.Nombre.ToLower() == producto.Nombre.ToLower() && p.Id != producto.Id);
            if (existeNombre)
            {
                throw new InvalidOperationException("Ya existe otro producto registrado con ese nombre.");
            }

            productoDb.Nombre = producto.Nombre;
            productoDb.Descripcion = producto.Descripcion;
            productoDb.Precio = producto.Precio;
            productoDb.Stock = producto.Stock;
            productoDb.ImagenUrl = producto.ImagenUrl;
            productoDb.IdCategoria = producto.IdCategoria;
            productoDb.IdProveedor = producto.IdProveedor;

            ctx.SaveChanges();
        }

        public void Eliminar(int id)
        {
            using var ctx = new AppDbContext();

            var producto = ctx.Productos.Find(id);
            if (producto == null)
            {
                throw new InvalidOperationException("El producto no existe o ya fue eliminado.");
            }

            bool tieneVentas = ctx.DetallesPedidos.Any(dv => dv.IdProducto == id);
            if (tieneVentas)
            {
                throw new InvalidOperationException("No se puede eliminar el producto porque tiene historial de ventas asociado.");
            }

            ctx.Productos.Remove(producto);
            ctx.SaveChanges();
        }

        private void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (producto.Nombre.Length > 100)
                throw new ArgumentException("El nombre no debe exceder los 100 caracteres.");

            if (producto.IdCategoria <= 0)
                throw new ArgumentException("Debe seleccionar una categoría válida.");

            if (producto.IdProveedor <= 0)
                throw new ArgumentException("Debe seleccionar un proveedor válido.");

            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");

            if (producto.Stock < 0)
                throw new ArgumentException("El stock no puede ser un valor negativo.");

            if (string.IsNullOrWhiteSpace(producto.ImagenUrl))
                throw new ArgumentException("La imagen del producto es obligatoria.");

            if (!string.IsNullOrEmpty(producto.Descripcion) && producto.Descripcion.Length > 500)
                throw new ArgumentException("La descripción no puede exceder los 500 caracteres.");
        }
    }
}