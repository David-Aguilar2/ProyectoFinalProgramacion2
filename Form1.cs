using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;

namespace VentasOnline
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

                MessageBox.Show(
                    $"Conexión a TiendaOnlineDb: {(conecta ? "OK" : "FALLÓ")}\n" +
                    $"Categorías registradas: {categorias}\n" +
                    $"Usuarios registrados: {usuarios}\n" +
                    $"Migraciones pendientes: {pendientes}",
                    "TiendaOnline", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message, "TiendaOnline",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
