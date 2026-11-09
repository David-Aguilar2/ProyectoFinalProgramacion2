using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmPagos : Form
    {
        private readonly PagoService _pagoService = new();
        private readonly PedidoService _pedidoService = new();

        private int _pagoIdSeleccionado = 0;
        private int _idPedidoActual = 0;

        public FrmPagos()
        {
            InitializeComponent();
            ConfigurarFormularioGrafico();
            CargarFiltroEstadoInicial();
        }

        private void ConfigurarFormularioGrafico()
        {
            // Configuración inicial de la ventana
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Configurar ComboBox de Filtro de Estado
            if (cboFiltroEstado.Items.Count == 0)
            {
                cboFiltroEstado.Items.AddRange(new object[] { "Todos", "Pendiente", "Completado", "Cancelado" });
                cboFiltroEstado.SelectedIndex = 0; // Inicia por defecto en "Todos"
            }
            cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFiltroEstado.SelectedIndexChanged += CboFiltroEstado_SelectedIndexChanged;

            // Configurar ComboBox de Nuevo Estado
            if (cboEstado.Items.Count == 0)
            {
                cboEstado.Items.AddRange(new object[] { "Pendiente", "Completado", "Cancelado" });
            }
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.SelectedIndexChanged += CboEstado_SelectedIndexChanged;

            // Configurar ComboBox de Método de Pago
            if (cboMetodo.Items.Count == 0)
            {
                cboMetodo.Items.AddRange(new object[] { "Por definir", "Efectivo", "Tarjeta", "Transferencia" });
            }
            cboMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMetodo.Enabled = false;

            // Configurar DataGridView
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.MultiSelect = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.CellClick += DgvHistorial_CellClick;

            // Eventos de botones
            btnGuardar.Click += BtnGuardar_Click;
        }

        private void CargarFiltroEstadoInicial()
        {
            CargarPagosSegunFiltro();
        }

        private void CboFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPagosSegunFiltro();
        }

        private void CargarPagosSegunFiltro()
        {
            try
            {
                string filtroEstado = cboFiltroEstado.SelectedItem?.ToString() ?? "Todos";
                var todosLosPedidos = _pedidoService.ListarPedidos();

                var listaPagosFiltrados = new List<Pago>();

                foreach (var pedido in todosLosPedidos)
                {
                    var pagosDelPedido = _pagoService.ObtenerPagosPorPedido(pedido.Id);
                    foreach (var pago in pagosDelPedido)
                    {
                        bool cumple = filtroEstado == "Todos" || pago.Estado.Equals(filtroEstado, StringComparison.OrdinalIgnoreCase);
                        if (cumple)
                        {
                            listaPagosFiltrados.Add(pago);
                        }
                    }
                }

                // Calcular el total global de pendientes para la etiqueta de conteo
                int pendientesGlobales = 0;
                foreach (var pedido in todosLosPedidos)
                {
                    pendientesGlobales += _pagoService.ObtenerPagosPorPedido(pedido.Id).Count(p => p.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase));
                }

                if (lblContadorPendientes != null)
                {
                    lblContadorPendientes.Text = $"Pagos Pendientes: {pendientesGlobales}";
                }

                // Cargar datos en la grilla
                dgvHistorial.DataSource = null;
                dgvHistorial.DataSource = listaPagosFiltrados;

                // Ocultar la columna de navegación "Pedido" que aparece vacía
                if (dgvHistorial.Columns["Pedido"] != null)
                {
                    dgvHistorial.Columns["Pedido"].Visible = false;
                }

                if (listaPagosFiltrados.Count > 0)
                {
                    dgvHistorial.Rows[0].Selected = true;
                    CargarDetallesPagoDesdeFila(listaPagosFiltrados[0]);
                }
                else
                {
                    _pagoIdSeleccionado = 0;
                    _idPedidoActual = 0;
                    if (lblTotalValor != null) lblTotalValor.Text = "$0.00";
                    cboEstado.SelectedIndex = 0;
                    cboMetodo.SelectedItem = "Por definir";
                    cboEstado.Enabled = false;
                    cboMetodo.Enabled = false;
                    btnGuardar.Enabled = false;
                    if (lblFechaLabel != null) lblFechaLabel.Text = "Fecha de pago: Sin fecha registrada";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar los pagos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvHistorial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvHistorial.Rows[e.RowIndex].DataBoundItem is Pago pagoSeleccionado)
            {
                CargarDetallesPagoDesdeFila(pagoSeleccionado);
            }
        }

        private void CargarDetallesPagoDesdeFila(Pago pago)
        {
            _pagoIdSeleccionado = pago.Id;
            _idPedidoActual = pago.IdPedido;
            cboEstado.SelectedItem = pago.Estado;
            cboMetodo.SelectedItem = pago.Metodo;

            if (lblFechaLabel != null)
            {
                if (pago.FechaPago.HasValue)
                {
                    lblFechaLabel.Text = $"Fecha de pago: {pago.FechaPago.Value:dd/MM/yyyy HH:mm}";
                }
                else
                {
                    lblFechaLabel.Text = "Fecha de pago: Sin fecha registrada";
                }
            }

            // Buscar el total del pedido asociado para mostrarlo fijo
            var pedido = _pedidoService.ListarPedidos().FirstOrDefault(p => p.Id == pago.IdPedido);
            if (pedido != null && lblTotalValor != null)
            {
                lblTotalValor.Text = $"${pedido.Total:N2}";
            }

            // Regla de seguridad: Si ya está Completado o Cancelado, bloqueamos la edición
            bool esEditable = pago.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase);
            cboEstado.Enabled = esEditable;
            cboMetodo.Enabled = esEditable && (cboEstado.SelectedItem?.ToString() == "Completado");
            btnGuardar.Enabled = esEditable;
        }

        private void CboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estadoSeleccionado = cboEstado.SelectedItem?.ToString() ?? "Pendiente";

            if (estadoSeleccionado == "Pendiente" || estadoSeleccionado == "Cancelado")
            {
                cboMetodo.SelectedItem = "Por definir";
                cboMetodo.Enabled = false;
            }
            else if (estadoSeleccionado == "Completado")
            {
                cboMetodo.Enabled = true;
                if (cboMetodo.SelectedItem?.ToString() == "Por definir")
                {
                    cboMetodo.SelectedIndex = 1; // Selecciona "Efectivo" por defecto al habilitarse
                }
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pagoIdSeleccionado == 0 || _idPedidoActual == 0)
                {
                    MessageBox.Show("Seleccione un registro de pago válido de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string estado = cboEstado.SelectedItem?.ToString() ?? "Pendiente";
                string metodo = cboMetodo.SelectedItem?.ToString() ?? "Por definir";

                // Validación: No permitir guardar "Por definir" si el estado es Completado
                if (estado == "Completado" && (string.IsNullOrWhiteSpace(metodo) || metodo == "Por definir"))
                {
                    MessageBox.Show("Debe seleccionar un método de pago válido para un pago completado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var pedidoActual = _pedidoService.ListarPedidos().FirstOrDefault(p => p.Id == _idPedidoActual);
                if (pedidoActual == null)
                {
                    MessageBox.Show("No se encontró el pedido asociado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal montoFijo = pedidoActual.Total;

                _pagoService.ActualizarPago(_pagoIdSeleccionado, estado, metodo, montoFijo);

                MessageBox.Show("Pago actualizado correctamente.", "SivarPOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar grilla y contadores manteniendo el filtro actual
                CargarPagosSegunFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}