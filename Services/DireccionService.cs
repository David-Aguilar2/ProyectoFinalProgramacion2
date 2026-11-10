using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public class DireccionService
    {
        // Obtener todas las direcciones asociadas a un cliente específico
        public List<Direccion> ObtenerPorCliente(int idCliente)
        {
            using var ctx = new AppDbContext();
            return ctx.Direcciones
                      .AsNoTracking()
                      .Where(d => d.IdCliente == idCliente)
                      .OrderBy(d => d.Departamento)
                      .ToList();
        }

        // Obtener una dirección específica por su ID
        public Direccion? ObtenerPorId(int idDireccion)
        {
            using var ctx = new AppDbContext();
            return ctx.Direcciones
                      .AsNoTracking()
                      .FirstOrDefault(d => d.Id == idDireccion);
        }

        // Registrar una nueva dirección para un cliente
        public int Crear(Direccion direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion.Departamento))
                throw new InvalidOperationException("El departamento es obligatorio.");

            if (string.IsNullOrWhiteSpace(direccion.Ubicacion))
                throw new InvalidOperationException("La ubicación/dirección exacta es obligatoria.");

            using var ctx = new AppDbContext();

            // Validar que el cliente exista
            bool clienteExiste = ctx.Clientes.Any(c => c.Id == direccion.IdCliente);
            if (!clienteExiste)
                throw new InvalidOperationException("El cliente especificado no existe.");

            // La relación es estricta 1:1, validamos que no tenga dirección previa
            bool yaTieneDireccion = ctx.Direcciones.Any(d => d.IdCliente == direccion.IdCliente);
            if (yaTieneDireccion)
                throw new InvalidOperationException("Este cliente ya cuenta con una dirección registrada. Debe actualizar la existente.");

            ctx.Direcciones.Add(direccion);
            ctx.SaveChanges();

            return direccion.Id;
        }

        // Actualizar una dirección existente
        public void Actualizar(Direccion direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion.Departamento))
                throw new InvalidOperationException("El departamento es obligatorio.");

            if (string.IsNullOrWhiteSpace(direccion.Ubicacion))
                throw new InvalidOperationException("La ubicación/dirección exacta es obligatoria.");

            using var ctx = new AppDbContext();
            var dirDb = ctx.Direcciones.FirstOrDefault(d => d.Id == direccion.Id)
                ?? throw new InvalidOperationException("La dirección especificada no existe.");

            dirDb.Departamento = direccion.Departamento;
            dirDb.Ubicacion = direccion.Ubicacion;

            ctx.SaveChanges();
        }

        // Eliminar una dirección (si no tiene envíos vinculados)
        public void Eliminar(int idDireccion)
        {
            using var ctx = new AppDbContext();
            var dirDb = ctx.Direcciones.Include(d => d.Envio)
                                       .FirstOrDefault(d => d.Id == idDireccion)
                ?? throw new InvalidOperationException("La dirección especificada no existe.");

            if (dirDb.Envio.Any())
                throw new InvalidOperationException("No se puede eliminar la dirección porque está asociada a uno o más envíos registrados.");

            ctx.Direcciones.Remove(dirDb);
            ctx.SaveChanges();
        }
    }
}