using System.ComponentModel.DataAnnotations;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmUsuario : Form
    {
        private readonly UsuarioService _usuarioService;
        private int _idSeleccionado = 0;

        public FrmUsuario()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
        }

        private void FrmUsuario_Load(object? sender, EventArgs e)
        {
            try
            {
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                var usuarios = _usuarioService.ObtenerTodas();
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

        private void dgvUsuarios_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvUsuarios.Rows[e.RowIndex]; // Asegúrate de que el nombre del control sea dgvUsuarios
            // Corregido: dgvUsuarios.Rows[e.RowIndex]
            var filaReal = dgvUsuarios.Rows[e.RowIndex];

            if (filaReal.Cells[colId.Name].Value is int idUsuario)
            {
                _idSeleccionado = idUsuario;
                txtNombre.Text = Convert.ToString(filaReal.Cells[colNombre.Name].Value) ?? string.Empty;
                txtCorreo.Text = Convert.ToString(filaReal.Cells[colCorreo.Name].Value) ?? string.Empty;
                txtTelefono.Text = Convert.ToString(filaReal.Cells[colTelefono.Name].Value) ?? string.Empty;
                txtPassword.Clear();

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

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string password = txtPassword.Text;
                string telefono = txtTelefono.Text.Trim();

                // Si el ComboBox tiene un índice 0 o el texto es el placeholder, enviamos vacío para que valide el servicio
                string rol = (cmbRol.SelectedIndex > 0 && cmbRol.SelectedItem != null)
                    ? cmbRol.SelectedItem.ToString()!
                    : string.Empty;

                if (!new EmailAddressAttribute().IsValid(correo))
                {
                    MessageBox.Show("Escribe un correo electrónico válido.", "Correo no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

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
                    _usuarioService.Crear(usuario, password);
                    MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _usuarioService.Actualizar(usuario, password);
                    MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarUsuarios();
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
                MessageBox.Show("Seleccione un usuario de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar al usuario '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _usuarioService.Eliminar(_idSeleccionado);
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarUsuarios();
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