using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VentasOnline.Config;
using VentasOnline.Models;

namespace VentasOnline.Data
{
    public class AppDbContext : DbContext
    {
        // Definición de DbSet para cada entidad del modelo
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedidos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Envio> Envios { get; set; }

        public AppDbContext() { }

        // Esto nos permite pasar opciones de configuración al contexto, como la cadena de conexión
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Configuración de la conexión a la base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Reemplazamos la cadena de conexión usando la clase Helper de Config
            if (!options.IsConfigured)
            {
                options.UseSqlServer(ConfiguracionApp.ObtenerCadenaConexion());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación 1 a Muchos: Cliente -> Direcciones
            // Un cliente puede tener múltiples direcciones de envío registradas
            modelBuilder.Entity<Direccion>()
                .HasOne(d => d.Cliente)
                .WithMany(c => c.Direcciones)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación 1 a Muchos: Cliente -> Pedidos
            // Restricción de borrado para proteger el historial de compras
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación 1 a Muchos: Usuario (Vendedor/Admin) -> Pedidos
            // Esto permite que un usuario (vendedor o administrador) pueda estar asociado a múltiples pedidos,
            // pero no se eliminarán los pedidos si el usuario es eliminado, protegiendo así el historial de ventas.
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones de DetallePedido (Tabla Intermedia)
            // La relación entre Pedido y Producto es de muchos a muchos, pero se maneja a través de la entidad DetallePedido.
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.DetallesPedidos)
                .HasForeignKey(dp => dp.IdPedido)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Producto)
                .WithMany(p => p.DetallesPedidos)
                .HasForeignKey(dp => dp.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones de Envío con Courier Externo
            // Un pedido puede tener múltiples envíos, pero cada envío está asociado a un solo pedido.
            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Pedido)
                .WithMany(p => p.Envios)
                .HasForeignKey(e => e.IdPedido)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación de Envío con Dirección
            // Cada envío está asociado a una dirección específica, pero una dirección puede estar asociada a múltiples envíos.
            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Direccion)
                .WithMany(d => d.Envio)
                .HasForeignKey(e => e.IdDireccion)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
