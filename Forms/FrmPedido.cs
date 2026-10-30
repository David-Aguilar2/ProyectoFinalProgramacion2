using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Forms
{
    public partial class FrmPedido : Form
    {

        public FrmPedido() : this(null)
        {
        }

        public FrmPedido(Usuario? usuarioActual)
        {
            InitializeComponent();
        }

    }
}
