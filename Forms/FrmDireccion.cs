using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmDireccion : Form
    {
        // Servicios y variables para manejar operaciones de direcciones
        private readonly DireccionService _direccionService = new();
        private int _idClienteRecibido;
        private int _idDireccionSeleccionada = 0;
        private bool _cargandoDatos = true;

        // Constructor que recibe el ID del cliente para gestionar sus direcciones
        public FrmDireccion(int idCliente)
        {
            InitializeComponent();
            _idClienteRecibido = idCliente;
            CargarInformacionClienteInicial();
            ConfigurarFormulario();
        }

        // Método para cargar la información del cliente en el formulario al iniciar
        private void CargarInformacionClienteInicial()
        {
            try
            {
                // Usamos AsNoTracking para mejorar el rendimiento al solo leer datos
                using var ctx = new AppDbContext();
                var cliente = ctx.Clientes.AsNoTracking().FirstOrDefault(c => c.Id == _idClienteRecibido);
                // Si encontramos el cliente, mostramos su nombre en el TextBox correspondiente
                if (cliente != null && txtCliente != null)
                {
                    txtCliente.Text = cliente.Nombre;
                }
                // Si no encontramos el cliente, aseguramos que el TextBox esté vacío
                else if (txtCliente != null)
                {
                    txtCliente.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                if (txtCliente != null) txtCliente.Text = string.Empty;
            }
        }

        // Método para configurar las propiedades y eventos del formulario y sus controles
        private void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Gestión General de Direcciones - SivarPOS";

            if (txtCliente != null) txtCliente.ReadOnly = true;

            // Configuración del ComboBox de departamentos con los valores predefinidos
            if (cboDepartamento.Items.Count == 0)
            {
                cboDepartamento.Items.AddRange(new object[] {
                    "San Salvador", "La Libertad", "Santa Ana", "San Miguel",
                    "Sonsonate", "Usulután", "Ahuachapán", "La Paz",
                    "Cuscatlán", "La Unión", "Chalatenango", "Morazán", "San Vicente", "Cabañas"
                });
            }
            cboDepartamento.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configuración del DataGridView para mostrar las direcciones
            dgvDirecciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDirecciones.MultiSelect = false;
            dgvDirecciones.ReadOnly = true;
            dgvDirecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDirecciones.CellClick += DgvDirecciones_CellClick;

            btnGuardar.Click += BtnGuardar_Click;
            btnEliminar.Click += BtnEliminar_Click;

            // Suscribir el evento de búsqueda en tiempo real por nombre de cliente
            if (txtBusqueda != null)
            {
                txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            }

            var btnCancelarLocal = this.Controls.Find("btnCancelar", true).FirstOrDefault() as Button;
            if (btnCancelarLocal != null)
            {
                btnCancelarLocal.Click += (s, e) => Close();
            }
        }

        private void FrmDireccion_Load(object? sender, EventArgs e)
        {
            CargarGrillaDirecciones();

            // Validar si el cliente ya tiene una dirección registrada al entrar
            CargarDireccionExistenteDelCliente();

            _cargandoDatos = false;
        }

        // Método para verificar y cargar la dirección previa del cliente (Relación 1:1)
        private void CargarDireccionExistenteDelCliente()
        {
            try
            {
                using var ctx = new AppDbContext();
                var direccionExistente = ctx.Direcciones
                    .AsNoTracking()
                    .FirstOrDefault(d => d.IdCliente == _idClienteRecibido);

                if (direccionExistente != null)
                {
                    // Si ya tiene dirección, cargamos sus datos para edición directa
                    _idDireccionSeleccionada = direccionExistente.Id;
                    cboDepartamento.SelectedItem = direccionExistente.Departamento;
                    txtUbicacion.Text = direccionExistente.Ubicacion ?? string.Empty;
                    btnGuardar.Text = "Actualizar";
                }
                else
                {
                    // Si no tiene, flujo normal de creación
                    RestablecerModoCreacion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar la dirección del cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para cargar la grilla filtrando opcionalmente por el nombre del cliente
        private void CargarGrillaDirecciones(string filtroNombreCliente = "")
        {
            try
            {
                using var ctx = new AppDbContext();

                var query = ctx.Direcciones
                               .AsNoTracking()
                               .Include(d => d.Cliente)
                               .AsQueryable();

                // Si hay texto en el buscador, filtramos por el nombre del cliente
                if (!string.IsNullOrWhiteSpace(filtroNombreCliente))
                {
                    query = query.Where(d => d.Cliente != null && d.Cliente.Nombre.Contains(filtroNombreCliente));
                }

                var direcciones = query
                    .Select(d => new
                    {
                        d.Id,
                        d.IdCliente,
                        ClienteNombre = d.Cliente != null ? d.Cliente.Nombre : "Sin Cliente",
                        d.Departamento,
                        d.Ubicacion
                    })
                    .ToList();

                dgvDirecciones.DataSource = null;
                dgvDirecciones.DataSource = direcciones;

                if (dgvDirecciones.Columns["Id"] != null) dgvDirecciones.Columns["Id"].Visible = false;
                if (dgvDirecciones.Columns["IdCliente"] != null) dgvDirecciones.Columns["IdCliente"].Visible = false;

                if (dgvDirecciones.Columns["ClienteNombre"] != null) dgvDirecciones.Columns["ClienteNombre"].HeaderText = "Cliente";
                if (dgvDirecciones.Columns["Departamento"] != null) dgvDirecciones.Columns["Departamento"].HeaderText = "Departamento";
                if (dgvDirecciones.Columns["Ubicacion"] != null) dgvDirecciones.Columns["Ubicacion"].HeaderText = "Ubicación Exacta";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las direcciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta al escribir en el txtBusqueda
        private void txtBusqueda_TextChanged(object? sender, EventArgs e)
        {
            if (_cargandoDatos) return;
            string textoFiltro = txtBusqueda.Text.Trim();
            CargarGrillaDirecciones(textoFiltro);
        }

        private void DgvDirecciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvDirecciones.Rows[e.RowIndex];

            if (fila.Cells["Id"].Value is int idDir)
            {
                _idDireccionSeleccionada = idDir;

                if (fila.Cells["IdCliente"].Value is int idCli)
                {
                    _idClienteRecibido = idCli;
                }

                cboDepartamento.SelectedItem = fila.Cells["Departamento"].Value?.ToString();
                txtUbicacion.Text = fila.Cells["Ubicacion"].Value?.ToString() ?? string.Empty;
                btnGuardar.Text = "Actualizar";

                if (txtCliente != null)
                {
                    txtCliente.Text = fila.Cells["ClienteNombre"].Value?.ToString() ?? string.Empty;
                }
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                string departamento = cboDepartamento.SelectedItem?.ToString()?.Trim() ?? string.Empty;
                string ubicacion = txtUbicacion.Text.Trim();

                if (string.IsNullOrEmpty(departamento))
                {
                    MessageBox.Show("Debe seleccionar un departamento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboDepartamento.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ubicacion))
                {
                    MessageBox.Show("La ubicación exacta es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUbicacion.Focus();
                    return;
                }

                if (_idDireccionSeleccionada == 0)
                {
                    var nuevaDireccion = new Direccion
                    {
                        IdCliente = _idClienteRecibido,
                        Departamento = departamento,
                        Ubicacion = ubicacion
                    };

                    _direccionService.Crear(nuevaDireccion);
                    MessageBox.Show("Dirección agregada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var direccionActualizada = new Direccion
                    {
                        Id = _idDireccionSeleccionada,
                        IdCliente = _idClienteRecibido,
                        Departamento = departamento,
                        Ubicacion = ubicacion
                    };

                    _direccionService.Actualizar(direccionActualizada);
                    MessageBox.Show("Dirección actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarGrillaDirecciones();
                if (txtBusqueda != null) txtBusqueda.Clear();
                CargarDireccionExistenteDelCliente(); // Refrescar estado actual
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idDireccionSeleccionada == 0)
            {
                MessageBox.Show("Seleccione de la grilla la dirección que desea eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show("¿Está seguro de eliminar esta dirección?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _direccionService.Eliminar(_idDireccionSeleccionada);
                    MessageBox.Show("Dirección eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarGrillaDirecciones();
                    if (txtBusqueda != null) txtBusqueda.Clear();
                    RestablecerModoCreacion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void RestablecerModoCreacion()
        {
            _idDireccionSeleccionada = 0;
            dgvDirecciones.ClearSelection();
            dgvDirecciones.CurrentCell = null;

            cboDepartamento.SelectedIndex = -1;
            cboDepartamento.Text = string.Empty;

            txtUbicacion.Clear();
            btnGuardar.Text = "Guardar";
            cboDepartamento.Focus();
        }
    }
}