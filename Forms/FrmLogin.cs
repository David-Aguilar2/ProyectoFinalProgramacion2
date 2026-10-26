using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Forms
{
    public partial class FrmLogin : Form
    {
        public Usuario? UsuarioAutenticado { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object? sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Escriba el correo y la contraseña.", "Inicio de sesión",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var contexto = new AppDbContext();
                Usuario? usuario = contexto.Usuarios
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Correo == correo && u.Password == password);

                if (usuario is null)
                {
                    MessageBox.Show("El correo o la contraseña no son correctos.", "Acceso denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.SelectAll();
                    txtPassword.Focus();
                    return;
                }

                UsuarioAutenticado = usuario;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo iniciar sesión: {ex.Message}", "Error de conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
