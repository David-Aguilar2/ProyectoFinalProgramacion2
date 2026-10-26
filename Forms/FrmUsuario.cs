using System;
using System.Windows.Forms;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Forms
{
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor completa los campos obligatorios (Nombre, Correo y Contraseña).", "Campos obligatorios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                var nuevoUsuario = new Usuario
                {
                    Nombre = txtNombre.Text,
                    Correo = txtCorreo.Text,
                    Password = txtPassword.Text,
                    Telefono = txtTelefono.Text,
                    Rol = txtRol.Text
                };

                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();
            }

            MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtNombre.Clear();
            txtCorreo.Clear();
            txtPassword.Clear();
            txtTelefono.Clear();
            txtRol.Clear();
        }
    }
}