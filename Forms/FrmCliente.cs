using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using System.ComponentModel.DataAnnotations;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmCliente : Form
    {
        private bool _cargandoDatos;
        private int? _clienteSeleccionadoId;
        private ClienteService _clienteService;
        private int _idSeleccionado = 0; // 0 significa nuevo, otro valor indica edición

        public FrmCliente()
        {
            InitializeComponent();
            ConfigureVisualLayout();
            _clienteService = new ClienteService();
        }

        private void ConfigureVisualLayout()
        {
            Text = "Clientes - SivarPOS";
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1120, 650);
            MinimumSize = new Size(1120, 650);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            lblMarca.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblMarca.ForeColor = Color.FromArgb(25, 71, 132);
            lblMarca.Location = new Point(35, 22);
            lblEncabezado.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblEncabezado.ForeColor = Color.FromArgb(87, 97, 107);
            lblEncabezado.Location = new Point(278, 22);

            dgvClientes.BackgroundColor = Color.FromArgb(222, 222, 222);
            dgvClientes.GridColor = Color.FromArgb(190, 190, 190);
            dgvClientes.EnableHeadersVisualStyles = false;
            dgvClientes.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(205, 205, 205),
                ForeColor = Color.FromArgb(35, 35, 35),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(205, 205, 205),
                SelectionForeColor = Color.FromArgb(35, 35, 35)
            };
            dgvClientes.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(35, 35, 35),
                Font = new Font("Segoe UI", 10F),
                SelectionBackColor = Color.FromArgb(55, 111, 161),
                SelectionForeColor = Color.White
            };
            dgvClientes.RowTemplate.Height = 34;
            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
        }

        private void FrmCliente_Load(object? sender, EventArgs e)
        {
            try
            {
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los clientes: {ex.InnerException?.Message ?? ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarClientes(int? clienteSeleccionadoId = null)
        {
            try
            {
                using var db = new AppDbContext();
                var clientes = db.Clientes
                    .AsNoTracking()
                    .OrderBy(cliente => cliente.Nombre)
                    .ThenBy(cliente => cliente.Id)
                    .ToList();

                _cargandoDatos = true;
                dgvClientes.Rows.Clear();
                foreach (var cliente in clientes)
                {
                    dgvClientes.Rows.Add(cliente.Id, cliente.Nombre, cliente.Telefono, cliente.Correo);
                }

                dgvClientes.ClearSelection();
                if (clienteSeleccionadoId.HasValue)
                {
                    foreach (DataGridViewRow fila in dgvClientes.Rows)
                    {
                        if (Convert.ToInt32(fila.Cells[colIdCliente.Name].Value) == clienteSeleccionadoId.Value)
                        {
                            fila.Selected = true;
                            dgvClientes.CurrentCell = fila.Cells[colNombre.Name];
                            break;
                        }
                    }
                }

                if (dgvClientes.CurrentRow == null && dgvClientes.Rows.Count > 0)
                {
                    dgvClientes.CurrentCell = dgvClientes.Rows[0].Cells[colNombre.Name];
                }

                lblCantidad.Text = $"Clientes registrados: {clientes.Count}";
                _cargandoDatos = false;
                MostrarClienteSeleccionado();
            }
            catch (Exception ex)
            {
                _cargandoDatos = false;
                MessageBox.Show($"No se pudieron cargar los clientes: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDatos)
            {
                MostrarClienteSeleccionado();
            }
        }

        private void MostrarClienteSeleccionado()
        {
            if (dgvClientes.CurrentRow == null || dgvClientes.CurrentRow.Cells[colIdCliente.Name].Value is not int idCliente)
            {
                return;
            }

            _idSeleccionado = Convert.ToInt32(dgvClientes.CurrentRow.Cells[colIdCliente.Name].Value);
            txtNombre.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[colNombre.Name].Value) ?? string.Empty;
            txtTelefono.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[colTelefono.Name].Value) ?? string.Empty;
            txtCorreo.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[colCorreo.Name].Value) ?? string.Empty;
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                var cliente = new Cliente
                {
                    Nombre = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim()
                };

                if (!new EmailAddressAttribute().IsValid(cliente.Correo))
                {
                    MessageBox.Show("Escribe un correo electrónico válido.", "Correo no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                if (_idSeleccionado == 0)
                {
                    // Crear
                    _clienteService.Crear(cliente);
                    MessageBox.Show("Cliente creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualizar
                    cliente.Id = _idSeleccionado; // Asignamos directamente el ID guardado en la variable
                    _clienteService.Actualizar(cliente);
                    MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarClientes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar el cliente '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _clienteService.Eliminar(_idSeleccionado);
                    MessageBox.Show("Cliente eliminado  correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarClientes();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            dgvClientes.ClearSelection();
            dgvClientes.CurrentCell = null;
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }

        private static string ObtenerMensajeError(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
}
