using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmEnvio : Form
    {
        private readonly EnvioService _envioService = new();
        private int _envioIdSeleccionado = 0;
        private int _idPedidoActual = 0;

        public FrmEnvio()
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
            this.Text = "Gestión de Envíos Contra Entrega - SivarPOS";

            // Configurar ComboBox de Filtro de Estado (arriba)
            if (cboFiltroEstado.Items.Count == 0)
            {
                cboFiltroEstado.Items.AddRange(new object[] { "Todos", "Pendiente", "En tránsito", "Entregado", "Cancelado" });
                cboFiltroEstado.SelectedIndex = 0; // Inicia por defecto en "Todos"
            }
            cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFiltroEstado.SelectedIndexChanged += CboFiltroEstado_SelectedIndexChanged;

            // Configurar ComboBox de Estado de Entrega (panel lateral)
            if (cboEstadoEntrega.Items.Count == 0)
            {
                cboEstadoEntrega.Items.AddRange(new object[] { "Pendiente", "En tránsito", "Entregado", "Cancelado" });
            }
            cboEstadoEntrega.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configuración del TextBox de Empresa de Transporte (Fijo para C807 Express)
            empresaTransporte.Text = "C807 Express";
            empresaTransporte.ReadOnly = true; // Impide la edición manteniendo la selección de texto

            // El número de guía se genera automáticamente, por lo que es de solo lectura
            txtGuiaTracking.ReadOnly = true;

            // Configurar DataGridView
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.MultiSelect = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.CellClick += DgvHistorial_CellClick;

            // Enlazar eventos de botones
            btnGuardar.Click += BtnGuardar_Click;

            // Si tienes un botón para cerrar o salir
            var btnCerrar = this.Controls.Find("btnCerrar", true).FirstOrDefault() as Button;
            if (btnCerrar != null)
            {
                btnCerrar.Click += (s, e) => Close();
            }
        }

        private void CargarFiltroEstadoInicial()
        {
            CargarEnvíosSegunFiltro();
        }

        private void CboFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEnvíosSegunFiltro();
        }

        private void CargarEnvíosSegunFiltro()
        {
            try
            {
                string filtroEstado = cboFiltroEstado.SelectedItem?.ToString() ?? "Todos";
                var todosLosEnvios = _envioService.ListarTodos();

                // Filtrar según el ComboBox superior
                var listaFiltrada = todosLosEnvios.Where(envio =>
                    filtroEstado == "Todos" || envio.Estado.Equals(filtroEstado, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                // Calcular el contador global de envíos activos/pendientes
                int pendientesGlobales = todosLosEnvios.Count(e =>
                    e.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase) ||
                    e.Estado.Equals("En tránsito", StringComparison.OrdinalIgnoreCase));

                if (lblContadorPendientes != null)
                {
                    lblContadorPendientes.Text = $"Envíos Pendientes: {pendientesGlobales}";
                }

                // Cargar datos en la grilla
                dgvHistorial.DataSource = null;
                dgvHistorial.DataSource = listaFiltrada;

                // Ocultar columnas de navegación complejas que puedan aparecer vacías
                if (dgvHistorial.Columns["Pedido"] != null) dgvHistorial.Columns["Pedido"].Visible = false;
                if (dgvHistorial.Columns["Direccion"] != null) dgvHistorial.Columns["Direccion"].Visible = false;

                if (listaFiltrada.Count > 0)
                {
                    dgvHistorial.Rows[0].Selected = true;
                    CargarDetallesEnvioDesdeFila(listaFiltrada[0]);
                }
                else
                {
                    _envioIdSeleccionado = 0;
                    _idPedidoActual = 0;
                    if (lblTotalValor != null) lblTotalValor.Text = "$0.00";
                    cboEstadoEntrega.SelectedIndex = 0;
                    empresaTransporte.Text = "C807 Express";
                    txtGuiaTracking.Clear();

                    cboEstadoEntrega.Enabled = false;
                    btnGuardar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar envíos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvHistorial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvHistorial.Rows[e.RowIndex].DataBoundItem is Envio envioSeleccionado)
            {
                CargarDetallesEnvioDesdeFila(envioSeleccionado);
            }
        }

        private void CargarDetallesEnvioDesdeFila(Envio envio)
        {
            _envioIdSeleccionado = envio.Id;
            _idPedidoActual = envio.IdPedido;

            cboEstadoEntrega.SelectedItem = envio.Estado;
            empresaTransporte.Text = !string.IsNullOrEmpty(envio.EmpresaTransporte) ? envio.EmpresaTransporte : "C807 Express";

            // Muestra la guía generada automáticamente (ej: C820261110094707)
            txtGuiaTracking.Text = envio.NumeroSeguimiento;

            if (envio.Pedido != null && lblTotalValor != null)
            {
                lblTotalValor.Text = $"${envio.Pedido.Total:N2}";
            }

            // Regla de seguridad: Si el envío ya fue Entregado o Cancelado, se bloquea la edición
            bool esEditable = !envio.Estado.Equals("Entregado", StringComparison.OrdinalIgnoreCase) &&
                              !envio.Estado.Equals("Cancelado", StringComparison.OrdinalIgnoreCase);

            cboEstadoEntrega.Enabled = esEditable;
            btnGuardar.Enabled = esEditable;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_envioIdSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un envío válido de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nuevoEstado = cboEstadoEntrega.SelectedItem?.ToString() ?? "Pendiente";
                string empresa = empresaTransporte.Text.Trim();

                // Actualizar estado mediante el servicio
                _envioService.ActualizarEstadoEnvio(_envioIdSeleccionado, nuevoEstado, empresa);

                MessageBox.Show("Envío actualizado correctamente.", "SivarPOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar la grilla manteniendo el filtro actual
                CargarEnvíosSegunFiltro();
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