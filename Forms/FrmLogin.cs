using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmLogin : Form
    {
        // Propiedad para almacenar el usuario autenticado
        public Usuario? UsuarioAutenticado { get; private set; }
        private const int MaxIntentos = 3;// Número máximo de intentos permitidos
        private int intentosFallidos = 0;// Contador de intentos fallidos

        public FrmLogin()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se carga el formulario
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            ActiveControl = txtCorreo;      // el cursor inicia en el correo

        }

        // Evento que se ejecuta cuando se cambia el estado del CheckBox para mostrar/ocultar la contraseña
        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrar.Checked)
            {
                // Si está marcado, desactivamos el ocultamiento del sistema para mostrar el texto plano
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                // Si se desmarca volvemos a impedir que se muestre la contraseña en texto plano
                txtPassword.PasswordChar = '\0';
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        // Evento que se ejecuta cuando se hace clic en el botón "Ingresar" para validar el correo y la contraseña ingresados
        private void btnIngresar_Click(object? sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim().ToLowerInvariant(); // Normalización a minúsculas
            string password = txtPassword.Text;   // la contraseña No se recorta

            // Validación básica: verificar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrEmpty(password))
            {
                lblMensaje.Text = "Ingrese correo y contraseña.";
                return;
            }

            // Validación: Longitud máxima del correo (150 caracteres)
            if (correo.Length > 150)
            {
                lblMensaje.Text = "El correo electrónico excede la longitud permitida.";
                txtCorreo.Focus();
                return;
            }

            // Validación: Formato válido del correo electrónico
            if (!new EmailAddressAttribute().IsValid(correo))
            {
                lblMensaje.Text = "Escribe un correo electrónico válido.";
                txtCorreo.Focus();
                return;
            }

            // Validación: Longitud mínima de la contraseña antes de enviarla al servicio (ej. al menos 6 caracteres)
            if (password.Length < 6)
            {
                lblMensaje.Text = "La contraseña debe tener al menos 6 caracteres.";
                txtPassword.Focus();
                return;
            }

            try
            {
                // Cambiamos el cursor a "espera" mientras se realiza la validación
                Cursor = Cursors.WaitCursor;

                // Validación de cuenta bloqueada o inactiva en base de datos previamente
                using var dbCtx = new AppDbContext();
                var usuarioEnDb = dbCtx.Usuarios.FirstOrDefault(u => u.Correo != null && u.Correo.ToLower() == correo);

                var usuarioValido = AuthService.IniciarSesion(correo, password);

                // Si el usuario es válido, iniciamos la sesión y cerramos el formulario de login con un resultado OK
                if (usuarioValido != null)
                {
                    // Validar que el usuario tenga un rol válido al autenticarse
                    if (string.IsNullOrWhiteSpace(usuarioValido.Rol))
                    {
                        MessageBox.Show("El usuario autenticado no tiene un rol válido asignado en el sistema.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    Sesion.Iniciar(usuarioValido);
                    DialogResult = DialogResult.OK;   // cierra el login y devuelve OK
                    return;
                }

                intentosFallidos++;
                int restantes = MaxIntentos - intentosFallidos;

                // Si se han agotado los intentos permitidos, mostramos un mensaje y cerramos el formulario con un resultado Cancel
                if (restantes <= 0)
                {
                    MessageBox.Show("Superó el número de intentos permitidos.\n" +
                                    "El sistema se cerrará por seguridad.", "Acceso bloqueado",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                // Mensaje genérico: no revela si falló el usuario o la contraseña
                lblMensaje.Text = "Usuario o contraseña incorrectos. " +
                                  $"Intentos restantes: {restantes}";
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (Exception)
            {
                // Evitar mostrar detalles internos de excepciones técnicas al usuario por seguridad
                MessageBox.Show("Ocurrió un error al intentar validar el acceso con el servidor.",
                    "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}