using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Forms;
using VentasOnline.Models;

namespace VentasOnline
{
    public partial class Form1 : Form
    {
        private readonly Usuario _usuarioActual;
        private Button? _botonSeleccionadoActual = null;

        // Definimos los colores base para que los cambies a tu gusto fácilmente
        private readonly Color ColorAzulPorDefecto = Color.FromArgb(55, 126, 211); // Tu azul normal
        private readonly Color ColorHover = Color.FromArgb(75, 145, 235);          // Un azul más claro al pasar el mouse
        private readonly Color ColorActivo = Color.FromArgb(35, 95, 175);          // Un azul un poco más oscuro o intenso para el seleccionado (opcional)

        public Form1(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual;
            InitializeComponent();

            // Configurar eventos de clic y efectos de Hover para cada botón del menú
            ConfigurarBotonMenu(btnEnvios, () => SelectNavigationButton(btnEnvios));
            ConfigurarBotonMenu(btnDetallesPedido, () => OpenForm(new FrmDetallePedido(), btnDetallesPedido));
            ConfigurarBotonMenu(productsButton, () => OpenForm(new FrmProductos(), productsButton));
            ConfigurarBotonMenu(btnCategoria, () => OpenForm(new FrmCategorias(), btnCategoria));
            ConfigurarBotonMenu(btnProducto, () => OpenForm(new FrmProductos(), btnProducto));
            ConfigurarBotonMenu(btnCliente, () => OpenForm(new FrmCliente(), btnCliente));
            ConfigurarBotonMenu(btnUsuarios, () => OpenForm(new FrmUsuario(), btnUsuarios));
            ConfigurarBotonMenu(btnPedidos, () => OpenForm(new FrmPedido(_usuarioActual), btnPedidos));
            ConfigurarBotonMenu(btnPagos, () => SelectNavigationButton(btnPagos));

            detallePedidoMenuItem.Click += (_, _) => OpenForm(new FrmDetallePedido(), btnDetallesPedido);

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

            // Al pasar el mouse (MouseEnter)
            boton.MouseEnter += (_, _) =>
            {
                // Si no es el botón seleccionado actualmente, cambia al color de hover
                if (boton != _botonSeleccionadoActual)
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
            bool esAdministrador = string.Equals(_usuarioActual.Rol, "Administrador", StringComparison.OrdinalIgnoreCase);
            btnProducto.Enabled = esAdministrador;
            sessionLabel.Text = $"{_usuarioActual.Nombre}\n({_usuarioActual.Rol})";
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
                        // El botón activo se queda con un tono fijo (puede ser el ColorActivo o el mismo ColorAzulPorDefecto)
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
            SelectNavigationButton(selectedButton);
            using (form)
            {
                form.ShowDialog(this);
            }
        }
    }
}