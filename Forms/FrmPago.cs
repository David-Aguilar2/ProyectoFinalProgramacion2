using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Forms
{
    public partial class FrmPago : Form
    {
        private bool _cargandoDatos;
        private int? _pedidoCargadoId;
        private List<Pago> _pagos = new();

        public FrmPago()
        {
            InitializeComponent();
            cmbMetodo.Items.AddRange(new object[] { "Efectivo", "Tarjeta de crédito", "Transferencia", "PayPal" });
            cmbEstado.Items.AddRange(new object[] { "Pendiente", "Completado", "Fallido", "Reembolsado" });
            cmbEstado.SelectedIndex = 0;
        }

        private void FrmPago_Load(object? sender, EventArgs e)
        {
            try
            {
                using var db = new AppDbContext();
                int? ultimoPedido = db.Pedidos
                    .OrderByDescending(pedido => pedido.Id)
                    .Select(pedido => (int?)pedido.Id)
                    .FirstOrDefault();

                if (ultimoPedido.HasValue)
                {
                    _cargandoDatos = true;
                    numIdPedido.Value = ultimoPedido.Value;
                    _cargandoDatos = false;
                    CargarPedidoSeleccionado();
                }

                CargarPagos(db);
            }
            catch (Exception ex)
            {
                _cargandoDatos = false;
                MessageBox.Show($"No se pudieron cargar los pagos: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPagos(AppDbContext db)
        {
            _pagos = db.Pagos
                .AsNoTracking()
                .OrderByDescending(pago => pago.Id)
                .ToList();

            _cargandoDatos = true;
            dgvPagos.Rows.Clear();
            foreach (var pago in _pagos)
            {
                dgvPagos.Rows.Add(pago.Id, pago.IdPedido, pago.Metodo, pago.Monto.ToString("C2"), pago.Estado, pago.FechaPago.ToString("dd/MM/yyyy HH:mm"));
            }
            _cargandoDatos = false;

            if (dgvPagos.Rows.Count > 0)
            {
                dgvPagos.Rows[0].Selected = true;
                MostrarPagoSeleccionado();
            }
            else
            {
                LimpiarCampos();
            }
        }

        private void numIdPedido_ValueChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDatos && _pedidoCargadoId != (int)numIdPedido.Value)
            {
                _pedidoCargadoId = null;
            }
        }

        private void numIdPedido_Leave(object? sender, EventArgs e)
        {
            if (_pedidoCargadoId != (int)numIdPedido.Value)
            {
                CargarPedidoSeleccionado();
            }
        }

        private void numIdPedido_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarPedidoSeleccionado();
                e.SuppressKeyPress = true;
            }
        }

        private void CargarPedidoSeleccionado(bool actualizarMonto = true)
        {
            int idPedido = (int)numIdPedido.Value;
            if (idPedido <= 0)
            {
                _pedidoCargadoId = null;
                return;
            }

            try
            {
                using var db = new AppDbContext();
                var pedido = db.Pedidos
                    .AsNoTracking()
                    .Where(item => item.Id == idPedido)
                    .Select(item => new { item.Id, item.Total })
                    .FirstOrDefault();

                if (pedido == null)
                {
                    _pedidoCargadoId = null;
                    MessageBox.Show($"No existe el pedido #{idPedido}.", "Pedido no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _pedidoCargadoId = pedido.Id;
                if (actualizarMonto)
                {
                    numMonto.Value = Math.Min(numMonto.Maximum, pedido.Total);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar el pedido: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPagos_SelectionChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDatos)
            {
                MostrarPagoSeleccionado();
            }
        }

        private void MostrarPagoSeleccionado()
        {
            if (dgvPagos.CurrentRow?.Cells[colIdPago.Name].Value is not int idPago)
            {
                return;
            }

            var pago = _pagos.FirstOrDefault(item => item.Id == idPago);
            if (pago == null)
            {
                return;
            }

            _cargandoDatos = true;
            numIdPago.Value = pago.Id;
            numIdPedido.Value = pago.IdPedido;
            _cargandoDatos = false;
            CargarPedidoSeleccionado(false);
            cmbMetodo.SelectedItem = pago.Metodo;
            if (cmbMetodo.SelectedIndex < 0)
            {
                cmbMetodo.Items.Add(pago.Metodo);
                cmbMetodo.SelectedItem = pago.Metodo;
            }
            numMonto.Value = Math.Min(numMonto.Maximum, pago.Monto);
            cmbEstado.SelectedItem = pago.Estado;
            if (cmbEstado.SelectedIndex < 0)
            {
                cmbEstado.Items.Add(pago.Estado);
                cmbEstado.SelectedItem = pago.Estado;
            }
            dtpFechaPago.Value = pago.FechaPago;
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            dgvPagos.ClearSelection();
            LimpiarCampos();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            int idPedido = (int)numIdPedido.Value;
            if (idPedido <= 0 || _pedidoCargadoId != idPedido)
            {
                MessageBox.Show("Escribe el ID de un pedido existente y presiona Enter para cargarlo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numIdPedido.Focus();
                return;
            }

            if (cmbMetodo.SelectedItem is not string metodo || cmbEstado.SelectedItem is not string estado)
            {
                MessageBox.Show("Selecciona el método y el estado del pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPago = (int)numIdPago.Value;
            try
            {
                using var db = new AppDbContext();
                if (!db.Pedidos.Any(pedido => pedido.Id == idPedido))
                {
                    throw new InvalidOperationException($"No existe el pedido #{idPedido}.");
                }

                Pago pago;
                if (idPago > 0)
                {
                    pago = db.Pagos.FirstOrDefault(item => item.Id == idPago)
                        ?? throw new InvalidOperationException("El pago seleccionado ya no existe. Actualiza la lista e intenta de nuevo.");
                }
                else
                {
                    pago = new Pago();
                    db.Pagos.Add(pago);
                }

                pago.IdPedido = idPedido;
                pago.Metodo = metodo;
                pago.Monto = numMonto.Value;
                pago.FechaPago = dtpFechaPago.Value;
                pago.Estado = estado;
                db.SaveChanges();

                MessageBox.Show($"Pago #{pago.Id} guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarPagos(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo guardar el pago: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            int idPago = (int)numIdPago.Value;
            if (idPago <= 0)
            {
                MessageBox.Show("Selecciona un pago de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"¿Quieres eliminar el pago #{idPago}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var db = new AppDbContext();
                var pago = db.Pagos.Find(idPago)
                    ?? throw new InvalidOperationException("El pago seleccionado ya no existe.");

                db.Pagos.Remove(pago);
                db.SaveChanges();
                MessageBox.Show("El pago se eliminó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarPagos(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo eliminar el pago: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void LimpiarCampos()
        {
            _cargandoDatos = true;
            numIdPago.Value = 0;
            numIdPedido.Value = 0;
            _pedidoCargadoId = null;
            cmbMetodo.SelectedIndex = -1;
            numMonto.Value = 0;
            dtpFechaPago.Value = DateTime.Now;
            if (cmbEstado.Items.Count > 0)
            {
                cmbEstado.SelectedItem = "Pendiente";
                if (cmbEstado.SelectedIndex < 0)
                {
                    cmbEstado.SelectedIndex = 0;
                }
            }
            _cargandoDatos = false;
        }

        private static string ObtenerMensajeError(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
}
