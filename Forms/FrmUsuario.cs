using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmUsuario : Form
    {
        // Servicio para manejar operaciones relacionadas con usuarios
        private readonly UsuarioService _usuarioService;
        private int _idSeleccionado = 0;

        // Constructor que inicializa el formulario y el servicio de usuario
        public FrmUsuario()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
        }

        // Evento que se ejecuta al cargar el formulario, cargando los usuarios y limpiando el formulario
        private void FrmUsuario_Load(object? sender, EventArgs e)
        {
            try
            {
                CargarUsuarios();
                LimpiarFormulario();

                // 💡 Suscribir el evento de búsqueda en tiempo real por correo
                if (txtBusqueda != null)
                {
                    txtBusqueda.TextChanged += txtBusqueda_TextChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para cargar los usuarios desde el servicio y mostrarlos en el DataGridView (filtrando opcionalmente por correo)
        private void CargarUsuarios(string filtroCorreo = "")
        {
            try
            {
                var usuarios = _usuarioService.ObtenerTodas();

                // Si hay texto en el buscador, filtramos por correo (ignorando mayúsculas/minúsculas)
                if (!string.IsNullOrWhiteSpace(filtroCorreo))
                {
                    usuarios = usuarios.Where(u => u.Correo != null && u.Correo.Contains(filtroCorreo, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                dgvUsuarios.Rows.Clear();
                foreach (var usuario in usuarios)
                {
                    dgvUsuarios.Rows.Add(usuario.Id, usuario.Nombre, usuario.Correo, usuario.Telefono, usuario.Rol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 💡 Evento que se ejecuta al escribir en el txtBusqueda
        private void txtBusqueda_TextChanged(object? sender, EventArgs e)
        {
            string textoFiltro = txtBusqueda.Text.Trim();
            CargarUsuarios(textoFiltro);
        }

        // Evento que se ejecuta al hacer clic en una celda del DataGridView, llenando los campos del formulario con los datos del usuario seleccionado
        private void dgvUsuarios_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Evita que se ejecute si se hace clic en el encabezado de la columna
            if (e.RowIndex < 0) return;

            var filaReal = dgvUsuarios.Rows[e.RowIndex];

            // Verifica si la celda de la columna "Id" contiene un valor válido y lo asigna a _idSeleccionado
            if (filaReal.Cells[colId.Name].Value is int idUsuario)
            {
                _idSeleccionado = idUsuario;
                txtNombre.Text = Convert.ToString(filaReal.Cells[colNombre.Name].Value) ?? string.Empty;
                txtCorreo.Text = Convert.ToString(filaReal.Cells[colCorreo.Name].Value) ?? string.Empty;
                txtTelefono.Text = Convert.ToString(filaReal.Cells[colTelefono.Name].Value) ?? string.Empty;
                txtPassword.Clear();

                // Selecciona el rol correspondiente en el ComboBox, si existe
                string rol = Convert.ToString(filaReal.Cells[colRol.Name].Value) ?? string.Empty;
                if (!string.IsNullOrEmpty(rol) && cmbRol.Items.Contains(rol))
                {
                    cmbRol.SelectedItem = rol;
                }
                else
                {
                    cmbRol.SelectedIndex = 0;
                }
            }
        }

        // Evento que se ejecuta al hacer clic en el botón "Nuevo", limpiando el formulario para ingresar un nuevo usuario
        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // Evento que se ejecuta al hacer clic en el botón "Guardar", validando los datos y guardando o actualizando el usuario
        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validación de campos obligatorios
                string nombre = txtNombre.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string password = txtPassword.Text;
                string telefono = txtTelefono.Text.Trim();

                // Si el ComboBox tiene un índice 0 o el texto es el placeholder, enviamos vacío para que valide el servicio
                string rol = (cmbRol.SelectedIndex > 0 && cmbRol.SelectedItem != null)
                    ? cmbRol.SelectedItem.ToString()!
                    : string.Empty;

                // Validamos que el Email sea válido
                if (!new EmailAddressAttribute().IsValid(correo))
                {
                    MessageBox.Show("Escribe un correo electrónico válido.", "Correo no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                // Asegura que el rol no esté vacío antes de crear o actualizar el usuario
                var usuario = new Usuario
                {
                    Id = _idSeleccionado,
                    Nombre = nombre,
                    Correo = correo,
                    Telefono = telefono,
                    Rol = rol
                };

                if (_idSeleccionado == 0)
                {
                    // Guardamos el nuevo usuario
                    _usuarioService.Crear(usuario, password);
                    MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualizamos el usuario existente
                    _usuarioService.Actualizar(usuario, password);
                    MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarUsuarios();
                if (txtBusqueda != null) txtBusqueda.Clear();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Evento que se ejecuta al hacer clic en el botón "Eliminar", eliminando el usuario seleccionado después de una confirmación
        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirmación antes de eliminar
            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar al usuario '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Llamada al servicio para eliminar el usuario seleccionado
                    _usuarioService.Eliminar(_idSeleccionado);
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarUsuarios();
                    if (txtBusqueda != null) txtBusqueda.Clear();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // Evento que se ejecuta al hacer clic en el botón "Cancelar", limpiando el formulario y cerrando la ventana
        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        // Método para limpiar los campos del formulario y restablecer el estado inicial
        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            dgvUsuarios.ClearSelection();
            dgvUsuarios.CurrentCell = null;
            txtNombre.Clear();
            txtCorreo.Clear();
            txtPassword.Clear();
            txtTelefono.Clear();
            if (cmbRol.Items.Count > 0) cmbRol.SelectedIndex = 0;
            txtNombre.Focus();
        }
    }
}