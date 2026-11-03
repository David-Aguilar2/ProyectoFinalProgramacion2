using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private readonly ProveedoresService _proveedorService;
        private int _idSeleccionado = 0;
        private string _rutaOrigenImagen = string.Empty; // Mantiene la ruta temporal si se selecciona una nueva imagen

        public FrmProductos()
        {
            InitializeComponent();
            _productoService = new ProductosService();
            _categoriaService = new CategoriaService();
            _proveedorService = new ProveedoresService();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarComboCategorias();
            CargarComboProveedores();
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
                cmbCategoria.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cargar el ComboBox de Proveedores
        private void CargarComboProveedores()
        {
            try
            {
                var proveedores = _proveedorService.ObtenerTodas();

                proveedores.Insert(0, new Proveedores
                {
                    Id = 0,
                    Nombre = "-- Seleccione un proveedor --"
                });

                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DisplayMember = "Nombre";
                cmbProveedor.ValueMember = "Id";
                cmbProveedor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proveedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cargar datos en el DataGridView desde el servicio
        private void CargarGrilla()
        {
            try
            {
                var lista = _productoService.ObtenerTodos().Select(p => new
                {
                    p.Id,
                    Categoria = p.Categoria?.Nombre ?? "Sin Categoría",
                    Proveedor = p.Proveedor != null ? p.Proveedor.Nombre : "Sin Proveedor", // <--- Validación segura
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.Stock,
                    p.ImagenUrl,
                    FechaPublicacion = p.FechaPublicacion.ToString("yyyy-MM-dd HH:mm"),
                    p.IdCategoria,
                    p.IdProveedor
                }).ToList();

                dgvProductos.DataSource = null;
                dgvProductos.DataSource = lista;

                if (dgvProductos.Columns["IdCategoria"] != null)
                    dgvProductos.Columns["IdCategoria"].Visible = false;

                if (dgvProductos.Columns["IdProveedor"] != null)
                    dgvProductos.Columns["IdProveedor"].Visible = false;

                if (dgvProductos.Columns["Id"] != null)
                    dgvProductos.Columns["Id"].Visible = false;

                if (dgvProductos.Columns["Categoria"] != null)
                    dgvProductos.Columns["Categoria"].DisplayIndex = 2;

                if (dgvProductos.Columns["Proveedor"] != null)
                    dgvProductos.Columns["Proveedor"].DisplayIndex = 3;
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
                    _idSeleccionado = seleccionado.Id;
                    cmbCategoria.SelectedValue = seleccionado.IdCategoria;
                    cmbProveedor.SelectedValue = seleccionado.IdProveedor;
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
                    txtImagen.Text = Path.GetFileName(_rutaOrigenImagen); // Mostrar solo el nombre del archivo
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
                // Validar categoría
                int idCategoriaSeleccionada = Convert.ToInt32(cmbCategoria.SelectedValue);
                if (idCategoriaSeleccionada <= 0)
                {
                    MessageBox.Show("Por favor, seleccione una categoría válida de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCategoria.Focus();
                    return;
                }

                // Validar proveedor
                int idProveedorSeleccionado = Convert.ToInt32(cmbProveedor.SelectedValue);
                if (idProveedorSeleccionado <= 0)
                {
                    MessageBox.Show("Por favor, seleccione un proveedor válido de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbProveedor.Focus();
                    return;
                }

                if (numericStock.Value % 1 != 0)
                {
                    MessageBox.Show("El stock debe ser un número entero (no se permiten decimales).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericStock.Focus();
                    return;
                }

                string rutaRelativaImagenBD = txtImagen.Text.Trim();
                bool esActualizacion = _idSeleccionado != 0;
                DateTime fechaFinalPublicacion = DateTime.Now;

                if (esActualizacion)
                {
                    int idProducto = _idSeleccionado;
                    var productoActual = _productoService.ObtenerPorId(idProducto);

                    if (productoActual != null)
                    {
                        fechaFinalPublicacion = productoActual.FechaPublicacion;
                        if (!string.IsNullOrEmpty(_rutaOrigenImagen))
                        {
                            rutaRelativaImagenBD = CopiarImagenACarpetaLocal(_rutaOrigenImagen);

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
                            rutaRelativaImagenBD = productoActual.ImagenUrl;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_rutaOrigenImagen))
                    {
                        rutaRelativaImagenBD = CopiarImagenACarpetaLocal(_rutaOrigenImagen);
                    }
                }

                var producto = new Producto
                {
                    IdCategoria = idCategoriaSeleccionada,
                    IdProveedor = idProveedorSeleccionado,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Precio = numericPrecio.Value,
                    Stock = (int)numericStock.Value,
                    ImagenUrl = rutaRelativaImagenBD,
                    FechaPublicacion = fechaFinalPublicacion
                };

                if (_idSeleccionado == 0)
                {
                    _productoService.Crear(producto);
                    MessageBox.Show("Producto registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    producto.Id = _idSeleccionado;
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
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = _idSeleccionado;

            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar el producto '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var productoAEliminar = _productoService.ObtenerPorId(id);

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
            Close();
        }

        // Limpieza de campos
        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
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

        // Copiar la imagen seleccionada a la carpeta /images
        private string CopiarImagenACarpetaLocal(string rutaOrigen)
        {
            string rutaBase = Application.StartupPath;
            string carpetaDestino = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images"));

            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            string extension = Path.GetExtension(rutaOrigen);
            string nombreArchivoUnico = $"{Guid.NewGuid()}{extension}";
            string rutaDestinoCompleta = Path.Combine(carpetaDestino, nombreArchivoUnico);

            File.Copy(rutaOrigen, rutaDestinoCompleta, true);

            return nombreArchivoUnico;
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void desktopLabel_Click(object sender, EventArgs e)
        {
        }
    }
}