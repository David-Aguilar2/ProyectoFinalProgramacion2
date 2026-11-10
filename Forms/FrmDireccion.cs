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

            // Validación: El identificador del cliente debe ser mayor que cero
            if (idCliente <= 0)
            {
                MessageBox.Show("El identificador del cliente no es válido.", "Error de inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(Close));
                return;
            }

            _idClienteRecibido = idCliente;

            // Validación: Verificar que el cliente recibido exista realmente en la base de datos
            using var ctxCheck = new AppDbContext();
            bool clienteExiste = ctxCheck.Clientes.Any(c => c.Id == _idClienteRecibido);
            if (!clienteExiste)
            {
                MessageBox.Show("El cliente especificado no existe en la base de datos.", "Error de inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(Close));
                return;
            }

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

                // Validación: Departamento seleccionado obligatorio y válido dentro de la lista permitida
                if (string.IsNullOrEmpty(departamento) || !cboDepartamento.Items.Contains(departamento))
                {
                    MessageBox.Show("Debe seleccionar un departamento válido de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboDepartamento.Focus();
                    return;
                }

                // Validación: Longitud máxima del departamento (100 caracteres según el modelo)
                if (departamento.Length > 100)
                {
                    MessageBox.Show("El departamento seleccionado supera la longitud permitida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación: La ubicación exacta no puede estar vacía ni contener únicamente espacios
                if (string.IsNullOrWhiteSpace(ubicacion))
                {
                    MessageBox.Show("La ubicación exacta es obligatoria y no puede contener únicamente espacios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUbicacion.Focus();
                    return;
                }

                // Validación: Longitud mínima razonable de la ubicación (al menos 5 caracteres)
                if (ubicacion.Length < 5)
                {
                    MessageBox.Show("La ubicación exacta es demasiado corta. Debe tener al menos 5 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUbicacion.Focus();
                    return;
                }

                // Validación: Longitud máxima de la ubicación (255 caracteres según el modelo)
                if (ubicacion.Length > 255)
                {
                    MessageBox.Show("La ubicación exacta no puede superar los 255 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUbicacion.Focus();
                    return;
                }

                using var ctxDb = new AppDbContext();

                // Validar que el cliente receptor todavía exista
                bool clienteAunExiste = ctxDb.Clientes.Any(c => c.Id == _idClienteRecibido);
                if (!clienteAunExiste)
                {
                    MessageBox.Show("El cliente asociado ya no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                if (_idDireccionSeleccionada == 0)
                {
                    // Control de condición de carrera: Verificar que el cliente no haya registrado una dirección simultáneamente (Regla 1:1)
                    bool yaTieneDireccion = ctxDb.Direcciones.Any(d => d.IdCliente == _idClienteRecibido);
                    if (yaTieneDireccion)
                    {
                        MessageBox.Show("El cliente ya cuenta con una dirección registrada. Solo se permite una dirección por cliente.", "Restricción 1:1", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CargarDireccionExistenteDelCliente();
                        return;
                    }

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
                    // Validar que la dirección pertenezca realmente al cliente seleccionado antes de actualizarla
                    var direccionEnDb = ctxDb.Direcciones.Find(_idDireccionSeleccionada);
                    if (direccionEnDb == null)
                    {
                        MessageBox.Show("La dirección seleccionada ya no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CargarGrillaDirecciones();
                        RestablecerModoCreacion();
                        return;
                    }

                    if (direccionEnDb.IdCliente != _idClienteRecibido)
                    {
                        MessageBox.Show("La dirección seleccionada no pertenece al cliente actual.", "Restricción de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    // Evitar modificar una dirección utilizada por envíos ya entregados
                    bool enviosEntregadosAsociados = ctxDb.Envios.Any(e => e.IdDireccion == _idDireccionSeleccionada && e.Estado.Equals("Entregado", StringComparison.OrdinalIgnoreCase));
                    if (enviosEntregadosAsociados)
                    {
                        MessageBox.Show("No se puede modificar la dirección porque está asociada a un envío que ya ha sido entregado.", "Restricción de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

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

            using var ctxDb = new AppDbContext();

            // Validar que la dirección exista
            var direccionAEliminar = ctxDb.Direcciones.Find(_idDireccionSeleccionada);
            if (direccionAEliminar == null)
            {
                MessageBox.Show("La dirección seleccionada ya no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarGrillaDirecciones();
                RestablecerModoCreacion();
                return;
            }

            // Validar que la dirección pertenezca al cliente correcto
            if (direccionAEliminar.IdCliente != _idClienteRecibido)
            {
                MessageBox.Show("La dirección seleccionada no pertenece al cliente actual.", "Restricción de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Validar que una dirección no tenga envíos asociados antes de eliminarla
            bool tieneEnviosAsociados = ctxDb.Envios.Any(e => e.IdDireccion == _idDireccionSeleccionada);
            if (tieneEnviosAsociados)
            {
                MessageBox.Show("No se puede eliminar la dirección porque tiene envíos registrados en el sistema.", "Restricción de integridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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