using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Forms;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline
{
    public partial class FrmMenu : Form
    {
        private readonly Usuario _usuarioActual;
        private Button? _botonSeleccionadoActual = null;

        // Definimos los colores base para que los cambies a tu gusto fácilmente
        private readonly Color ColorAzulPorDefecto = Color.FromArgb(55, 126, 211); // Azul
        private readonly Color ColorHover = Color.FromArgb(75, 145, 235);          // Azul más claro al pasar el mouse
        private readonly Color ColorActivo = Color.FromArgb(35, 95, 175);          // Un azul intenso al seleccionar

        public FrmMenu(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual ?? Sesion.UsuarioActual;
            InitializeComponent();

            // Configurar eventos de clic y efectos de Hover para cada botón del menú
            ConfigurarBotonMenu(btnEnvios, () => SelectNavigationButton(btnEnvios));
            //ConfigurarBotonMenu(btnDetallesPedido, () => OpenForm(new FrmDetallePedido(), btnDetallesPedido));
            ConfigurarBotonMenu(btnCategoria, () => OpenForm(new FrmCategorias(), btnCategoria));
            ConfigurarBotonMenu(btnProveedor, () => OpenForm(new FrmProveedores(), btnProveedor));
            ConfigurarBotonMenu(btnProducto, () => OpenForm(new FrmProductos(), btnProducto));
            ConfigurarBotonMenu(btnCliente, () => OpenForm(new FrmCliente(), btnCliente));
            ConfigurarBotonMenu(btnUsuarios, () => OpenForm(new FrmUsuario(), btnUsuarios));
            ConfigurarBotonMenu(btnPedidos, () => OpenForm(new FrmPedido(_usuarioActual), btnPedidos));
            ConfigurarBotonMenu(btnPagos, () => OpenForm(new FrmPagos(), btnPagos));

            SelectNavigationButton(btnEnvios);
            ConfigurePermissions();
        }

        // Método que configura el comportamiento visual del mouse para cada botón
        private void ConfigurarBotonMenu(Button boton, Action accionClick)
        {
            // Asignamos el color azul por defecto al iniciar
            boton.BackColor = ColorAzulPorDefecto;
            boton.ForeColor = Color.White; // Asegura que el texto se lea bien en azul
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;

            boton.Click += (_, _) => accionClick();

            // Al pasar el mouses
            boton.MouseEnter += (_, _) =>
            {
                // Si no es el botón seleccionado actualmente, cambia al color de hover
                if (boton != _botonSeleccionadoActual && boton.Enabled)
                {
                    boton.BackColor = ColorHover;
                }
            };

            // Al retirar el mouse (MouseLeave)
            boton.MouseLeave += (_, _) =>
            {
                // Si no es el seleccionado, regresa al azul por defecto
                if (boton != _botonSeleccionadoActual)
                {
                    boton.BackColor = ColorAzulPorDefecto;
                }
            };
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

            }
        }

        private void ConfigurePermissions()
        {
            if (_usuarioActual == null) return;

            bool esAdministrador = string.Equals(_usuarioActual.Rol, "Administrador", StringComparison.OrdinalIgnoreCase);

            // Restringir módulos exclusivos para Administradores
            btnProveedor.Enabled = esAdministrador;  // Bloqueado para Vendedores
            btnUsuarios.Enabled = esAdministrador;   // Bloqueado para Vendedores
            //admin@localhost.com Test123!
        }

        private void logoutButton_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void SelectNavigationButton(Button selectedButton)
        {
            _botonSeleccionadoActual = selectedButton;

            foreach (Control control in navigationPanel.Controls)
            {
                if (control is Button button)
                {
                    if (button == selectedButton)
                    {
                        // El botón activo se queda con un tono fijo
                        button.BackColor = ColorActivo;
                    }
                    else
                    {
                        // Los demás regresan a su color azul normal
                        button.BackColor = ColorAzulPorDefecto;
                    }
                }
            }
        }

        private void OpenForm(Form form, Button selectedButton)
        {
            // Validación de seguridad por si intentan acceder mediante código
            if (!selectedButton.Enabled)
            {
                MessageBox.Show("No tienes permisos para acceder a este módulo.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                form.Dispose();
                return;
            }

            SelectNavigationButton(selectedButton);
            using (form)
            {
                form.ShowDialog(this);
            }
        }

        private void navigationPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}