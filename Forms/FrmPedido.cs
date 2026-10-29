using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;

namespace VentasOnline.Forms
{
    public partial class FrmPedido : Form
    {
        private readonly Usuario? _usuarioActual;
        private readonly List<DetallePedido> _detalles = new();

        public FrmPedido() : this(null)
        {
        }

        public FrmPedido(Usuario? usuarioActual)
        {
            _usuarioActual = usuarioActual;
            InitializeComponent();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            cmbEstado.Items.AddRange(new object[] { "Pendiente", "Procesando", "Completado", "Cancelado" });
            cmbEstado.SelectedIndex = 0;
            ActualizarTotal();
        }

        private void FrmPedido_Load(object? sender, EventArgs e)
        {
            try
            {
                using var db = new AppDbContext();
                cmbCliente.DataSource = db.Clientes.OrderBy(cliente => cliente.Nombre).ToList();
                cmbCliente.DisplayMember = nameof(Cliente.Nombre);
                cmbCliente.ValueMember = nameof(Cliente.Id);

                cmbUsuario.DataSource = db.Usuarios.OrderBy(usuario => usuario.Nombre).ToList();
                cmbUsuario.DisplayMember = nameof(Usuario.Nombre);
                cmbUsuario.ValueMember = nameof(Usuario.Id);
                if (_usuarioActual != null)
                {
                    cmbUsuario.SelectedValue = _usuarioActual.Id;
                }

                CargarProductos(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del pedido: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos(AppDbContext db)
        {
            cmbProducto.DataSource = db.Productos
                .Where(producto => producto.Stock > 0)
                .OrderBy(producto => producto.Nombre)
                .ToList();
            cmbProducto.DisplayMember = nameof(Producto.Nombre);
            cmbProducto.ValueMember = nameof(Producto.Id);
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem is not Producto producto)
            {
                MessageBox.Show("No hay productos disponibles para agregar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad = (int)numCantidad.Value;
            var detalleExistente = _detalles.FirstOrDefault(detalle => detalle.IdProducto == producto.Id);
            int cantidadEnPedido = detalleExistente?.Cantidad ?? 0;
            if (cantidadEnPedido + cantidad > producto.Stock)
            {
                MessageBox.Show($"Solo hay {producto.Stock} unidades disponibles de {producto.Nombre}.", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (detalleExistente == null)
            {
                detalleExistente = new DetallePedido
                {
                    IdProducto = producto.Id,
                    Producto = producto,
                    PrecioUnitario = producto.Precio
                };
                _detalles.Add(detalleExistente);
            }

            detalleExistente.Cantidad += cantidad;
            detalleExistente.SubTotal = detalleExistente.Cantidad * detalleExistente.PrecioUnitario;
            ActualizarGrilla();
            numCantidad.Value = 1;
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvDetalles.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un producto de la lista para quitarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idProducto = Convert.ToInt32(dgvDetalles.CurrentRow.Cells[nameof(DetallePedido.IdProducto)].Value);
            _detalles.RemoveAll(detalle => detalle.IdProducto == idProducto);
            ActualizarGrilla();
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void numCostoEnvio_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarTotal();
        }

        private void ActualizarGrilla()
        {
            dgvDetalles.Rows.Clear();
            foreach (var detalle in _detalles)
            {
                dgvDetalles.Rows.Add(detalle.IdProducto, detalle.Producto.Nombre, detalle.Cantidad, detalle.PrecioUnitario, detalle.SubTotal);
            }

            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            decimal totalProductos = _detalles.Sum(detalle => detalle.SubTotal);
            lblTotal.Text = $"Total: {totalProductos + numCostoEnvio.Value:C2}";
        }

        private void LimpiarFormulario()
        {
            _detalles.Clear();
            ActualizarGrilla();
            txtId.Clear();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            numCantidad.Value = 1;
            numCostoEnvio.Value = 0;
            if (cmbEstado.Items.Count > 0)
            {
                cmbEstado.SelectedIndex = 0;
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (cmbCliente.SelectedItem is not Cliente cliente || cmbUsuario.SelectedItem is not Usuario usuario)
            {
                MessageBox.Show("Selecciona un cliente y un vendedor para el pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_detalles.Count == 0)
            {
                MessageBox.Show("Agrega al menos un producto al pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new AppDbContext();
                using var transaction = db.Database.BeginTransaction();
                var idsProductos = _detalles.Select(detalle => detalle.IdProducto).ToList();
                var productosActuales = db.Productos
                    .Where(producto => idsProductos.Contains(producto.Id))
                    .ToDictionary(producto => producto.Id);

                foreach (var detalle in _detalles)
                {
                    if (!productosActuales.TryGetValue(detalle.IdProducto, out var producto) || producto.Stock < detalle.Cantidad)
                    {
                        throw new InvalidOperationException($"El producto {detalle.Producto.Nombre} ya no tiene stock suficiente. Vuelve a cargar el pedido e intenta de nuevo.");
                    }

                    producto.Stock -= detalle.Cantidad;
                }

                var pedido = new Pedido
                {
                    IdCliente = cliente.Id,
                    IdUsuario = usuario.Id,
                    Fecha = DateTime.Now,
                    Estado = cmbEstado.SelectedItem?.ToString() ?? "Pendiente",
                    CostoEnvio = numCostoEnvio.Value,
                    Total = _detalles.Sum(detalle => detalle.SubTotal) + numCostoEnvio.Value,
                    DetallesPedidos = _detalles.Select(detalle => new DetallePedido
                    {
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        SubTotal = detalle.SubTotal
                    }).ToList()
                };

                db.Pedidos.Add(pedido);
                db.SaveChanges();
                transaction.Commit();

                MessageBox.Show($"Pedido #{pedido.Id} registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                txtId.Text = pedido.Id.ToString();
                txtFecha.Text = pedido.Fecha.ToString("dd/MM/yyyy HH:mm");
                CargarProductos(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo guardar el pedido: {ObtenerMensajeError(ex)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string ObtenerMensajeError(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }

        private void previewLabel_Click(object sender, EventArgs e)
        {

        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
