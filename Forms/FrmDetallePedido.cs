using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Forms
{
    public partial class FrmDetallePedido : Form
    {
        private readonly int? _pedidoInicial;
        private List<DetallePedido> _detalles = new();
        private int? _pedidoCargadoId;
        private bool _cargandoDetalle;

        public FrmDetallePedido() : this(null)
        {
        }

        public FrmDetallePedido(int? idPedido)
        {
            _pedidoInicial = idPedido;
            InitializeComponent();
            ActualizarSubtotal();
        }

        private void FrmDetallePedido_Load(object? sender, EventArgs e)
        {
            try
            {
                using var db = new AppDbContext();
                cmbProducto.DataSource = db.Productos
                    .AsNoTracking()
                    .OrderBy(producto => producto.Nombre)
                    .ToList();
                cmbProducto.DisplayMember = nameof(Producto.Nombre);
                cmbProducto.ValueMember = nameof(Producto.Id);

                int? idPedido = _pedidoInicial ?? db.Pedidos
                    .OrderByDescending(pedido => pedido.Id)
                    .Select(pedido => (int?)pedido.Id)
                    .FirstOrDefault();

                if (idPedido.HasValue)
                {
                    numIdPedido.Value = idPedido.Value;
                    CargarDetallesPedido();
                }
                else
                {
                    LimpiarCamposDetalle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los detalles del pedido: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numIdPedido_ValueChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDetalle && _pedidoCargadoId != (int)numIdPedido.Value)
            {
                dgvDetalles.ClearSelection();
                LimpiarCamposDetalle();
            }
        }

        private void numIdPedido_Leave(object? sender, EventArgs e)
        {
            if (_pedidoCargadoId != (int)numIdPedido.Value)
            {
                CargarDetallesPedido();
            }
        }

        private void numIdPedido_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarDetallesPedido();
                e.SuppressKeyPress = true;
            }
        }

        private void CargarDetallesPedido()
        {
            int idPedido = (int)numIdPedido.Value;
            if (idPedido <= 0)
            {
                _pedidoCargadoId = null;
                _detalles.Clear();
                dgvDetalles.Rows.Clear();
                LimpiarCamposDetalle();
                return;
            }

            try
            {
                using var db = new AppDbContext();
                if (!db.Pedidos.Any(pedido => pedido.Id == idPedido))
                {
                    _pedidoCargadoId = null;
                    _detalles.Clear();
                    dgvDetalles.Rows.Clear();
                    LimpiarCamposDetalle();
                    MessageBox.Show($"No existe el pedido #{idPedido}.", "Pedido no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _detalles = db.DetallesPedidos
                    .AsNoTracking()
                    .Include(detalle => detalle.Producto)
                    .Where(detalle => detalle.IdPedido == idPedido)
                    .OrderBy(detalle => detalle.Id)
                    .ToList();
                _pedidoCargadoId = idPedido;

                _cargandoDetalle = true;
                dgvDetalles.Rows.Clear();
                foreach (var detalle in _detalles)
                {
                    dgvDetalles.Rows.Add(detalle.Id, detalle.Producto.Nombre, detalle.Cantidad, detalle.PrecioUnitario, detalle.SubTotal);
                }

                LimpiarCamposDetalle();
                if (dgvDetalles.Rows.Count > 0)
                {
                    dgvDetalles.Rows[0].Selected = true;
                    MostrarDetalleSeleccionado();
                }
                _cargandoDetalle = false;
            }
            catch (Exception ex)
            {
                _cargandoDetalle = false;
                MessageBox.Show($"No se pudieron cargar los detalles del pedido: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDetalles_SelectionChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDetalle)
            {
                MostrarDetalleSeleccionado();
            }
        }

        private void MostrarDetalleSeleccionado()
        {
            if (dgvDetalles.CurrentRow?.Cells[colIdDetalle.Name].Value is not int idDetalle)
            {
                return;
            }

            var detalle = _detalles.FirstOrDefault(item => item.Id == idDetalle);
            if (detalle == null)
            {
                return;
            }

            _cargandoDetalle = true;
            numIdDetalle.Value = detalle.Id;
            cmbProducto.SelectedValue = detalle.IdProducto;
            numCantidad.Value = detalle.Cantidad;
            numPrecioUnitario.Value = Math.Min(numPrecioUnitario.Maximum, detalle.PrecioUnitario);
            txtSubtotal.Text = detalle.SubTotal.ToString("C2");
            _cargandoDetalle = false;
        }

        private void cmbProducto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_cargandoDetalle && cmbProducto.SelectedItem is Producto producto)
            {
                numPrecioUnitario.Value = Math.Min(numPrecioUnitario.Maximum, producto.Precio);
                ActualizarSubtotal();
            }
        }

        private void numCantidad_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarSubtotal();
        }

        private void numPrecioUnitario_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarSubtotal();
        }

        private void ActualizarSubtotal()
        {
            if (txtSubtotal != null && numCantidad != null && numPrecioUnitario != null)
            {
                txtSubtotal.Text = (numCantidad.Value * numPrecioUnitario.Value).ToString("C2");
            }
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            dgvDetalles.ClearSelection();
            LimpiarCamposDetalle();
            numIdDetalle.Value = 0;
            if (cmbProducto.Items.Count > 0)
            {
                cmbProducto.SelectedIndex = 0;
            }
            numCantidad.Value = 1;
            if (cmbProducto.SelectedItem is Producto producto)
            {
                numPrecioUnitario.Value = Math.Min(numPrecioUnitario.Maximum, producto.Precio);
            }
            ActualizarSubtotal();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            int idPedido = (int)numIdPedido.Value;
            if (idPedido <= 0 || _pedidoCargadoId != idPedido)
            {
                MessageBox.Show("Escribe un ID de pedido existente y presiona Enter para cargarlo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numIdPedido.Focus();
                return;
            }

            if (cmbProducto.SelectedItem is not Producto productoSeleccionado)
            {
                MessageBox.Show("Selecciona un producto para el detalle.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProducto.Focus();
                return;
            }

            int idDetalle = (int)numIdDetalle.Value;
            int idProducto = productoSeleccionado.Id;
            int cantidad = (int)numCantidad.Value;
            decimal precioUnitario = numPrecioUnitario.Value;
            decimal subtotal = cantidad * precioUnitario;

            try
            {
                using var db = new AppDbContext();
                using var transaccion = db.Database.BeginTransaction();

                var pedido = db.Pedidos.Find(idPedido);
                if (pedido == null)
                {
                    throw new InvalidOperationException($"No existe el pedido #{idPedido}.");
                }

                var detalleExistente = idDetalle > 0
                    ? db.DetallesPedidos.FirstOrDefault(detalle => detalle.Id == idDetalle && detalle.IdPedido == idPedido)
                    : null;
                if (idDetalle > 0 && detalleExistente == null)
                {
                    throw new InvalidOperationException("El detalle seleccionado ya no existe. Vuelve a cargar el pedido.");
                }

                var productoNuevo = db.Productos.Find(idProducto);
                if (productoNuevo == null)
                {
                    throw new InvalidOperationException("El producto seleccionado ya no existe.");
                }

                if (detalleExistente != null && detalleExistente.IdProducto == idProducto)
                {
                    int stockDisponible = productoNuevo.Stock + detalleExistente.Cantidad;
                    if (cantidad > stockDisponible)
                    {
                        throw new InvalidOperationException($"Solo hay {stockDisponible} unidades disponibles de {productoNuevo.Nombre} para este pedido.");
                    }
                    productoNuevo.Stock = stockDisponible - cantidad;
                }
                else
                {
                    if (detalleExistente != null)
                    {
                        var productoAnterior = db.Productos.Find(detalleExistente.IdProducto);
                        if (productoAnterior == null)
                        {
                            throw new InvalidOperationException("No se encontró el producto asociado al detalle actual.");
                        }
                        productoAnterior.Stock += detalleExistente.Cantidad;
                    }

                    if (cantidad > productoNuevo.Stock)
                    {
                        throw new InvalidOperationException($"Solo hay {productoNuevo.Stock} unidades disponibles de {productoNuevo.Nombre}.");
                    }
                    productoNuevo.Stock -= cantidad;
                }

                if (detalleExistente == null)
                {
                    detalleExistente = new DetallePedido { IdPedido = idPedido };
                    db.DetallesPedidos.Add(detalleExistente);
                }

                detalleExistente.IdProducto = idProducto;
                detalleExistente.Cantidad = cantidad;
                detalleExistente.PrecioUnitario = precioUnitario;
                detalleExistente.SubTotal = subtotal;
                db.SaveChanges();

                pedido.Total = db.DetallesPedidos
                    .Where(detalle => detalle.IdPedido == idPedido)
                    .Sum(detalle => detalle.SubTotal) + pedido.CostoEnvio;
                db.SaveChanges();
                transaccion.Commit();

                CargarDetallesPedido();
                MessageBox.Show("El detalle del pedido se guardó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo guardar el detalle: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            int idDetalle = (int)numIdDetalle.Value;
            int idPedido = (int)numIdPedido.Value;
            if (idDetalle <= 0)
            {
                MessageBox.Show("Selecciona un detalle de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show("¿Quieres eliminar el detalle seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var db = new AppDbContext();
                using var transaccion = db.Database.BeginTransaction();
                var detalle = db.DetallesPedidos.FirstOrDefault(item => item.Id == idDetalle && item.IdPedido == idPedido);
                if (detalle == null)
                {
                    throw new InvalidOperationException("El detalle ya no existe. Vuelve a cargar el pedido.");
                }

                var pedido = db.Pedidos.Find(idPedido);
                var producto = db.Productos.Find(detalle.IdProducto);
                if (pedido == null || producto == null)
                {
                    throw new InvalidOperationException("No se encontraron el pedido o producto relacionado con el detalle.");
                }

                producto.Stock += detalle.Cantidad;
                db.DetallesPedidos.Remove(detalle);
                db.SaveChanges();
                pedido.Total = db.DetallesPedidos
                    .Where(item => item.IdPedido == idPedido)
                    .Sum(item => item.SubTotal) + pedido.CostoEnvio;
                db.SaveChanges();
                transaccion.Commit();

                CargarDetallesPedido();
                MessageBox.Show("El detalle se eliminó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo eliminar el detalle: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void LimpiarCamposDetalle()
        {
            _cargandoDetalle = true;
            numIdDetalle.Value = 0;
            numCantidad.Value = 1;
            if (cmbProducto.Items.Count > 0)
            {
                cmbProducto.SelectedIndex = 0;
            }
            if (cmbProducto.SelectedItem is Producto producto)
            {
                numPrecioUnitario.Value = Math.Min(numPrecioUnitario.Maximum, producto.Precio);
            }
            else
            {
                numPrecioUnitario.Value = 0;
            }
            _cargandoDetalle = false;
            ActualizarSubtotal();
        }

        private static string ObtenerMensajeError(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
}
