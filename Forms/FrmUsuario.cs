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
    public partial class FrmUsuario : Form
    {
        // Servicio para manejar operaciones relacionadas con usuarios
        private readonly UsuarioService _usuarioService;
        private int _idSeleccionado = 0;
        private readonly Usuario? _usuarioActual; // Usuario autenticado actualmente en la sesión

        // Constructor por defecto que llama al constructor principal con null
        public FrmUsuario() : this(null) { }

        // Constructor que inicializa el formulario, el servicio de usuario y recibe la sesión actual
        public FrmUsuario(Usuario? usuarioActual)
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
            _usuarioActual = usuarioActual ?? Sesion.UsuarioActual;
        }

        // Evento que se ejecuta al cargar el formulario, cargando los usuarios y limpiando el formulario
        private void FrmUsuario_Load(object? sender, EventArgs e)
        {
            try
            {
                CargarUsuarios();
                LimpiarFormulario();

                // Asegurar que el botón de eliminar inicie deshabilitado al cargar
                if (btnEliminar != null)
                {
                    btnEliminar.Enabled = false;
                }

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

                // Habilitar el botón de eliminar al seleccionar un usuario válido de la grilla
                if (btnEliminar != null)
                {
                    btnEliminar.Enabled = true;
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
                string correo = txtCorreo.Text.Trim().ToLowerInvariant(); // Normalización a minúsculas
                string password = txtPassword.Text;
                string telefono = txtTelefono.Text.Trim();

                // Validación: El nombre no puede estar vacío ni contener únicamente espacios
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("El nombre del usuario es obligatorio y no puede contener únicamente espacios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Validación: Longitud máxima del nombre (100 caracteres)
                if (nombre.Length > 100)
                {
                    MessageBox.Show("El nombre del usuario no puede superar los 100 caracteres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Validación: El correo electrónico no puede estar vacío
                if (string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show("El correo electrónico del usuario es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                // Validación: Longitud máxima del correo (150 caracteres)
                if (correo.Length > 150)
                {
                    MessageBox.Show("El correo electrónico no puede superar los 150 caracteres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                // Validamos que el Email sea válido
                if (!new EmailAddressAttribute().IsValid(correo))
                {
                    MessageBox.Show("Escribe un correo electrónico válido.", "Correo no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                // Validación: El teléfono es obligatorio
                if (string.IsNullOrWhiteSpace(telefono))
                {
                    MessageBox.Show("El teléfono del usuario es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }

                // Validación: El teléfono debe contener únicamente números y exactamente 8 dígitos
                if (!System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d{8}$"))
                {
                    MessageBox.Show("El teléfono debe contener únicamente números y tener una longitud exacta de 8 dígitos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }

                // Validación de Rol explícito (asumiendo que el índice 0 es un texto de selección o vacío)
                if (cmbRol.SelectedIndex <= 0 || cmbRol.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un rol válido para el usuario.", "Rol requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.Focus();
                    return;
                }

                string rol = cmbRol.SelectedItem.ToString()!;

                // Validación de Contraseña
                if (_idSeleccionado == 0)
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("La contraseña es obligatoria para un nuevo usuario.", "Contraseña requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassword.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(password))
                {
                    if (password.Length < 6)
                    {
                        MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Seguridad de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassword.Focus();
                        return;
                    }

                    // Validar complejidad básica (letras y números)
                    bool tieneLetras = password.Any(char.IsLetter);
                    bool tieneDigitos = password.Any(char.IsDigit);
                    if (!tieneLetras || !tieneDigitos)
                    {
                        MessageBox.Show("La contraseña debe contener al menos una letra y un número.", "Seguridad de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassword.Focus();
                        return;
                    }
                }

                // Validación de unicidad de correo en base de datos
                var listaUsuarios = _usuarioService.ObtenerTodas();
                if (listaUsuarios.Any(u => u.Correo != null && u.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase) && u.Id != _idSeleccionado))
                {
                    MessageBox.Show("Ya existe otro usuario registrado con este mismo correo electrónico.", "Correo duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                using var dbCheck = new AppDbContext();

                if (_idSeleccionado == 0)
                {
                    // Guardamos el nuevo usuario
                    _usuarioService.Crear(usuario, password);
                    MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Validar que el usuario todavía exista antes de actualizarlo
                    var usuarioExistente = dbCheck.Usuarios.Find(_idSeleccionado);
                    if (usuarioExistente == null)
                    {
                        MessageBox.Show("El usuario seleccionado ya no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CargarUsuarios();
                        LimpiarFormulario();
                        return;
                    }

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

            // Evitar que un usuario pueda eliminarse a sí mismo
            if (_usuarioActual != null && _usuarioActual.Id == _idSeleccionado)
            {
                MessageBox.Show("No puedes eliminar tu propia cuenta de usuario activa.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Validar que el usuario a eliminar exista en la base de datos
            using var db = new AppDbContext();
            var usuarioAEliminar = db.Usuarios.Find(_idSeleccionado);
            if (usuarioAEliminar == null)
            {
                MessageBox.Show("El usuario seleccionado ya no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarUsuarios();
                LimpiarFormulario();
                return;
            }

            // Evitar eliminar el último administrador del sistema
            if (usuarioAEliminar.Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            {
                int totalAdministradores = db.Usuarios.Count(u => u.Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase));
                if (totalAdministradores <= 1)
                {
                    MessageBox.Show("No se puede eliminar el último administrador registrado en el sistema.", "Restricción de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
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

            // Deshabilitar el botón de eliminar al limpiar el formulario
            if (btnEliminar != null)
            {
                btnEliminar.Enabled = false;
            }

            txtNombre.Focus();
        }
    }
}