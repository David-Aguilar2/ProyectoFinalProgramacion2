using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VentasOnline.Data;
using VentasOnline.Dtos;
using VentasOnline.Models;
using VentasOnline.Reportes;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmEnvio : Form
    {
        private readonly EnvioService _envioService = new();
        // Servicio para consultar la información completa del envío/pedido para el reporte PDF
        private readonly EnvioReporteService _envioReporteService = new();
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

            // 💡 Enlazar evento para la generación del reporte PDF
            if (btnGenerarReporte != null)
            {
                btnGenerarReporte.Click += BtnGenerarReporte_Click;
                btnGenerarReporte.Enabled = false; // Deshabilitado por defecto hasta que se seleccione un envío
            }

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

                    // Deshabilitar botón de reporte si no hay envíos en el filtro actual
                    if (btnGenerarReporte != null) btnGenerarReporte.Enabled = false;
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

            // Habilitar el botón de reporte al seleccionar válidamente un envío de la grilla
            if (btnGenerarReporte != null)
            {
                btnGenerarReporte.Enabled = _envioIdSeleccionado > 0;
            }
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
                string numeroGuia = txtGuiaTracking.Text.Trim();

                // Validación: El estado debe pertenecer estrictamente a la lista permitida
                if (nuevoEstado != "Pendiente" && nuevoEstado != "En tránsito" && nuevoEstado != "Entregado" && nuevoEstado != "Cancelado")
                {
                    MessageBox.Show("El estado de envío seleccionado no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación: La empresa de transporte no puede estar vacía
                if (string.IsNullOrWhiteSpace(empresa))
                {
                    MessageBox.Show("La empresa de transporte es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación: Longitud máxima de la empresa de transporte (100 caracteres)
                if (empresa.Length > 100)
                {
                    MessageBox.Show("La empresa de transporte no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación: El número de seguimiento/guía no puede estar vacío
                if (string.IsNullOrWhiteSpace(numeroGuia))
                {
                    MessageBox.Show("El número de seguimiento (guía) es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación: Longitud máxima del número de seguimiento (50 caracteres)
                if (numeroGuia.Length > 50)
                {
                    MessageBox.Show("El número de seguimiento no puede superar los 50 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var ctxDb = new AppDbContext();

                // Validación: Verificar que el envío seleccionado siga existiendo en la base de datos
                var envioActualEnDb = ctxDb.Envios.Include(e => e.Pedido).ThenInclude(p => p.Pagos).FirstOrDefault(e => e.Id == _envioIdSeleccionado);
                if (envioActualEnDb == null)
                {
                    MessageBox.Show("El envío seleccionado ya no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CargarEnvíosSegunFiltro();
                    return;
                }

                // Validación: Verificar transiciones de estado permitidas
                string estadoActual = envioActualEnDb.Estado;

                // Evitar Entregado → Pendiente
                if (estadoActual.Equals("Entregado", StringComparison.OrdinalIgnoreCase) && nuevoEstado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("No se puede cambiar un envío entregado a estado pendiente.", "Restricción de transición", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // Evitar Cancelado → Entregado
                if (estadoActual.Equals("Cancelado", StringComparison.OrdinalIgnoreCase) && nuevoEstado.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("No se puede entregar un envío que ha sido cancelado.", "Restricción de transición", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // Validación: Verificar que el pedido asociado exista
                if (envioActualEnDb.Pedido == null)
                {
                    MessageBox.Show("El pedido asociado a este envío no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validación: Verificar unicidad del número de seguimiento si fue modificado
                if (!envioActualEnDb.NumeroSeguimiento.Equals(numeroGuia, StringComparison.OrdinalIgnoreCase))
                {
                    bool guiaDuplicada = ctxDb.Envios.Any(e => e.NumeroSeguimiento.ToLower() == numeroGuia.ToLower() && e.Id != _envioIdSeleccionado);
                    if (guiaDuplicada)
                    {
                        MessageBox.Show("Ya existe otro envío registrado con este mismo número de seguimiento (guía).", "Guía duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

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

        // Genera y exporta el comprobante de envío y detalle del pedido en formato PDF utilizando QuestPDF.
        private void BtnGenerarReporte_Click(object? sender, EventArgs e)
        {
            if (_envioIdSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccione un envío de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Obtener los detalles completos del envío, cliente, dirección y productos mediante el servicio de reportes
            EnvioDetalleReporteDto? dto = _envioReporteService.ObtenerDetalleEnvioParaReporte(_envioIdSeleccionado);

            if (dto == null)
            {
                MessageBox.Show("No se encontraron los detalles completos del envío o del pedido asociado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Mostrar diálogo para seleccionar la ubicación de guardado del PDF
            using var dialogo = new SaveFileDialog
            {
                Filter = "Archivo PDF (*.pdf)|*.pdf",
                FileName = $"ComprobanteEnvio_{dto.IdEnvio}_Pedido_{dto.IdPedido}.pdf"
            };

            if (dialogo.ShowDialog() != DialogResult.OK) return;

            try
            {
                // 3. Obtener el nombre del usuario logueado en la sesión activa si está disponible
                string usuarioActual = Sesion.UsuarioActual != null ? Sesion.UsuarioActual.Nombre : "Administrador";

                // 4. Instanciar y generar el documento PDF con QuestPDF
                var documento = new EnvioComprobanteDocument(dto, usuarioActual);
                documento.GeneratePdf(dialogo.FileName);

                // 5. Abrir el archivo PDF automáticamente con el visor predeterminado del sistema operativo
                Process.Start(new ProcessStartInfo(dialogo.FileName) { UseShellExecute = true });
            }
            catch (IOException)
            {
                MessageBox.Show("No se pudo guardar el archivo. Verifique si el PDF está abierto en otro programa.", "SivarPOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el comprobante PDF:\n{ex.Message}", "SivarPOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}