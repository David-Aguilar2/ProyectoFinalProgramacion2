using System;
using System.Windows.Forms;
using VentasOnline.Data; // Ajusta el namespace según tu DbContext
using VentasOnline.Models; // Ajusta el namespace según tus Modelos

namespace VentasOnline.Forms
{
    public partial class FrmCliente : Form
    {
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtTeléfono.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Por favor completa todos los campos.", "Campos obligatorios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                var nuevoCliente = new Cliente
                {
                    Nombre = txtNombre.Text,
                    Telefono = txtTeléfono.Text,
                    Correo = txtCorreo.Text
                };

                db.Clientes.Add(nuevoCliente);
                db.SaveChanges();
            }

            MessageBox.Show("Cliente guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtNombre.Clear();
            txtTeléfono.Clear();
            txtCorreo.Clear();
        }
    }
}