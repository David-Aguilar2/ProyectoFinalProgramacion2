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

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

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
            // Reemplaza la antigua relación con la tabla Vendedor
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones de DetallePedido (Tabla Intermedia)
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
            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Pedido)
                .WithMany(p => p.Envios)
                .HasForeignKey(e => e.IdPedido)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Direccion)
                .WithMany(d => d.Envio)
                .HasForeignKey(e => e.IdDireccion)
                .OnDelete(DeleteBehavior.Restrict);

            //Usuario inicial (Admin)
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Administrador Principal",
                    Correo = "admin@tiendaOnline.sv",
                    Password = "Admin123!",
                    Telefono = "70000000",
                    Rol = "Administrador",
                    FechaRegistro = new DateTime(2026, 1, 1)
                }
            );
        }
    }
}
