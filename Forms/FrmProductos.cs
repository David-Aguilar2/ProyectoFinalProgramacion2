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
        private int _idSeleccionado = 0; // 0 significa nuevo registro
        private string _rutaOrigenImagen = string.Empty;

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

        private void CargarComboCategorias()
        {
            try
            {
                var categorias = _categoriaService.ObtenerTodas();
                categorias.Insert(0, new Categoria { Id = 0, Nombre = "-- Seleccione una categoría --" });

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

        private void CargarComboProveedores()
        {
            try
            {
                var proveedores = _proveedorService.ObtenerTodas();
                proveedores.Insert(0, new Proveedores { Id = 0, Nombre = "-- Seleccione un proveedor --" });

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

        private void CargarGrilla()
        {
            try
            {
                var lista = _productoService.ObtenerTodos().Select(p => new
                {
                    p.Id,
                    Categoria = p.Categoria?.Nombre ?? "Sin Categoría",
                    Proveedor = p.Proveedor != null ? p.Proveedor.Nombre : "Sin Proveedor",
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

                if (dgvProductos.Columns["IdCategoria"] != null) dgvProductos.Columns["IdCategoria"].Visible = false;
                if (dgvProductos.Columns["IdProveedor"] != null) dgvProductos.Columns["IdProveedor"].Visible = false;
                if (dgvProductos.Columns["Id"] != null) dgvProductos.Columns["Id"].Visible = false;

                if (dgvProductos.Columns["Categoria"] != null) dgvProductos.Columns["Categoria"].DisplayIndex = 2;
                if (dgvProductos.Columns["Proveedor"] != null) dgvProductos.Columns["Proveedor"].DisplayIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reemplazo de SelectionChanged por CellClick para evitar sobrescrituras de ID no deseadas
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvProductos.Rows[e.RowIndex];
            if (fila.Cells["Id"].Value != null)
            {
                int id = Convert.ToInt32(fila.Cells["Id"].Value);
                var seleccionado = _productoService.ObtenerPorId(id);

                if (seleccionado != null)
                {
                    _idSeleccionado = seleccionado.Id;
                    cmbCategoria.SelectedValue = seleccionado.IdCategoria;
                    cmbProveedor.SelectedValue = seleccionado.IdProveedor;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion ?? string.Empty;
                    numericPrecio.Value = seleccionado.Precio;
                    numericStock.Value = seleccionado.Stock;
                    txtImagen.Text = seleccionado.ImagenUrl ?? string.Empty;
                    _rutaOrigenImagen = string.Empty;
                }
            }
        }

        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp;*.webp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _rutaOrigenImagen = dialog.FileName;
                    txtImagen.Text = Path.GetFileName(_rutaOrigenImagen);
                }
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
                // 1. Validar Categoría
                int idCategoriaSeleccionada = Convert.ToInt32(cmbCategoria.SelectedValue);
                if (idCategoriaSeleccionada <= 0)
                {
                    MessageBox.Show("Por favor, seleccione una categoría válida de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCategoria.Focus();
                    return;
                }

                // 2. Validar Proveedor
                int idProveedorSeleccionado = Convert.ToInt32(cmbProveedor.SelectedValue);
                if (idProveedorSeleccionado <= 0)
                {
                    MessageBox.Show("Por favor, seleccione un proveedor válido de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbProveedor.Focus();
                    return;
                }

                // 3. Validar Nombre
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre del producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // 4. Validar Precio
                if (numericPrecio.Value <= 0)
                {
                    MessageBox.Show("El precio debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericPrecio.Focus();
                    return;
                }

                // 5. Validar Stock (Entero y no negativo)
                if (numericStock.Value < 0 || numericStock.Value % 1 != 0)
                {
                    MessageBox.Show("El stock debe ser un número entero válido (no se permiten negativos ni decimales).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericStock.Focus();
                    return;
                }

                // 6. Validar Imagen Obligatoria
                if (string.IsNullOrWhiteSpace(txtImagen.Text))
                {
                    MessageBox.Show("Debe seleccionar una imagen para el producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnBuscarImagen.Focus();
                    return;
                }

                string rutaRelativaImagenBD = txtImagen.Text.Trim();
                bool esActualizacion = _idSeleccionado != 0;
                DateTime fechaFinalPublicacion = DateTime.Now;

                if (esActualizacion)
                {
                    var productoActual = _productoService.ObtenerPorId(_idSeleccionado);
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
                                if (File.Exists(rutaImagenAnterior)) File.Delete(rutaImagenAnterior);
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
                    Id = _idSeleccionado,
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
                    _productoService.Actualizar(producto);
                    MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarGrilla();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar el producto '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var productoAEliminar = _productoService.ObtenerPorId(_idSeleccionado);
                    if (productoAEliminar != null && !string.IsNullOrEmpty(productoAEliminar.ImagenUrl))
                    {
                        string rutaBase = Application.StartupPath;
                        string rutaImagenFisica = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images", productoAEliminar.ImagenUrl));
                        if (File.Exists(rutaImagenFisica)) File.Delete(rutaImagenFisica);
                    }

                    _productoService.Eliminar(_idSeleccionado);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
            dgvProductos.ClearSelection();
            dgvProductos.CurrentCell = null;
            txtNombre.Clear();
            txtDescripcion.Clear();
            numericPrecio.Value = 0;
            numericStock.Value = 0;
            txtImagen.Clear();
            _rutaOrigenImagen = string.Empty;
            cmbCategoria.Focus();
        }

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

        private void desktopLabel_Click(object sender, EventArgs e) { }
    }
}