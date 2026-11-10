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
        // Servicios para manejar operaciones relacionadas con pedidos
        private readonly PedidoService _servicio = new();
        private readonly BindingList<LineaPedido> _lineas = new();
        private int _pedidoId = 0; // 0 = Pedido nuevo
        private readonly Usuario? _usuarioActual;// Usuario autenticado que realiza el pedido

        // Constructor por defecto que llama al constructor principal con null
        public FrmPedido() : this(null) { }

        // Constructor principal que recibe el usuario autenticado
        public FrmPedido(Usuario? usuarioActual)
        {
            InitializeComponent();
            _usuarioActual = usuarioActual ?? Sesion.UsuarioActual;
        }

        // Evento que se ejecuta al cargar el formulario
        private void FrmPedido_Load(object sender, EventArgs e)
        {
            CargarCatalogos();
            cboProducto.SelectedIndexChanged += cboProducto_SelectedIndexChanged;
            dgvDetalle.DataSource = _lineas;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Aqui ocultamos las columnas que no queremos mostrar en el DataGridView
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

            // Cargar la imagen del producto seleccionado al iniciar el formulario
            cboProducto_SelectedIndexChanged(cboProducto, EventArgs.Empty);
        }

        // Carga los datos de clientes, usuarios y productos en los ComboBoxes correspondientes
        private void CargarCatalogos()
        {
            // Configuramos el ComboBox para que muestre el Correo en lugar del Nombre
            cboCliente.DisplayMember = "Correo"; // O "Email" según tu modelo de Cliente
            cboCliente.ValueMember = "Id";
            cboCliente.DataSource = _servicio.ListarClientes();

            // Habilitamos la búsqueda predictiva (autocompletar) por correo
            cboCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboCliente.AutoCompleteSource = AutoCompleteSource.ListItems;

            cboUsuario.DisplayMember = "Nombre";
            cboUsuario.ValueMember = "Id";
            cboUsuario.DataSource = _servicio.ListarUsuarios();
            cboUsuario.Enabled = false; // Bloqueado para reflejar únicamente al usuario autenticado

            cboProducto.DisplayMember = "Nombre";
            cboProducto.ValueMember = "Id";
            cboProducto.DataSource = _servicio.ListarProductosActivos();
        }

        // Carga el historial de pedidos en el DataGridView
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

        // Inicializa un nuevo pedido, reseteando los campos y la lista de líneas
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

            // Si hay un usuario autenticado se selecciona automáticamente en el ComboBox de usuarios
            if (_usuarioActual != null)
            {
                cboUsuario.SelectedValue = _usuarioActual.Id;
            }
        }

        // Actualiza el total del pedido sumando los subtotales de cada línea
        private void ActualizarTotal()
        {
            lblTotal.Text = $"Total: ${_lineas.Sum(l => l.Subtotal):N2}";
        }

        // Evento que se ejecuta al hacer clic en el botón "Agregar" para añadir un producto al pedido
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.SelectedItem is not Producto productoSeleccionado) return;
            int cantidadSolicitada = (int)nudCantidad.Value;

            // Consultar el stock real y actualizado desde la base de datos
            using var ctx = new AppDbContext();
            var productoDb = ctx.Productos.AsNoTracking().FirstOrDefault(p => p.Id == productoSeleccionado.Id);

            if (productoDb == null)
            {
                MessageBox.Show("El producto ya no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el producto ya está agregado en el carrito temporal
            LineaPedido? existente = _lineas.FirstOrDefault(l => l.ProductoId == productoDb.Id);
            int cantidadTotalEnCarrito = (existente != null ? existente.Cantidad : 0) + cantidadSolicitada;

            // Validar que la cantidad acumulada no supere el stock de la base de datos
            if (cantidadTotalEnCarrito > productoDb.Stock)
            {
                MessageBox.Show(
                    $"No hay suficiente stock disponible.\n\n" +
                    $"Stock actual en BD: {productoDb.Stock}\n" +
                    $"Ya tienes en el carrito: {(existente != null ? existente.Cantidad : 0)}\n" +
                    $"Intentas agregar: {cantidadSolicitada}",
                    "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si el producto ya existe en la lista, simplemente actualizamos la cantidad
            if (existente != null)
            {
                existente.Cantidad += cantidadSolicitada;
                _lineas.ResetBindings();
            }
            else
            {
                _lineas.Add(new LineaPedido
                {
                    ProductoId = productoDb.Id,
                    Producto = productoDb.Nombre,
                    Cantidad = cantidadSolicitada,
                    PrecioUnitario = productoDb.Precio
                });
            }

            nudCantidad.Value = 1;// Reinicia la cantidad a 1 después de agregar
            ActualizarTotal();
        }

        // Evento que se ejecuta al hacer clic en el botón "Quitar" para eliminar un producto del pedido
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow?.DataBoundItem is LineaPedido linea)
            {
                _lineas.Remove(linea);
                ActualizarTotal();
            }
        }

        // Evento que se ejecuta al hacer clic en el botón "Nuevo" para iniciar un nuevo pedido
        private void btnNueva_Click(object sender, EventArgs e) => NuevoPedido();

        // Evento que se ejecuta al hacer clic en el botón "Guardar" para registrar o actualizar el pedido
        // Evento que se ejecuta al hacer clic en el botón "Guardar" para registrar o actualizar el pedido
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que haya al menos un cliente y usuario seleccionados
            if (cboCliente.SelectedValue is not int clienteId ||
                cboUsuario.SelectedValue is not int usuarioId)
            {
                MessageBox.Show("Selecciona el cliente y el usuario.", "VentasOnline", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_lineas.Count == 0)
            {
                MessageBox.Show("Agrega al menos un producto al pedido.", "VentasOnline", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Si el pedido es nuevo, se registra con su envío automático
                if (_pedidoId == 0)
                {
                    // Buscamos la dirección del cliente directamente en la base de datos
                    using var ctx = new AppDbContext();
                    var direccionCliente = ctx.Direcciones.FirstOrDefault(d => d.IdCliente == clienteId);

                    if (direccionCliente == null)
                    {
                        MessageBox.Show("El cliente seleccionado no tiene una dirección registrada. Por favor, asígnale una dirección antes de continuar.",
                            "Dirección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Calcular el subtotal exacto de los productos en el carrito temporal
                    decimal subtotalProductos = _lineas.Sum(l => l.Subtotal);

                    // Registrar un nuevo pedido pasando el ID de la dirección y asegurando el total de la mercancía
                    // (Nota: Si tu método Registrar acepta el total como parámetro, o si lo calcula internamente en el servicio, 
                    // asegúrate de que el servicio sume 'subtotalProductos' y le sume los 8.00 del CostoEnvio).
                    int id = _servicio.Registrar(clienteId, usuarioId, direccionCliente.Id, _lineas);

                    MessageBox.Show($"Pedido #{id} registrado y guía de C807 Express generada exitosamente.", "VentasOnline",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualizar un pedido existente
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

        // Evento que se ejecuta al hacer clic en una celda del DataGridView de pedidos para cargar los detalles del pedido seleccionado
        private void dgvPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evita que se ejecute si se hace clic en el encabezado de la columna
            if (e.RowIndex < 0) return;
            int id = Convert.ToInt32(dgvPedidos.Rows[e.RowIndex].Cells["Id"].Value);

            // Cargar el pedido seleccionado desde la base de datos
            Pedido? pedido = _servicio.ObtenerConDetalle(id);
            if (pedido == null) return;

            _pedidoId = pedido.Id;
            cboCliente.SelectedValue = pedido.IdCliente;
            cboUsuario.SelectedValue = pedido.IdUsuario;

            // Limpiar y cargar las líneas del pedido en el BindingList
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
            // Actualizar la interfaz para reflejar que se está editando un pedido existente
            lblVenta.Text = $"Editando pedido #{pedido.Id}";
            btnEliminar.Enabled = true;
            ActualizarTotal();
        }

        // Carga la imagen y actualiza el stock al cambiar la selección en el ComboBox de productos
        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProducto.SelectedItem is Producto producto)
            {
                // Muestra el stock disponible en el Label
                lblStock.Text = $"Stock: {producto.Stock}";

                // Cargar la imagen del producto desde la carpeta "Images" si existe
                if (!string.IsNullOrEmpty(producto.ImagenUrl))
                {
                    string rutaBase = Application.StartupPath;
                    string rutaCompleta = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images", producto.ImagenUrl));

                    // Verifica si el archivo de imagen existe antes de intentar cargarlo
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

            // Si no hay producto seleccionado o no se puede cargar la imagen, se limpia la imagen y se muestra un stock vacío
            lblStock.Text = "Stock: -";
            LimpiarImagenProducto();
        }

        // Método auxiliar para limpiar la imagen del PictureBox y liberar recursos
        private void LimpiarImagenProducto()
        {
            if (pictureBoxProducto.Image != null)
            {
                pictureBoxProducto.Image.Dispose();
                pictureBoxProducto.Image = null;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}