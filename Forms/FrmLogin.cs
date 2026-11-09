using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Services;
using static System.Collections.Specialized.BitVector32;

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
            string correo = txtCorreo.Text.Trim();
            string password = txtPassword.Text;   // la contraseña No se recorta

            // Validación básica: verificar que los campos no estén vacíos
            if (correo == "" || password == "")
            {
                lblMensaje.Text = "Ingrese correo y contraseña.";
                return;
            }

            try
            {
                // Cambiamos el cursor a "espera" mientras se realiza la validación
                Cursor = Cursors.WaitCursor;
                var usuarioValido = AuthService.IniciarSesion(correo, password);

                // Si el usuario es válido, iniciamos la sesión y cerramos el formulario de login con un resultado OK
                if (usuarioValido != null)
                {
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
                                    "El sistema se cerrará.", "Acceso bloqueado",
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
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo validar el acceso.\n\nDetalle: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
