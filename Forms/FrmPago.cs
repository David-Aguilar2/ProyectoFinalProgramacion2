using System;
using System.Windows.Forms;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Forms
{
    public partial class FrmPago : Form
    {
        public FrmPago()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPedidoId.Text, out int pedidoId) ||
                !decimal.TryParse(txtMonto.Text, out decimal monto))
            {
                MessageBox.Show("El ID de pedido y el monto deben ser números válidos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                var nuevoPago = new Pago
                {
                    IdPedido= pedidoId,
                    Metodo = txtMetodo.Text,
                    Monto = monto,
                    Estado = txtEstado.Text,
                    FechaPago = DateTime.Now
                };

                db.Pagos.Add(nuevoPago);
                db.SaveChanges();
            }

            MessageBox.Show("Pago registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtPedidoId.Clear();
            txtMetodo.Clear();
            txtMonto.Clear();
            txtEstado.Clear();
        }
    }
}