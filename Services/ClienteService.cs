using System;
using System.Collections.Generic;
using System.Text;
using VentasOnline.Data;
using VentasOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace VentasOnline.Services
{
    public class ClienteService
    {
        // READ

        //Obtener todas las categorías ordenadas
        public List<Cliente> ObtenerTodas()
        {
            // Un contexto por operación
            using var ctx = new AppDbContext();
            return ctx.Clientes.AsNoTracking().OrderBy(c => c.Nombre).ToList();
        }

        //Obtener un cliente por su Id
        public Cliente? ObtenerPorId(int id)
        {
            using var ctx = new AppDbContext();
            return ctx.Clientes.Find(id);
        }

        // CREATE: Insertar un nuevo cliente
        public void Crear(Cliente cliente)
        {
            ValidarCliente(cliente);

            using var ctx = new AppDbContext();

            // Validar que no exista otro cliente con el mismo correo electrónico o teléfono
            bool existe = ctx.Clientes.Any(c => c.Correo.ToLower() == cliente.Correo.ToLower());
            bool existeTelefono = ctx.Clientes.Any(c => c.Telefono == cliente.Telefono);
            if (existe)
            {
                throw new InvalidOperationException("Ya existe un cliente con ese mismo correo electrónico.");
            }

            if (existeTelefono)
            {
                throw new InvalidOperationException("Ya existe un cliente con ese mismo teléfono.");
            }

            ctx.Clientes.Add(cliente);
            ctx.SaveChanges();
        }

        // UPDATE: Actualizar un cliente existente
        public void Actualizar(Cliente cliente)
        {
            ValidarCliente(cliente);

            using var ctx = new AppDbContext();

            var clienteDb = ctx.Clientes.Find(cliente.Id);
            if (clienteDb == null)
            {
                throw new InvalidOperationException("El cliente que intenta actualizar no existe.");
            }

            // Validar duplicidad de correo electrónico y teléfono excluyendo el registro actual
            bool existeCorreo = ctx.Clientes.Any(c => c.Correo.ToLower() == cliente.Correo.ToLower() && c.Id != cliente.Id);
            bool existeTelefono = ctx.Clientes.Any(c => c.Telefono == cliente.Telefono && c.Id != cliente.Id);
            if (existeCorreo)
            {
                throw new InvalidOperationException("Ya existe otro cliente con ese mismo correo electrónico.");
            }
            if (existeTelefono)
            {
                throw new InvalidOperationException("Ya existe otro cliente con ese mismo teléfono.");
            }

            clienteDb.Nombre = cliente.Nombre;
            clienteDb.Telefono = cliente.Telefono;
            clienteDb.Correo = cliente.Correo;

            ctx.SaveChanges();
        }

        // DELETE: Eliminar un cliente con validación de relación
        public void Eliminar(int id)
        {
            using var ctx = new AppDbContext();

            var cliente = ctx.Clientes.Find(id);
            if (cliente == null)
            {
                throw new InvalidOperationException("El cliente no existe o ya fue eliminado.");
            }

            bool tienePedidos = ctx.Pedidos.Any(p => p.IdCliente == id);
            if (tienePedidos)
            {
                throw new InvalidOperationException("No se puede eliminar el cliente porque tiene pedidos vinculados.");
            }

            ctx.Clientes.Remove(cliente);
            ctx.SaveChanges();
        }

        // Reglas de validación para el cliente
        private void ValidarCliente(Cliente cliente)
        {
            //Validar el nombre del cliente
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            }

            if (cliente.Nombre.Length > 100)
            {
                throw new ArgumentException("El nombre no debe exceder los 100 caracteres.");
            }

            // Validar el correo electrónico
            if (string.IsNullOrWhiteSpace(cliente.Correo))
            {
                throw new ArgumentException("El correo electrónico es obligatorio.");
            }

            //Validar el teléfono
            if (string.IsNullOrWhiteSpace(cliente.Telefono))
            {
                throw new ArgumentException("El teléfono es obligatorio.");
            }
        }
    }
}
