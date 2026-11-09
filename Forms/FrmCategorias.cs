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
    public partial class FrmCategorias : Form
    {
        private readonly CategoriaService _categoriaService;
        private int _idSeleccionado = 0;

        public FrmCategorias()
        {
            InitializeComponent();
            _categoriaService = new CategoriaService();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            //LimpiarFormulario();
        }

        // Cargar datos en la grilla desde el servicio
        private void CargarGrilla()
        {
            try
            {
                var lista = _categoriaService.ObtenerTodas();
                dgvCategorias.DataSource = null;
                dgvCategorias.DataSource = lista;

                if (dgvCategorias.Columns["Productos"] != null)
                {
                    dgvCategorias.Columns["Productos"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow?.DataBoundItem is Categoria seleccionada)
            {
                _idSeleccionado = seleccionada.Id;
                txtNombre.Text = seleccionada.Nombre;
                txtDescripcion.Text = seleccionada.Descripcion;
            }
        }

        // Botón Nuevo
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
        }

        // Botón Guardar (Crear / Actualizar)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var categoria = new Categoria
                {
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim()
                };

                if (_idSeleccionado == 0)
                {
                    // Crear
                    _categoriaService.Crear(categoria);
                    MessageBox.Show("Categoría creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    // Actualizar
                    categoria.Id = _idSeleccionado;
                    _categoriaService.Actualizar(categoria);
                    MessageBox.Show("Categoría actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Botón Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione una categoría de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar la categoría '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _categoriaService.Eliminar(_idSeleccionado);
                    MessageBox.Show("Categoría eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarGrilla();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // Botón Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        // Limpiar cajas de texto y selección
        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            dgvCategorias.ClearSelection();
            dgvCategorias.CurrentCell = null;
            txtNombre.Clear();
            txtDescripcion.Clear();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}