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
        private int _idSeleccionado = 0; // 0 significa registro nuevo

        public FrmCategorias()
        {
            InitializeComponent();
            _categoriaService = new CategoriaService();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            LimpiarFormulario();
        }

        private void CargarCategorias()
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

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            txtNombre.Clear();
            txtDescripcion.Clear();
            if (btnEliminar != null) btnEliminar.Enabled = false;
            dgvCategorias.ClearSelection();
            dgvCategorias.CurrentCell = null;
            txtNombre.Focus();
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvCategorias.Rows[e.RowIndex];

            // Verificamos si la fuente de datos está enlazada directamente a objetos Categoria
            if (fila.DataBoundItem is Categoria categoriaSeleccionada)
            {
                _idSeleccionado = categoriaSeleccionada.Id;
                txtNombre.Text = categoriaSeleccionada.Nombre;
                txtDescripcion.Text = categoriaSeleccionada.Descripcion ?? string.Empty;
                if (btnEliminar != null) btnEliminar.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();

                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("El nombre de la categoría es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                var categoria = new Categoria
                {
                    Id = _idSeleccionado,
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                if (_idSeleccionado == 0)
                {
                    _categoriaService.Crear(categoria);
                    MessageBox.Show("Categoría creada con éxito.", "VentasOnline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _categoriaService.Actualizar(categoria);
                    MessageBox.Show("Categoría actualizada con éxito.", "VentasOnline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarCategorias();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarCategorias();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0) return;

            var respuesta = MessageBox.Show($"¿Estás seguro de eliminar la categoría \"{txtNombre.Text}\"?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            try
            {
                _categoriaService.Eliminar(_idSeleccionado);
                MessageBox.Show("Categoría eliminada con éxito.", "VentasOnline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategorias();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}