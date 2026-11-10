using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmCategorias : Form
    {
        // Servicio para manejar operaciones relacionadas con categorías
        private readonly CategoriaService _categoriaService;
        private int _idSeleccionado = 0; // 0 significa registro nuevo

        // Constructor del formulario, inicializa componentes y el servicio de categorías
        public FrmCategorias()
        {
            InitializeComponent();
            _categoriaService = new CategoriaService();
        }

        // Evento que se ejecuta al cargar el formulario, carga las categorías y limpia el formulario
        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            LimpiarFormulario();

            // Suscribir el evento de búsqueda en tiempo real
            if (txtBusqueda != null)
            {
                txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            }
        }

        // Método que carga las categorías desde la base de datos y las muestra en el DataGridView (con filtro opcional)
        private void CargarCategorias(string filtro = "")
        {
            try
            {
                var lista = _categoriaService.ObtenerTodas();

                // Filtrar por nombre si se proporciona un texto de búsqueda
                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    lista = lista.Where(c => c.Nombre != null && c.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                dgvCategorias.DataSource = null;
                dgvCategorias.DataSource = lista;

                // Ocultar la columna de productos si existe
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

        // Evento que se ejecuta al escribir en la caja de texto de búsqueda
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string textoFiltro = txtBusqueda.Text.Trim();
            CargarCategorias(textoFiltro);
        }

        // Método que limpia los campos del formulario y resetea el estado de selección
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

        // Evento que se ejecuta al hacer clic en una celda del DataGridView, selecciona la categoría correspondiente
        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;// Evita errores si se hace clic en el encabezado

            var fila = dgvCategorias.Rows[e.RowIndex];

            // Verificamos si es una fila válida y obtenemos la categoría seleccionada
            if (fila.DataBoundItem is Categoria categoriaSeleccionada)
            {
                _idSeleccionado = categoriaSeleccionada.Id;
                txtNombre.Text = categoriaSeleccionada.Nombre;
                txtDescripcion.Text = categoriaSeleccionada.Descripcion ?? string.Empty;
                if (btnEliminar != null) btnEliminar.Enabled = true;
            }
        }

        // Evento que al hacer clic limpia el formulario para crear una nueva categoría
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // Evento que se ejecuta al hacer clic en el botón de guardar, crea o actualiza la categoría según corresponda
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();

                // Validación básica: el nombre de la categoría no puede estar vacío
                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("El nombre de la categoría es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Crear un objeto de categoría con los datos ingresados
                var categoria = new Categoria
                {
                    Id = _idSeleccionado,
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                // Dependiendo de si es un registro nuevo o una actualización, se llama al método correspondiente del servicio
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
                if (txtBusqueda != null) txtBusqueda.Clear();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarCategorias();
            }
        }

        // Evento que se ejecuta al hacer clic elimina la categoría seleccionada después de confirmar la acción
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Validación: si no hay una categoría seleccionada no se hace nada
            if (_idSeleccionado == 0) return;

            var respuesta = MessageBox.Show($"¿Estás seguro de eliminar la categoría \"{txtNombre.Text}\"?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            // Intento de eliminar la categoría seleccionada, mostrando un mensaje de éxito o error según corresponda
            try
            {
                _categoriaService.Eliminar(_idSeleccionado);
                MessageBox.Show("Categoría eliminada con éxito.", "VentasOnline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategorias();
                if (txtBusqueda != null) txtBusqueda.Clear();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de cancelar, limpia el formulario y cierra la ventana
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e) { }
    }
}