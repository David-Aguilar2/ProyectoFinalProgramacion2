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
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
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

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Administrador)
                .WithOne(a => a.Usuario)
                .HasForeignKey<Administrador>(a => a.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Cliente)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Cliente>(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Vendedor)
                .WithOne(v => v.Usuario)
                .HasForeignKey<Vendedor>(v => v.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Vendedor)
                .WithMany(v => v.Pedidos)
                .HasForeignKey(p => p.IdVendedor)
                .OnDelete(DeleteBehavior.Restrict);

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
                    Correo = "admin@mitiandapos.sv",
                    Password = "Admin123!",
                    Telefono = "70000000",
                    FechaRegistro = new DateTime(2026, 1, 1)
                }
            );

            //Vinculación del Usuario como Administrador
            modelBuilder.Entity<Administrador>().HasData(
                new Administrador { Id = 1, IdUsuario = 1 }
            );
        }
    }
}
