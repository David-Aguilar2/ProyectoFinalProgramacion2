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

        // Colores base para el hover del menú y el botón activo
        private readonly Color ColorAzulPorDefecto = Color.FromArgb(55, 126, 211); // Azul
        private readonly Color ColorHover = Color.FromArgb(75, 145, 235);          // Azul más claro al pasar el mouse
        private readonly Color ColorActivo = Color.FromArgb(35, 95, 175);          // Un azul intenso al seleccionar

        public FrmMenu(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual ?? Sesion.UsuarioActual;
            InitializeComponent();

            // Eventos de clic y efectos de Hover para cada botón del menú
            // Con este evento, al hacer clic en un botón, se selecciona y se abre el formulario correspondiente
            ConfigurarBotonMenu(btnEnvios, () => SelectNavigationButton(btnEnvios));
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

        // Evento que se ejecuta al cargar el formulario principal
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {// Verificar la conexión a la base de datos y obtener información básica
                using var contexto = new AppDbContext();
                bool conecta = contexto.Database.CanConnect();
                int categorias = contexto.Categorias.Count();
                int usuarios = contexto.Usuarios.Count();
                int pendientes = contexto.Database.GetPendingMigrations().Count();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método que configura los permisos de los botones según el rol del usuario
        private void ConfigurePermissions()
        {
            if (_usuarioActual == null) return;

            bool esAdministrador = string.Equals(_usuarioActual.Rol, "Administrador", StringComparison.OrdinalIgnoreCase);

            // Restringir módulos exclusivos para Administradores
            btnProveedor.Enabled = esAdministrador;  // Bloqueado para Vendedores
            btnUsuarios.Enabled = esAdministrador;   // Bloqueado para Vendedores
            //admin@localhost.com Test123!
        }

        // Evento que se ejecuta al hacer clic en el botón de cerrar sesión
        private void logoutButton_Click(object? sender, EventArgs e)
        {
            Close();
        }

        // Método que se usa para que el botón seleccionado se mantenga con un color fijo y los demás regresen a su color azul normal
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

        // Método que abre un formulario y valida si el usuario tiene permisos para acceder a él
        private void OpenForm(Form form, Button selectedButton)
        {
            // Validación de seguridad por si intentan acceder mediante código
            if (!selectedButton.Enabled)
            {
                MessageBox.Show("No tienes permisos para acceder a este módulo.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                form.Dispose();
                return;
            }
            // Si el usuario tiene permisos, se selecciona el botón y se abre el formulario
            SelectNavigationButton(selectedButton);
            using (form)
            {
                form.ShowDialog(this);
            }
        }

        private void navigationPanel_Paint(object sender, PaintEventArgs e){}
    }
}