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
        // Variables para controlar la carga de datos y el cliente seleccionado
        private bool _cargandoDatos;
        private int? _clienteSeleccionadoId;
        private ClienteService _clienteService;
        private int _idSeleccionado = 0; // 0 significa nuevo, otro valor indica edición

        // Constructor que inicializa los componentes, configura el diseño visual y crea una instancia del servicio de clientes
        public FrmCliente()
        {
            InitializeComponent();
            ConfigureVisualLayout();
            _clienteService = new ClienteService();
        }

        // Método que configura el diseño visual del formulario, incluyendo colores, fuentes y estilos de los controles
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

        // Evento que se ejecuta al cargar el formulario, intenta cargar los clientes y maneja cualquier excepción que pueda ocurrir
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

        // Método que carga los clientes desde la base de datos y los muestra en el DataGridView
        // opcionalmente seleccionando un cliente específico
        private void CargarClientes(int? clienteSeleccionadoId = null)
        {
            try
            {
                // Cargar clientes desde la base de datos y ordenarlos por nombre y luego por ID
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
                // Si se proporcionó un ID de cliente para seleccionar, buscarlo y seleccionarlo
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

                // Si no hay un cliente seleccionado y hay clientes en la lista, seleccionar el primero
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
        // Este método se ejecuta cuando cambia la selección en el DataGridView de clientes
        // Si no se están cargando datos, llama al método MostrarClienteSeleccionado para actualizar los campos del formulario con la información del cliente seleccionado.
        private void dgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDatos)
            {
                MostrarClienteSeleccionado();
            }
        }

        // Este método muestra la información del cliente seleccionado en los campos de texto del formulario.
        private void MostrarClienteSeleccionado()
        {
            if (dgvClientes.CurrentRow == null || dgvClientes.CurrentRow.Cells[colIdCliente.Name].Value is not int idCliente)
            {
                return;
            }

            // Guardamos el ID del cliente seleccionado en la variable _idSeleccionado para usarlo en operaciones de actualización o eliminación
            _idSeleccionado = Convert.ToInt32(dgvClientes.CurrentRow.Cells[colIdCliente.Name].Value);
            txtNombre.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[colNombre.Name].Value) ?? string.Empty;
            txtTelefono.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[colTelefono.Name].Value) ?? string.Empty;
            txtCorreo.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[colCorreo.Name].Value) ?? string.Empty;
        }

        // Este método se ejecuta cuando se hace clic en el botón "Nuevo". Limpia el formulario y establece el foco en el campo de nombre.
        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
        }

        // Este método se ejecuta cuando se hace clic en el botón "Guardar". Valida los datos del cliente y llama al servicio para crear o actualizar el cliente según corresponda.
        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validar que el nombre no esté vacío
                var cliente = new Cliente
                {
                    Nombre = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim()
                };

                //Validamos que el email sea válido usando la clase EmailAddressAttribute
                if (!new EmailAddressAttribute().IsValid(cliente.Correo))
                {
                    MessageBox.Show("Escribe un correo electrónico válido.", "Correo no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                // Comprobamos si estamos creando un nuevo cliente o actualizando uno existente
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

        // Este método se ejecuta cuando se hace clic en el botón "Eliminar". Verifica si hay un cliente seleccionado, solicita confirmación al usuario y llama al servicio para eliminar el cliente.
        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Confirmación de eliminación
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

        // Este método se ejecuta cuando se hace clic en el botón "Cancelar". Limpia el formulario y cierra la ventana.
        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }
        // Este método limpia los campos del formulario y restablece el ID seleccionado a 0, indicando que no hay un cliente seleccionado.
        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            dgvClientes.ClearSelection();
            dgvClientes.CurrentCell = null;
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }

        // Este método obtiene un mensaje de error a partir de una excepción, devolviendo el mensaje de la excepción interna si está disponible, o el mensaje de la excepción principal en caso contrario.
        private static string ObtenerMensajeError(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
}
