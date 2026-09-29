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

        public FrmCategorias()
        {
            InitializeComponent();
            _categoriaService = new CategoriaService();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarFormulario();
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
                txtId.Text = seleccionada.Id.ToString();
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

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    // Crear
                    _categoriaService.Crear(categoria);
                    MessageBox.Show("Categoría creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualizar
                    categoria.Id = int.Parse(txtId.Text);
                    _categoriaService.Actualizar(categoria);
                    MessageBox.Show("Categoría actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione una categoría de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = int.Parse(txtId.Text);

            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar la categoría '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _categoriaService.Eliminar(id);
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
        }

        // Limpiar cajas de texto y selección
        private void LimpiarFormulario()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            if (dgvCategorias.Rows.Count > 0)
            {
                dgvCategorias.ClearSelection();
            }
        }
    }
}