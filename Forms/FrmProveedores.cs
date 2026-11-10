using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmProveedores : Form
    {
        // Instancia del servicio encargado de la lógica de negocio y comunicación con la base de datos para los proveedores
        private readonly ProveedoresService _proveedorService;

        // Variable de control para saber si se va a registrar un nuevo proveedor (0) o si se está editando uno existente
        private int _idSeleccionado = 0; // 0 significa registro nuevo

        public FrmProveedores()
        {
            InitializeComponent();
            _proveedorService = new ProveedoresService();
        }

        // Evento que se ejecuta al cargar la ventana, inicializando la grilla de datos y limpiando los campos de texto
        private void FormProveedores_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarFormulario();
        }

        // Cargar todos los proveedores en el DataGridView
        private void CargarGrilla()
        {
            try
            {
                // Obtiene la lista completa de proveedores desde la base de datos a través del servicio
                var lista = _proveedorService.ObtenerTodas();
                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = lista;

                // Ocultar la columna de navegación de Productos si existe en el modelo
                if (dgvProveedores.Columns["Productos"] != null)
                {
                    dgvProveedores.Columns["Productos"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error en caso de que falle la carga de datos
                MessageBox.Show($"Error al cargar proveedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento para capturar el clic en la grilla y rellenar el formulario para editar
        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evita errores si se hace clic en los encabezados de la tabla (índice menor a 0)
            if (e.RowIndex < 0) return;

            var fila = dgvProveedores.Rows[e.RowIndex];

            // Extrae el objeto Proveedores vinculado a la fila seleccionada y carga sus valores en los TextBox correspondientes
            if (fila.DataBoundItem is Proveedores proveedorSeleccionado)
            {
                _idSeleccionado = proveedorSeleccionado.Id;
                txtNombre.Text = proveedorSeleccionado.Nombre;
                txtTelefono.Text = proveedorSeleccionado.Telefono ?? string.Empty;
                txtCorreo.Text = proveedorSeleccionado.Correo ?? string.Empty;
            }
        }

        // Botón Nuevo
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Restablece el formulario para permitir el registro de un proveedor completamente nuevo
            LimpiarFormulario();
        }

        // Botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Captura y limpia los espacios en blanco de los campos de texto
                string nombre = txtNombre.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string correo = txtCorreo.Text.Trim();

                // Validación obligatoria: El nombre del proveedor no puede estar vacío
                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("El nombre del proveedor es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Crea una instancia del modelo con los datos ingresados
                var proveedor = new Proveedores
                {
                    Id = _idSeleccionado,
                    Nombre = nombre,
                    Telefono = telefono,
                    Correo = correo
                };

                if (_idSeleccionado == 0)
                {
                    // Crear nuevo
                    _proveedorService.Crear(proveedor);
                    MessageBox.Show("Proveedor creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualizar existente
                    _proveedorService.Actualizar(proveedor);
                    MessageBox.Show("Proveedor actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Actualiza la tabla y limpia los campos tras una operación exitosa
                CargarGrilla();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // Muestra advertencias si ocurre algún error controlado durante el guardado
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Botón Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Valida que se haya seleccionado un proveedor previamente antes de intentar borrar
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Pide confirmación al usuario antes de proceder con la eliminación
            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar el proveedor '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Ejecuta el método de eliminación en el servicio usando el ID seleccionado
                    _proveedorService.Eliminar(_idSeleccionado);
                    MessageBox.Show("Proveedor eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresca la grilla y limpia el formulario
                    CargarGrilla();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    // Maneja excepciones específicas (como restricciones de claves foráneas al borrar)
                    MessageBox.Show(ex.Message, "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // Botón Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpia los campos y cierra la ventana actual
            LimpiarFormulario();
            Close();
        }

        // Limpiar controles
        private void LimpiarFormulario()
        {
            // Restablece el ID a 0, quita la selección del DataGridView y limpia las cajas de texto enfocando nuevamente el nombre
            _idSeleccionado = 0;
            dgvProveedores.ClearSelection();
            dgvProveedores.CurrentCell = null;
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtNombre.Focus();
        }
    }
}