using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VentasOnline.Data;
using VentasOnline.Dtos;
using VentasOnline.Models;
using VentasOnline.Services;
using Microsoft.EntityFrameworkCore;

namespace VentasOnline.Forms
{
    public partial class FrmPedido : Form
    {
        private readonly PedidoService _servicio = new();
        private readonly BindingList<LineaPedido> _lineas = new();
        private int _pedidoId = 0; // 0 = Pedido nuevo
        private readonly Usuario? _usuarioActual;

        public FrmPedido() : this(null) { }

        public FrmPedido(Usuario? usuarioActual)
        {
            InitializeComponent();
            _usuarioActual = usuarioActual ?? Sesion.UsuarioActual;
        }

        private void FrmPedido_Load(object sender, EventArgs e)
        {
            CargarCatalogos();
            dgvDetalle.DataSource = _lineas;

            if (dgvDetalle.Columns["ProductoId"] != null)
                dgvDetalle.Columns["ProductoId"].Visible = false;

            if (dgvDetalle.Columns["PrecioUnitario"] != null)
            {
                dgvDetalle.Columns["PrecioUnitario"].HeaderText = "Precio";
                dgvDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
            }

            if (dgvDetalle.Columns["Subtotal"] != null)
                dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "N2";

            CargarHistorial();
            NuevoPedido();
        }

        private void CargarCatalogos()
        {
            cboCliente.DisplayMember = "Nombre";
            cboCliente.ValueMember = "Id";
            cboCliente.DataSource = _servicio.ListarClientes();

            cboUsuario.DisplayMember = "Nombre";
            cboUsuario.ValueMember = "Id";
            cboUsuario.DataSource = _servicio.ListarUsuarios();
            cboUsuario.Enabled = false; // Bloqueado para reflejar únicamente al usuario autenticado

            cboProducto.DisplayMember = "Nombre";
            cboProducto.ValueMember = "Id";
            cboProducto.DataSource = _servicio.ListarProductosActivos();
        }

        private void CargarHistorial()
        {
            dgvPedidos.DataSource = _servicio.ListarPedidos()
                .Select(p => new
                {
                    p.Id,
                    Fecha = p.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    Cliente = p.Cliente?.Nombre ?? "Sin Cliente",
                    p.Total
                })
                .ToList();
        }

        private void NuevoPedido()
        {
            _pedidoId = 0;
            _lineas.Clear();
            nudCantidad.Value = 1;
            lblVenta.Text = "Pedido nuevo";
            btnEliminar.Enabled = false;
            dgvPedidos.ClearSelection();
            ActualizarTotal();
            LimpiarImagenProducto();

            if (_usuarioActual != null)
            {
                cboUsuario.SelectedValue = _usuarioActual.Id;
            }
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = $"Total: ${_lineas.Sum(l => l.Subtotal):N2}";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.SelectedItem is not Producto producto) return;
            int cantidad = (int)nudCantidad.Value;

            LineaPedido? existente = _lineas.FirstOrDefault(l => l.ProductoId == producto.Id);
            if (existente != null)
            {
                existente.Cantidad += cantidad;
                _lineas.ResetBindings();
            }
            else
            {
                _lineas.Add(new LineaPedido
                {
                    ProductoId = producto.Id,
                    Producto = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                });
            }
            nudCantidad.Value = 1;
            ActualizarTotal();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow?.DataBoundItem is LineaPedido linea)
            {
                _lineas.Remove(linea);
                ActualizarTotal();
                LimpiarImagenProducto();
            }
        }

        private void btnNueva_Click(object sender, EventArgs e) => NuevoPedido();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedValue is not int clienteId ||
                cboUsuario.SelectedValue is not int usuarioId)
            {
                MessageBox.Show("Selecciona el cliente y el usuario.", "VentasOnline", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_pedidoId == 0)
                {
                    int id = _servicio.Registrar(clienteId, usuarioId, _lineas);
                    MessageBox.Show($"Pedido #{id} registrado exitosamente.", "VentasOnline",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _servicio.Actualizar(_pedidoId, clienteId, usuarioId, _lineas);
                    MessageBox.Show($"Pedido #{_pedidoId} actualizado exitosamente.", "VentasOnline",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CargarCatalogos();
                CargarHistorial();
                NuevoPedido();
            }
            catch (Exception ex)
            {
                string mensajeReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show(mensajeReal, "Error interno de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int id = Convert.ToInt32(dgvPedidos.Rows[e.RowIndex].Cells["Id"].Value);

            Pedido? pedido = _servicio.ObtenerConDetalle(id);
            if (pedido == null) return;

            _pedidoId = pedido.Id;
            cboCliente.SelectedValue = pedido.IdCliente;
            cboUsuario.SelectedValue = pedido.IdUsuario;

            _lineas.Clear();
            foreach (var d in pedido.DetallesPedidos)
            {
                _lineas.Add(new LineaPedido
                {
                    ProductoId = d.Producto?.Id ?? 0,
                    Producto = d.Producto?.Nombre ?? "Desconocido",
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                });
            }
            lblVenta.Text = $"Editando pedido #{pedido.Id}";
            btnEliminar.Enabled = true;
            ActualizarTotal();
            LimpiarImagenProducto();
        }

        private void dgvDetalle_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow?.DataBoundItem is LineaPedido linea)
            {
                using var ctx = new AppDbContext();
                var producto = ctx.Productos.AsNoTracking().FirstOrDefault(p => p.Id == linea.ProductoId);

                if (producto != null && !string.IsNullOrEmpty(producto.ImagenUrl))
                {
                    string rutaBase = Application.StartupPath;
                    string rutaCompleta = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images", producto.ImagenUrl));

                    if (File.Exists(rutaCompleta))
                    {
                        try
                        {
                            pictureBoxProducto.Image = Image.FromFile(rutaCompleta);
                            pictureBoxProducto.SizeMode = PictureBoxSizeMode.Zoom;
                            return;
                        }
                        catch { /* Evita bloqueos si la imagen está en uso */ }
                    }
                }
            }
            LimpiarImagenProducto();
        }

        private void LimpiarImagenProducto()
        {
            if (pictureBoxProducto.Image != null)
            {
                pictureBoxProducto.Image.Dispose();
                pictureBoxProducto.Image = null;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_pedidoId == 0) return;

            var respuesta = MessageBox.Show(
                $"¿Eliminar el pedido #{_pedidoId}?\nSe devolverá al inventario el stock de sus productos.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            try
            {
                int idEliminado = _pedidoId;
                _servicio.Eliminar(idEliminado);

                CargarCatalogos();
                CargarHistorial();
                NuevoPedido();

                MessageBox.Show($"El pedido #{idEliminado} fue eliminado y el stock fue devuelto al inventario.",
                    "Pedido eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo eliminar el pedido.\n\nDetalle: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}