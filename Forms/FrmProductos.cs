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
    public partial class FrmProductos : Form
    {
        private readonly ProductosService _productoService;
        private readonly CategoriaService _categoriaService;
        private string _rutaOrigenImagen = string.Empty; // Mantiene la ruta temporal si se selecciona una nueva imagen
        public FrmProductos()
        {
            InitializeComponent();
            _productoService = new ProductosService();
            _categoriaService = new CategoriaService();

        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarComboCategorias();
            CargarGrilla();
            LimpiarFormulario();
        }

        // Cargar el ComboBox de Categorías
        private void CargarComboCategorias()
        {
            try
            {
                var categorias = _categoriaService.ObtenerTodas();

                categorias.Insert(0, new Categoria
                {
                    Id = 0,
                    Nombre = "-- Seleccione una categoría --"
                });

                cmbCategoria.DataSource = categorias;
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "Id";
                cmbCategoria.SelectedIndex = 0; // Selecciona por defecto la opción "-- Seleccione una categoría --"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cargar datos en el DataGridView desde el servicio
        // Cargar datos en el DataGridView desde el servicio
        private void CargarGrilla()
        {
            try
            {
                var lista = _productoService.ObtenerTodos().Select(p => new
                {
                    p.Id,
                    Categoria = p.Categoria?.Nombre ?? "Sin Categoría",
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.Stock,
                    p.ImagenUrl,
                    FechaPublicacion = p.FechaPublicacion.ToString("yyyy-MM-dd HH:mm"),
                    p.IdCategoria
                }).ToList();

                dgvProductos.DataSource = null;
                dgvProductos.DataSource = lista;

                // Ocultar el ID de la categoría y del productos que ya no es necesario ver en la grilla
                if (dgvProductos.Columns["IdCategoria"] != null)
                    dgvProductos.Columns["IdCategoria"].Visible = false;

                if (dgvProductos.Columns["Id"] != null)
                    dgvProductos.Columns["Id"].Visible = false;

                // Posicionar la columna de la categoría despues del nombre del producto
                if (dgvProductos.Columns["Categoria"] != null)
                    dgvProductos.Columns["Categoria"].DisplayIndex = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Selección de fila en la grilla
        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow != null && dgvProductos.CurrentRow.Cells["Id"].Value != null)
            {
                int id = Convert.ToInt32(dgvProductos.CurrentRow.Cells["Id"].Value);
                var seleccionado = _productoService.ObtenerPorId(id);

                if (seleccionado != null)
                {
                    txtId.Text = seleccionado.Id.ToString();
                    cmbCategoria.SelectedValue = seleccionado.IdCategoria;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    numericPrecio.Value = seleccionado.Precio;
                    numericStock.Value = seleccionado.Stock;
                    txtImagen.Text = seleccionado.ImagenUrl ?? string.Empty;

                    _rutaOrigenImagen = string.Empty;
                }
            }
        }

        // Botón Buscar imagen
        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp;*.webp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _rutaOrigenImagen = dialog.FileName;
                    txtImagen.Text = Path.GetFileName(_rutaOrigenImagen); // Mostrar solo el nombre del archivo en la caja de texto
                }
            }
        }

        // Botón Nuevo
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            cmbCategoria.Focus();
        }

        // Botón Guardar (Crear / Actualizar)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se haya seleccionado una categoría válida (mayor a 0)
                int idCategoriaSeleccionada = Convert.ToInt32(cmbCategoria.SelectedValue);
                if (idCategoriaSeleccionada <= 0)
                {
                    MessageBox.Show("Por favor, seleccione una categoría válida de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCategoria.Focus();
                    return;
                }

                if (numericStock.Value % 1 != 0)
                {
                    MessageBox.Show("El stock debe ser un número entero (no se permiten decimales).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericStock.Focus();
                    return;
                }

                string rutaRelativaImagenBD = txtImagen.Text.Trim();
                bool esActualizacion = !string.IsNullOrEmpty(txtId.Text);
                DateTime fechaFinalPublicacion = DateTime.Now;

                if (esActualizacion)
                {
                    int idProducto = int.Parse(txtId.Text);
                    // Obtenemos el registro actual de la base de datos para conocer su imagen anterior
                    var productoActual = _productoService.ObtenerPorId(idProducto);

                    if (productoActual != null)
                    {
                        // Conservamos la fecha original de cuando se creó el producto
                        fechaFinalPublicacion = productoActual.FechaPublicacion;
                        // Si el usuario seleccionó una NUEVA imagen (_rutaOrigenImagen tiene valor)
                        if (!string.IsNullOrEmpty(_rutaOrigenImagen))
                        {
                            // 1. Copiamos la nueva imagen y obtenemos su nombre único
                            rutaRelativaImagenBD = CopiarImagenACarpetaLocal(_rutaOrigenImagen);

                            // 2. Eliminamos la imagen vieja de la carpeta física si existe
                            if (!string.IsNullOrEmpty(productoActual.ImagenUrl))
                            {
                                string rutaBase = Application.StartupPath;
                                string rutaImagenAnterior = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images", productoActual.ImagenUrl));

                                if (File.Exists(rutaImagenAnterior))
                                {
                                    File.Delete(rutaImagenAnterior);
                                }
                            }
                        }
                        else
                        {
                            // Si no seleccionó una nueva imagen, mantenemos la que ya tenía guardada
                            rutaRelativaImagenBD = productoActual.ImagenUrl;
                        }
                    }
                }
                else
                {
                    // Si es un producto NUEVO y seleccionó imagen, la copiamos
                    if (!string.IsNullOrEmpty(_rutaOrigenImagen))
                    {
                        rutaRelativaImagenBD = CopiarImagenACarpetaLocal(_rutaOrigenImagen);
                    }
                }

                var producto = new Producto
                {
                    IdCategoria = idCategoriaSeleccionada,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Precio = numericPrecio.Value,
                    Stock = (int)numericStock.Value,
                    ImagenUrl = rutaRelativaImagenBD,
                    FechaPublicacion = fechaFinalPublicacion
                };

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    _productoService.Crear(producto);
                    MessageBox.Show("Producto registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    producto.Id = int.Parse(txtId.Text);
                    _productoService.Actualizar(producto);
                    MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarGrilla();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show(mensajeError, "Error detallado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Botón Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione un producto de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = int.Parse(txtId.Text);

            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar el producto '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var productoAEliminar = _productoService.ObtenerPorId(id);

                    // Borrar archivo físico si existe
                    if (productoAEliminar != null && !string.IsNullOrEmpty(productoAEliminar.ImagenUrl))
                    {
                        string rutaBase = Application.StartupPath;
                        string rutaImagenFisica = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images", productoAEliminar.ImagenUrl));

                        if (File.Exists(rutaImagenFisica))
                        {
                            File.Delete(rutaImagenFisica);
                        }
                    }
                    _productoService.Eliminar(id);
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        // Limpieza de campos
        private void LimpiarFormulario()
        {
            txtId.Clear();
            cmbCategoria.SelectedIndex = 0;
            txtNombre.Clear();
            txtDescripcion.Clear();
            numericPrecio.Value = 0;
            numericStock.Value = 0;
            txtImagen.Clear();
            _rutaOrigenImagen = string.Empty;

            if (dgvProductos.Rows.Count > 0)
            {
                dgvProductos.ClearSelection();
            }
        }

        // Copiar la imagen seleccionada a la carpeta /images en el directorio del ejecutable
        private string CopiarImagenACarpetaLocal(string rutaOrigen)
        {
            string rutaBase = Application.StartupPath;
            string carpetaDestino = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images"));

            // Si la carpeta Images no existe en la raíz del proyecto la creamos
            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            string extension = Path.GetExtension(rutaOrigen);
            string nombreArchivoUnico = $"{Guid.NewGuid()}{extension}";
            string rutaDestinoCompleta = Path.Combine(carpetaDestino, nombreArchivoUnico);

            // Copia el archivo directamente al directorio del proyecto
            File.Copy(rutaOrigen, rutaDestinoCompleta, true);

            return nombreArchivoUnico;
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
