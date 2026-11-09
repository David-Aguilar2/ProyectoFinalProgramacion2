using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Services
{
    public class PagoService
    {
        // Listar todos los pagos
        public List<Pago> ObtenerPagosPorPedido(int idPedido)
        {
            using var ctx = new AppDbContext();
            return ctx.Pagos.AsNoTracking()
                      .Where(p => p.IdPedido == idPedido)
                      .ToList();
        }

        // Método para listar todos los pagos del sistema (útil para la grilla general)
        public List<Pago> ObtenerTodosLosPagos()
        {
            using var ctx = new AppDbContext();
            return ctx.Pagos.AsNoTracking().ToList();
        }

        // Registrar automáticamente un pago en estado "Pendiente" al crear el pedido (FechaPago es null)
        public void RegistrarPagoPendiente(AppDbContext ctx, int idPedido, decimal montoTotal)
        {
            var pagoPendiente = new Pago
            {
                IdPedido = idPedido,
                Monto = montoTotal,
                Estado = "Pendiente",
                Metodo = "Por definir",
                FechaPago = null // Aún no se ha pagado
            };

            ctx.Pagos.Add(pagoPendiente);
        }

        // Actualizar el estado, método y monto del pago
        public void ActualizarPago(int pagoId, string nuevoEstado, string nuevoMetodo, decimal nuevoMonto)
        {
            using var ctx = new AppDbContext();
            var pago = ctx.Pagos.FirstOrDefault(p => p.Id == pagoId)
                ?? throw new InvalidOperationException("El registro de pago especificado no existe.");

            if (nuevoEstado == "Completado")
            {
                if (string.IsNullOrWhiteSpace(nuevoMetodo) || nuevoMetodo == "Por definir")
                    throw new InvalidOperationException("Debe seleccionar un método de pago válido (Efectivo, Tarjeta, etc.) para completar el pago.");

                // Si pasa a completado, registramos la fecha y hora exacta del cobro
                pago.FechaPago = DateTime.Now;
            }
            else if (nuevoEstado == "Pendiente" || nuevoEstado == "Cancelado")
            {
                // Si regresa a pendiente o se cancela, la fecha vuelve a ser nula
                pago.FechaPago = null;
            }

            pago.Estado = nuevoEstado;
            pago.Metodo = nuevoMetodo;
            pago.Monto = nuevoMonto;

            ctx.SaveChanges();
        }
    }
}