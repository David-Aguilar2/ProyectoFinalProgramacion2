using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Forms
{
    public partial class FrmEnvio : Form
    {
        private bool _cargandoDatos;
        private int? _pedidoCargadoId;
        private List<Envio> _envios = new();

        public FrmEnvio()
        {
            InitializeComponent();
            cmbEmpresa.Items.AddRange(new object[] { "DHL Express", "FedEx", "UPS", "Correos de El Salvador", "Cargo Expreso" });
            cmbEstado.Items.AddRange(new object[] { "Preparación", "En Tránsito", "Entregado", "Cancelado" });
            cmbEstado.SelectedIndex = 0;
        }

        private void FrmEnvio_Load(object? sender, EventArgs e)
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
                    CargarDireccionesPedido(db, ultimoPedido.Value);
                    _pedidoCargadoId = ultimoPedido.Value;
                }
                else
                {
                    cmbDireccion.DataSource = null;
                }

                CargarEnvios(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los envíos: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEnvios(AppDbContext db)
        {
            _envios = db.Envios
                .AsNoTracking()
                .Include(envio => envio.Direccion)
                .OrderByDescending(envio => envio.Id)
                .ToList();

            _cargandoDatos = true;
            dgvEnvios.Rows.Clear();
            foreach (var envio in _envios)
            {
                dgvEnvios.Rows.Add(envio.Id, envio.IdPedido, envio.EmpresaTransporte, envio.NumeroSeguimiento, envio.Estado);
            }
            _cargandoDatos = false;

            if (dgvEnvios.Rows.Count > 0)
            {
                dgvEnvios.Rows[0].Selected = true;
                MostrarEnvioSeleccionado();
            }
            else
            {
                LimpiarCampos(false);
            }
        }

        private void CargarDireccionesPedido(AppDbContext db, int idPedido, int? idDireccionSeleccionada = null)
        {
            var idCliente = db.Pedidos
                .Where(pedido => pedido.Id == idPedido)
                .Select(pedido => (int?)pedido.IdCliente)
                .FirstOrDefault();

            _cargandoDatos = true;
            if (idCliente.HasValue)
            {
                var direcciones = db.Direcciones
                    .AsNoTracking()
                    .Where(direccion => direccion.IdCliente == idCliente.Value)
                    .OrderBy(direccion => direccion.Departamento)
                    .ThenBy(direccion => direccion.Ubicacion)
                    .Select(direccion => new
                    {
                        direccion.Id,
                        Descripcion = direccion.Departamento + ", " + direccion.Ubicacion
                    })
                    .ToList();

                cmbDireccion.DataSource = direcciones;
                cmbDireccion.DisplayMember = "Descripcion";
                cmbDireccion.ValueMember = "Id";
                if (idDireccionSeleccionada.HasValue)
                {
                    cmbDireccion.SelectedValue = idDireccionSeleccionada.Value;
                }
            }
            else
            {
                cmbDireccion.DataSource = null;
            }
            _cargandoDatos = false;
        }

        private void numIdPedido_ValueChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDatos && _pedidoCargadoId != (int)numIdPedido.Value)
            {
                cmbDireccion.DataSource = null;
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

        private void CargarPedidoSeleccionado(int? idDireccionSeleccionada = null)
        {
            int idPedido = (int)numIdPedido.Value;
            try
            {
                using var db = new AppDbContext();
                if (!db.Pedidos.Any(pedido => pedido.Id == idPedido))
                {
                    _pedidoCargadoId = null;
                    cmbDireccion.DataSource = null;
                    MessageBox.Show($"No existe el pedido #{idPedido}.", "Pedido no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CargarDireccionesPedido(db, idPedido, idDireccionSeleccionada);
                _pedidoCargadoId = idPedido;
                if (cmbDireccion.Items.Count == 0)
                {
                    MessageBox.Show("El pedido seleccionado no tiene una dirección registrada para el cliente.", "Sin dirección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar el pedido: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEnvios_SelectionChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDatos)
            {
                MostrarEnvioSeleccionado();
            }
        }

        private void MostrarEnvioSeleccionado()
        {
            if (dgvEnvios.CurrentRow?.Cells[colIdEnvio.Name].Value is not int idEnvio)
            {
                return;
            }

            var envio = _envios.FirstOrDefault(item => item.Id == idEnvio);
            if (envio == null)
            {
                return;
            }

            _cargandoDatos = true;
            numIdEnvio.Value = envio.Id;
            numIdPedido.Value = envio.IdPedido;
            _cargandoDatos = false;
            CargarPedidoSeleccionado(envio.IdDireccion);
            cmbEmpresa.SelectedItem = envio.EmpresaTransporte;
            if (cmbEmpresa.SelectedIndex < 0)
            {
                cmbEmpresa.Items.Add(envio.EmpresaTransporte);
                cmbEmpresa.SelectedItem = envio.EmpresaTransporte;
            }
            txtNumeroGuia.Text = envio.NumeroSeguimiento;
            cmbEstado.SelectedItem = envio.Estado;
            if (cmbEstado.SelectedIndex < 0)
            {
                cmbEstado.Items.Add(envio.Estado);
                cmbEstado.SelectedItem = envio.Estado;
            }
            dtpFechaEnvio.Value = envio.FechaEnvio;
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarCampos(true);
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

            if (cmbDireccion.SelectedValue is not int idDireccion)
            {
                MessageBox.Show("Selecciona una dirección de envío del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDireccion.Focus();
                return;
            }

            if (cmbEmpresa.SelectedItem is not string empresa || string.IsNullOrWhiteSpace(txtNumeroGuia.Text))
            {
                MessageBox.Show("Selecciona la empresa de transporte e ingresa el número de guía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEnvio = (int)numIdEnvio.Value;
            try
            {
                using var db = new AppDbContext();
                var pedido = db.Pedidos.Find(idPedido);
                if (pedido == null)
                {
                    throw new InvalidOperationException($"No existe el pedido #{idPedido}.");
                }

                bool direccionPerteneceAlCliente = db.Direcciones.Any(direccion =>
                    direccion.Id == idDireccion && direccion.IdCliente == pedido.IdCliente);
                if (!direccionPerteneceAlCliente)
                {
                    throw new InvalidOperationException("La dirección seleccionada no pertenece al cliente del pedido.");
                }

                Envio? envio;
                if (idEnvio > 0)
                {
                    envio = db.Envios.FirstOrDefault(item => item.Id == idEnvio);
                    if (envio == null)
                    {
                        throw new InvalidOperationException("El envío seleccionado ya no existe. Actualiza la lista e intenta de nuevo.");
                    }
                }
                else
                {
                    envio = new Envio();
                    db.Envios.Add(envio);
                }

                envio.IdPedido = idPedido;
                envio.IdDireccion = idDireccion;
                envio.EmpresaTransporte = empresa;
                envio.NumeroSeguimiento = txtNumeroGuia.Text.Trim();
                envio.Estado = cmbEstado.SelectedItem?.ToString() ?? "Preparación";
                envio.FechaEnvio = dtpFechaEnvio.Value;
                db.SaveChanges();

                MessageBox.Show($"Envío #{envio.Id} guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEnvios(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo guardar el envío: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            int idEnvio = (int)numIdEnvio.Value;
            if (idEnvio <= 0)
            {
                MessageBox.Show("Selecciona un envío de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"¿Quieres eliminar el envío #{idEnvio}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var db = new AppDbContext();
                var envio = db.Envios.Find(idEnvio);
                if (envio == null)
                {
                    throw new InvalidOperationException("El envío seleccionado ya no existe.");
                }

                db.Envios.Remove(envio);
                db.SaveChanges();
                MessageBox.Show("El envío se eliminó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEnvios(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo eliminar el envío: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void LimpiarCampos(bool limpiarPedido)
        {
            _cargandoDatos = true;
            numIdEnvio.Value = 0;
            if (limpiarPedido)
            {
                numIdPedido.Value = 0;
                _pedidoCargadoId = null;
                cmbDireccion.DataSource = null;
            }
            txtNumeroGuia.Clear();
            if (cmbEmpresa.Items.Count > 0)
            {
                cmbEmpresa.SelectedIndex = -1;
            }
            if (cmbEstado.Items.Count > 0)
            {
                cmbEstado.SelectedItem = "Preparación";
                if (cmbEstado.SelectedIndex < 0)
                {
                    cmbEstado.SelectedIndex = 0;
                }
            }
            dtpFechaEnvio.Value = DateTime.Today;
            _cargandoDatos = false;
        }

        private static string ObtenerMensajeError(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
}
