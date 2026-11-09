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
        // Servicio para manejar operaciones de proveedores
        private readonly ProveedoresService _proveedorService;
        private int _idSeleccionado = 0;

        // Constructor que inicializa el formulario y el servicio de proveedores
        public FrmProveedores()
        {
            InitializeComponent();
            _proveedorService = new ProveedoresService();
        }

        // Evento Load del formulario que carga la grilla de proveedores y limpia el formulario al iniciar
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
                // Obtener la lista de proveedores desde el servicio
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
                MessageBox.Show($"Error al cargar proveedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón Nuevo que limpia el formulario para ingresar un nuevo proveedor
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
        }

        // Botón Guardar que crea o actualiza un proveedor según el estado del formulario
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                var proveedor = new Proveedores
                {
                    Nombre = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim()
                };

                if (_idSeleccionado == 0)
                {
                    // Crear nuevo
                    _proveedorService.Crear(proveedor);
                    MessageBox.Show("Proveedor creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    // Actualizar existente
                    proveedor.Id = _idSeleccionado;
                    _proveedorService.Actualizar(proveedor);
                    MessageBox.Show("Proveedor actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }

                CargarGrilla();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Botón Eliminar que elimina el proveedor seleccionado después de confirmar la acción
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirmar eliminación
            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar el proveedor '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _proveedorService.Eliminar(_idSeleccionado);
                    MessageBox.Show("Proveedor eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarGrilla();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // Botón Cancelar que limpia el formulario y cierra la ventana
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        // Limpiar controles
        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            dgvProveedores.ClearSelection();
            dgvProveedores.CurrentCell = null;
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }
    }
}