using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmCliente : Form
    {
        // Instancia del servicio para manejar la lógica de negocio de los clientes
        private ClienteService _clienteService;

        // Variable para controlar si se está creando un registro nuevo (0) o editando uno existente
        private int _idSeleccionado = 0; // 0 significa nuevo, otro valor indica edición

        public FrmCliente()
        {
            InitializeComponent();
            ConfigureVisualLayout();
            _clienteService = new ClienteService();
        }

        // Método para configurar el diseño visual y las propiedades iniciales de la ventana y sus controles
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

            dgvClientes.CellClick += dgvClientes_CellClick;

            // Asegurar que el botón de direcciones inicie deshabilitado
            if (btnDireccion != null)
            {
                btnDireccion.Enabled = false;
                btnDireccion.Click += btnDireccion_Click;
            }
        }

        // Evento que se ejecuta al cargar el formulario, inicializando los datos y limpiando campos
        private void FrmCliente_Load(object? sender, EventArgs e)
        {
            try
            {
                CargarClientes();
                LimpiarFormulario();

                // 💡 Suscribir el evento de búsqueda en tiempo real por correo
                if (txtBusqueda != null)
                {
                    txtBusqueda.TextChanged += txtBusqueda_TextChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los clientes: {ex.InnerException?.Message ?? ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para consultar la base de datos y llenar la tabla (DataGridView) filtrando opcionalmente por correo
        private void CargarClientes(string filtroCorreo = "")
        {
            try
            {
                using var db = new AppDbContext();
                var query = db.Clientes.AsNoTracking().AsQueryable();

                // Si hay texto en el buscador, filtramos por correo (ignorando mayúsculas/minúsculas)
                if (!string.IsNullOrWhiteSpace(filtroCorreo))
                {
                    query = query.Where(c => c.Correo != null && c.Correo.Contains(filtroCorreo));
                }

                var clientes = query
                    .OrderBy(cliente => cliente.Nombre)
                    .ThenBy(cliente => cliente.Id)
                    .ToList();

                dgvClientes.Rows.Clear();
                foreach (var cliente in clientes)
                {
                    dgvClientes.Rows.Add(cliente.Id, cliente.Nombre, cliente.Telefono, cliente.Correo);
                }

                lblCantidad.Text = $"Clientes registrados: {clientes.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los clientes: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 💡 Evento que se ejecuta al escribir en el txtBusqueda
        private void txtBusqueda_TextChanged(object? sender, EventArgs e)
        {
            string textoFiltro = txtBusqueda.Text.Trim();
            CargarClientes(textoFiltro);
        }

        // Evento único para manejar la selección manual por celda y cargar los datos en los TextBox para su edición
        private void dgvClientes_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvClientes.Rows[e.RowIndex];

            if (fila.Cells[colIdCliente.Name].Value is int idCliente)
            {
                _idSeleccionado = idCliente;
                txtNombre.Text = Convert.ToString(fila.Cells[colNombre.Name].Value) ?? string.Empty;
                txtTelefono.Text = Convert.ToString(fila.Cells[colTelefono.Name].Value) ?? string.Empty;
                txtCorreo.Text = Convert.ToString(fila.Cells[colCorreo.Name].Value) ?? string.Empty;

                // Al hacer clic en un cliente válido, habilitamos el botón de direcciones
                if (btnDireccion != null)
                {
                    btnDireccion.Enabled = true;
                }
            }
        }

        // Evento del botón "Nuevo", limpia el formulario para permitir el registro de un cliente nuevo
        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // Evento del botón "Dirección", abre el formulario de direcciones asignando el cliente actual por defecto
        private void btnDireccion_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccione un cliente primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var frmDirecciones = new FrmDireccion(_idSeleccionado);
                frmDirecciones.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir el módulo de direcciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón "Guardar", valida los campos obligatorios y el formato del correo antes de crear o actualizar
        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string correo = txtCorreo.Text.Trim();

                // Validación: El nombre no puede estar vacío
                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("El nombre del cliente es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Validación: El correo electrónico debe tener un formato válido
                if (!new EmailAddressAttribute().IsValid(correo))
                {
                    MessageBox.Show("Escribe un correo electrónico válido.", "Correo no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                var cliente = new Cliente
                {
                    Id = _idSeleccionado,
                    Nombre = nombre,
                    Telefono = telefono,
                    Correo = correo
                };

                if (_idSeleccionado == 0)
                {
                    _clienteService.Crear(cliente);
                    MessageBox.Show("Cliente creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _clienteService.Actualizar(cliente);
                    MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarClientes();
                if (txtBusqueda != null) txtBusqueda.Clear();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Evento del botón "Eliminar", solicita confirmación y procede a borrar el cliente seleccionado de la base de datos
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
                    MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarClientes();
                    if (txtBusqueda != null) txtBusqueda.Clear();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // Evento del botón "Cancelar", limpia el formulario y cierra la ventana actual
        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        // Método auxiliar para restablecer los controles de entrada y deseleccionar la tabla
        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            dgvClientes.ClearSelection();
            dgvClientes.CurrentCell = null;
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();

            if (btnDireccion != null)
            {
                btnDireccion.Enabled = false;
            }

            txtNombre.Focus();
        }

        // Método auxiliar para extraer el mensaje de error principal o interno de una excepción
        private static string ObtenerMensajeError(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
}