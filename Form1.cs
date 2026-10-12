using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Forms;
using VentasOnline.Models;

namespace VentasOnline
{
    public partial class Form1 : Form
    {
        private readonly Usuario _usuarioActual;

        public Form1(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual;
            InitializeComponent();
            homeButton.Click += (_, _) => SelectNavigationButton(homeButton);
            categoriesButton.Click += (_, _) => OpenForm(new FrmCategorias(), categoriesButton);
            productsButton.Click += (_, _) => OpenForm(new FrmProductos(), productsButton);
            clientsButton.Click += (_, _) => SelectNavigationButton(clientsButton);
            usersButton.Click += (_, _) => SelectNavigationButton(usersButton);
            ordersButton.Click += (_, _) => SelectNavigationButton(ordersButton);
            paymentsButton.Click += (_, _) => SelectNavigationButton(paymentsButton);
            shippingButton.Click += (_, _) => SelectNavigationButton(shippingButton);
            reportsButton.Click += (_, _) => SelectNavigationButton(reportsButton);
            SelectNavigationButton(homeButton);
            ConfigurePermissions();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                using var contexto = new AppDbContext();

                bool conecta = contexto.Database.CanConnect();
                int categorias = contexto.Categorias.Count();
                int usuarios = contexto.Usuarios.Count();
                int pendientes = contexto.Database.GetPendingMigrations().Count();

            }
            catch (Exception ex)
            {
                // La pantalla principal también puede abrirse sin conexión a la base de datos.
            }
        }

        private void ConfigurePermissions()
        {
            bool esAdministrador = string.Equals(_usuarioActual.Rol, "Administrador", StringComparison.OrdinalIgnoreCase);
            usersButton.Enabled = esAdministrador;
            sessionLabel.Text = $"{_usuarioActual.Nombre}\n({_usuarioActual.Rol})";
        }

        private void logoutButton_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void SelectNavigationButton(Button selectedButton)
        {
            foreach (Control control in navigationPanel.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = button == selectedButton
                        ? Color.FromArgb(55, 126, 211)
                        : Color.Transparent;
                }
            }
        }

        private void OpenForm(Form form, Button selectedButton)
        {
            SelectNavigationButton(selectedButton);
            using (form)
            {
                form.ShowDialog(this);
            }
        }

        private void logoLabel_Click(object sender, EventArgs e)
        {

        }

        private void categoriesButton_Click(object sender, EventArgs e)
        {

        }

        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void productsButton_Click(object sender, EventArgs e)
        {

        }
    }
}
