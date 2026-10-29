using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmUsuario : Form
    {
        private bool _cargandoDatos;
        private UsuarioService _usuarioService;
        private int _idSeleccionado = 0; // 0 significa nuevo, otro valor indica edición

        public FrmUsuario()
        {
            InitializeComponent();
            cmbRol.SelectedIndex = 0;
            _usuarioService = new UsuarioService();
        }

        private void FrmUsuario_Load(object? sender, EventArgs e)
        {
            try
            {
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los usuarios: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarios(int? usuarioSeleccionadoId = null)
        {
            try
            {
                using var db = new AppDbContext();
                var usuarios = db.Usuarios
                    .AsNoTracking()
                    .OrderBy(u => u.Nombre)
                    .ThenBy(u => u.Id)
                    .ToList();

                _cargandoDatos = true;
                dgvUsuarios.Rows.Clear();
                foreach (var usuario in usuarios)
                {
                    dgvUsuarios.Rows.Add(usuario.Id, usuario.Nombre, usuario.Correo, usuario.Telefono, usuario.Rol);
                }

                dgvUsuarios.ClearSelection();
                if (usuarioSeleccionadoId.HasValue)
                {
                    foreach (DataGridViewRow fila in dgvUsuarios.Rows)
                    {
                        if (Convert.ToInt32(fila.Cells[colId.Name].Value) == usuarioSeleccionadoId.Value)
                        {
                            fila.Selected = true;
                            dgvUsuarios.CurrentCell = fila.Cells[colNombre.Name];
                            break;
                        }
                    }
                }

                if (dgvUsuarios.CurrentRow == null && dgvUsuarios.Rows.Count > 0)
                {
                    dgvUsuarios.CurrentCell = dgvUsuarios.Rows[0].Cells[colNombre.Name];
                }

                _cargandoDatos = false;
                MostrarUsuarioSeleccionado();
            }
            catch (Exception ex)
            {
                _cargandoDatos = false;
                MessageBox.Show($"No se pudieron cargar los usuarios: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_SelectionChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDatos)
            {
                MostrarUsuarioSeleccionado();
            }
        }

        private void MostrarUsuarioSeleccionado()
        {
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.CurrentRow.Cells[colId.Name].Value is not int idUsuario)
            {
                return;
            }

            _idSeleccionado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells[colId.Name].Value);
            txtNombre.Text = Convert.ToString(dgvUsuarios.CurrentRow.Cells[colNombre.Name].Value) ?? string.Empty;
            txtCorreo.Text = Convert.ToString(dgvUsuarios.CurrentRow.Cells[colCorreo.Name].Value) ?? string.Empty;
            txtTelefono.Text = Convert.ToString(dgvUsuarios.CurrentRow.Cells[colTelefono.Name].Value) ?? string.Empty;
            txtPassword.Clear(); // La contraseña no se muestra por seguridad en la interfaz

            string rol = Convert.ToString(dgvUsuarios.CurrentRow.Cells[colRol.Name].Value) ?? string.Empty;
            if (!string.IsNullOrEmpty(rol) && cmbRol.Items.Contains(rol))
            {
                cmbRol.SelectedItem = rol;
            }
            else
            {
                cmbRol.SelectedIndex = 0;
            }
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string password = txtPassword.Text;
            string telefono = txtTelefono.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(telefono) || cmbRol.SelectedIndex <= 0)
            {
                MessageBox.Show("Completa nombre, correo, teléfono y selecciona un rol.", "Campos obligatorios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!new EmailAddressAttribute().IsValid(correo))
            {
                MessageBox.Show("Escribe un correo electrónico válido.", "Correo no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            try
            {
                using var db = new AppDbContext();
                Usuario? usuario = _idSeleccionado != 0
                    ? db.Usuarios.Find(_idSeleccionado)
                    : db.Usuarios.FirstOrDefault(item => item.Correo == correo);

                bool esNuevo = usuario == null;
                if (esNuevo && string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("La contraseña es obligatoria para un usuario nuevo.", "Contraseña requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                usuario ??= new Usuario();
                usuario.Nombre = nombre;
                usuario.Correo = correo;
                usuario.Telefono = telefono;
                usuario.Rol = cmbRol.SelectedItem!.ToString()!;

                if (!string.IsNullOrWhiteSpace(password))
                {
                    usuario.Password = password;
                }

                if (esNuevo)
                {
                    usuario.FechaRegistro = DateTime.Now;
                    db.Usuarios.Add(usuario);
                }

                db.SaveChanges();
                MessageBox.Show(esNuevo ? "Usuario registrado exitosamente." : "Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios(usuario.Id);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo guardar el usuario: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    using var db = new AppDbContext();
                    var usuario = db.Usuarios.Find(_idSeleccionado);
                    if (usuario != null)
                    {
                        db.Usuarios.Remove(usuario);
                        db.SaveChanges();
                        MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    CargarUsuarios();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo eliminar el usuario. Puede tener registros asociados. Detalle: {ObtenerMensajeError(ex)}", "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            cmbRol.SelectedIndex = 0;
        }

        private static string ObtenerMensajeError(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
}