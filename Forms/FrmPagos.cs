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
            this.Text = "Liquidación de Pagos Contra Entrega - SivarPOS";

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

            var btnCerrar = this.Controls.Find("btnCerrar", true).FirstOrDefault() as Button;
            if (btnCerrar != null)
            {
                btnCerrar.Click += (s, e) => Close();
            }
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

                var listaPagosProyectados = new List<object>();
                int pendientesGlobales = 0;

                foreach (var pedido in todosLosPedidos)
                {
                    var pagosDelPedido = _pagoService.ObtenerPagosPorPedido(pedido.Id);
                    foreach (var pago in pagosDelPedido)
                    {
                        if (pago.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                        {
                            pendientesGlobales++;
                        }

                        bool cumple = filtroEstado == "Todos" || pago.Estado.Equals(filtroEstado, StringComparison.OrdinalIgnoreCase);
                        if (cumple)
                        {
                            // 💡 Proyectamos los datos para la tabla incluyendo el costo de envío y el total consolidado
                            listaPagosProyectados.Add(new
                            {
                                pago.Id,
                                pago.IdPedido,
                                EstadoPago = pago.Estado,
                                MetodoPago = pago.Metodo,
                                SubtotalProductos = pedido.Total,
                                CostoEnvio = pedido.CostoEnvio,
                                Total = pedido.Total + pedido.CostoEnvio, // Total limpio con envío sumado
                                pago.FechaPago,
                                ObjetoPagoOriginal = pago
                            });
                        }
                    }
                }

                if (lblContadorPendientes != null)
                {
                    lblContadorPendientes.Text = $"Pagos Pendientes: {pendientesGlobales}";
                }

                // Cargar datos en la grilla
                dgvHistorial.DataSource = null;
                dgvHistorial.DataSource = listaPagosProyectados;

                // Ocultar columnas técnicas
                if (dgvHistorial.Columns["IdPedido"] != null) dgvHistorial.Columns["IdPedido"].Visible = false;
                if (dgvHistorial.Columns["ObjetoPagoOriginal"] != null) dgvHistorial.Columns["ObjetoPagoOriginal"].Visible = false;

                // Dar formato de moneda a las columnas numéricas
                if (dgvHistorial.Columns["SubtotalProductos"] != null)
                    dgvHistorial.Columns["SubtotalProductos"].DefaultCellStyle.Format = "N2";
                if (dgvHistorial.Columns["CostoEnvio"] != null)
                    dgvHistorial.Columns["CostoEnvio"].DefaultCellStyle.Format = "N2";
                if (dgvHistorial.Columns["Total"] != null)
                    dgvHistorial.Columns["Total"].DefaultCellStyle.Format = "N2";

                if (listaPagosProyectados.Count > 0)
                {
                    dgvHistorial.Rows[0].Selected = true;
                    var primerItem = dgvHistorial.Rows[0].DataBoundItem;
                    var pagoReal = (Pago)primerItem.GetType().GetProperty("ObjetoPagoOriginal").GetValue(primerItem);
                    CargarDetallesPagoDesdeFila(pagoReal);
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

            var itemSeleccionado = dgvHistorial.Rows[e.RowIndex].DataBoundItem;
            if (itemSeleccionado != null)
            {
                var pagoReal = (Pago)itemSeleccionado.GetType().GetProperty("ObjetoPagoOriginal").GetValue(itemSeleccionado);
                if (pagoReal != null)
                {
                    CargarDetallesPagoDesdeFila(pagoReal);
                }
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

            // Buscar el pedido asociado para mostrar únicamente el total limpio con el envío sumado
            var pedido = _pedidoService.ListarPedidos().FirstOrDefault(p => p.Id == pago.IdPedido);
            if (pedido != null && lblTotalValor != null)
            {
                decimal totalConEnvio = pedido.Total + pedido.CostoEnvio;
                lblTotalValor.Text = $"${totalConEnvio:N2}"; // 💡 Únicamente el total sin textos adicionales
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

                // El monto total a guardar incluye productos + costo de envío
                decimal montoFinalConEnvio = pedidoActual.Total + pedidoActual.CostoEnvio;

                _pagoService.ActualizarPago(_pagoIdSeleccionado, estado, metodo, montoFinalConEnvio);

                MessageBox.Show("Pago y liquidación actualizados correctamente.", "SivarPOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar grilla y contadores manteniendo el filtro actual
                CargarPagosSegunFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}