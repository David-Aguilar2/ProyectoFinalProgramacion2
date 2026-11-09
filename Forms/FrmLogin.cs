using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Services;
using static System.Collections.Specialized.BitVector32;

namespace VentasOnline.Forms
{
    public partial class FrmLogin : Form
    {
        public Usuario? UsuarioAutenticado { get; private set; }
        private const int MaxIntentos = 3;
        private int intentosFallidos = 0;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            ActiveControl = txtCorreo;      // el cursor inicia en el correo

        }
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

        private void btnIngresar_Click(object? sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string password = txtPassword.Text;   // la contraseña No se recorta

            if (correo == "" || password == "")
            {
                lblMensaje.Text = "Ingrese correo y contraseña.";
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var usuarioValido = AuthService.IniciarSesion(correo, password);

                if (usuarioValido != null)
                {
                    Sesion.Iniciar(usuarioValido);
                    DialogResult = DialogResult.OK;   // cierra el login y devuelve OK
                    return;
                }

                intentosFallidos++;
                int restantes = MaxIntentos - intentosFallidos;

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
