using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentasOnline.Data;
using VentasOnline.Models;
using VentasOnline.Services;

namespace VentasOnline.Forms
{
    public partial class FrmProductos : Form
    {
        // Servicios para manejar productos, categorías y proveedores
        private readonly ProductosService _productoService;
        private readonly CategoriaService _categoriaService;
        private readonly ProveedoresService _proveedorService;
        private int _idSeleccionado = 0; // 0 significa nuevo registro
        private string _rutaOrigenImagen = string.Empty;// Variable para almacenar la ruta de la imagen seleccionada

        // Constructor que inicializa los servicios y componentes del formulario
        public FrmProductos()
        {
            InitializeComponent();
            _productoService = new ProductosService();
            _categoriaService = new CategoriaService();
            _proveedorService = new ProveedoresService();
        }

        // Evento que se ejecuta al cargar el formulario, inicializa los combos y la grilla
        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarComboCategorias();
            CargarComboProveedores();
            CargarGrilla();
            LimpiarFormulario();

            // Asegurar que el botón de eliminar inicie deshabilitado al cargar el formulario
            if (btnEliminar != null)
            {
                btnEliminar.Enabled = false;
            }

            // 💡 Suscribir el evento de búsqueda en tiempo real por nombre de producto
            if (txtBusqueda != null)
            {
                txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            }
        }

        // Método para cargar las categorías en el combo box
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

        // Método para cargar los proveedores en el combo box
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

        // Método para cargar los productos en la grilla filtrando opcionalmente por nombre
        private void CargarGrilla(string filtroNombre = "")
        {
            try
            {
                var query = _productoService.ObtenerTodos().AsQueryable();

                // Si hay texto en el buscador, filtramos por el nombre del producto
                if (!string.IsNullOrWhiteSpace(filtroNombre))
                {
                    query = query.Where(p => p.Nombre != null && p.Nombre.Contains(filtroNombre, StringComparison.OrdinalIgnoreCase));
                }

                var lista = query.Select(p => new
                {
                    p.Id,
                    Categoria = p.Categoria != null ? p.Categoria.Nombre : "Sin Categoría",
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

                // Ocultar columnas que no son necesarias para la visualización
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

        // 💡 Evento que se ejecuta al escribir en el txtBusqueda
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string textoFiltro = txtBusqueda.Text.Trim();
            CargarGrilla(textoFiltro);
        }

        // Evento que se ejecuta al hacer clic en una celda de la grilla, carga los datos del producto seleccionado en el formulario
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;// Evita errores al hacer clic en el encabezado de la grilla

            // Obtiene la fila seleccionada y carga los datos del producto en el formulario
            var fila = dgvProductos.Rows[e.RowIndex];
            if (fila.Cells["Id"].Value != null)
            {
                int id = Convert.ToInt32(fila.Cells["Id"].Value);
                var seleccionado = _productoService.ObtenerPorId(id);

                // Carga los datos del producto seleccionado en los controles del formulario
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

                    // Habilitar el botón de eliminar al seleccionar un producto válido de la grilla
                    if (btnEliminar != null)
                    {
                        btnEliminar.Enabled = true;
                    }
                }
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de buscar imagen, abre un diálogo para seleccionar una imagen y actualiza el campo de texto con el nombre del archivo
        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                // Filtra los tipos de archivos de imagen permitidos
                dialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp;*.webp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaSeleccionada = dialog.FileName;

                    // Validar existencia física del archivo
                    if (!File.Exists(rutaSeleccionada))
                    {
                        MessageBox.Show("El archivo de imagen seleccionado no existe.", "Error de archivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validar tamaño máximo (Ej. máximo 5 MB)
                    FileInfo fi = new FileInfo(rutaSeleccionada);
                    if (fi.Length > 5 * 1024 * 1024)
                    {
                        MessageBox.Show("El tamaño de la imagen no puede superar los 5 MB.", "Archivo muy pesado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validar que realmente sea una imagen válida cargándola en memoria
                    try
                    {
                        using (var imgTemp = Image.FromFile(rutaSeleccionada))
                        {
                            if (imgTemp.Width == 0 || imgTemp.Height == 0)
                            {
                                throw new Exception();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("El archivo seleccionado no es una imagen válida o está dañado.", "Imagen inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _rutaOrigenImagen = rutaSeleccionada;// Almacena la ruta completa de la imagen validada
                    txtImagen.Text = Path.GetFileName(_rutaOrigenImagen);// Muestra solo el nombre del archivo en el campo de texto
                }
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de nuevo, limpia el formulario para ingresar un nuevo producto
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // Evento que se ejecuta al hacer clic en el botón de guardar, valida los datos y guarda o actualiza el producto en la base de datos
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

                // Verificar que la categoría todavía exista en la base de datos
                using var dbCtx = new AppDbContext();
                bool categoriaExiste = dbCtx.Categorias.Any(c => c.Id == idCategoriaSeleccionada);
                if (!categoriaExiste)
                {
                    MessageBox.Show("La categoría seleccionada ya no existe en la base de datos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarComboCategorias();
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

                // Verificar que el proveedor todavía exista en la base de datos
                bool proveedorExiste = dbCtx.Proveedores.Any(p => p.Id == idProveedorSeleccionado);
                if (!proveedorExiste)
                {
                    MessageBox.Show("El proveedor seleccionado ya no existe en la base de datos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarComboProveedores();
                    return;
                }

                // 3. Validar Nombre
                string nombreProducto = txtNombre.Text.Trim();
                if (string.IsNullOrWhiteSpace(nombreProducto))
                {
                    MessageBox.Show("El nombre del producto es obligatorio y no puede contener únicamente espacios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Longitud máxima del nombre (100 caracteres)
                if (nombreProducto.Length > 100)
                {
                    MessageBox.Show("El nombre del producto no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Validar longitud de la descripción (máximo 255 caracteres)
                string descripcionProducto = txtDescripcion.Text.Trim();
                if (descripcionProducto.Length > 255)
                {
                    MessageBox.Show("La descripción del producto no puede superar los 255 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescripcion.Focus();
                    return;
                }

                // Verificar que el nombre del producto no esté duplicado
                bool nombreDuplicado = dbCtx.Productos.Any(p => p.Nombre != null && p.Nombre.ToLower() == nombreProducto.ToLower() && p.Id != _idSeleccionado);
                if (nombreDuplicado)
                {
                    MessageBox.Show("Ya existe otro producto registrado con este mismo nombre.", "Nombre duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // 4. Validar Precio (Mayor a cero, límite decimal(10,2) y máximo 2 decimales)
                decimal precio = numericPrecio.Value;
                if (precio <= 0)
                {
                    MessageBox.Show("El precio debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericPrecio.Focus();
                    return;
                }

                if (precio > 99999999.99m)
                {
                    MessageBox.Show("El precio supera el límite máximo permitido por el sistema.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericPrecio.Focus();
                    return;
                }

                // Comprobar máximo 2 decimales
                decimal decimalesVerificacion = (precio * 100) % 1;
                if (decimalesVerificacion != 0)
                {
                    MessageBox.Show("El precio debe tener como máximo dos decimales.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericPrecio.Focus();
                    return;
                }

                // 5. Validar Stock (Entero, no negativo y con un límite máximo razonable de 1,000,000)
                int stock = (int)numericStock.Value;
                if (numericStock.Value < 0 || numericStock.Value % 1 != 0 || stock > 1000000)
                {
                    MessageBox.Show("El stock debe ser un número entero válido entre 0 y 1,000,000.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                // Determinar la ruta relativa de la imagen y si es una actualización o un nuevo registro
                string rutaRelativaImagenBD = txtImagen.Text.Trim();
                bool esActualizacion = _idSeleccionado != 0;
                DateTime fechaFinalPublicacion = DateTime.Now;

                if (esActualizacion)
                {
                    // Validar que el producto todavía exista antes de actualizarlo
                    var productoActual = dbCtx.Productos.Find(_idSeleccionado);
                    if (productoActual == null)
                    {
                        MessageBox.Show("El producto seleccionado ya no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CargarGrilla();
                        LimpiarFormulario();
                        return;
                    }

                    fechaFinalPublicacion = productoActual.FechaPublicacion;
                    // Si se seleccionó una nueva imagen, se copia a la carpeta local y se elimina la anterior
                    if (!string.IsNullOrEmpty(_rutaOrigenImagen))
                    {
                        rutaRelativaImagenBD = CopiarImagenACarpetaLocal(_rutaOrigenImagen);

                        // Eliminar la imagen anterior si existe
                        if (!string.IsNullOrEmpty(productoActual.ImagenUrl))
                        {
                            string rutaBase = Application.StartupPath;
                            string rutaImagenAnterior = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images", productoActual.ImagenUrl));
                            if (File.Exists(rutaImagenAnterior)) File.Delete(rutaImagenAnterior);
                        }
                    }
                    else
                    {
                        rutaRelativaImagenBD = productoActual.ImagenUrl;// Mantener la imagen anterior si no se seleccionó una nueva
                    }
                }
                else
                {
                    // Si es un nuevo registro y se seleccionó una imagen, se copia a la carpeta local
                    if (!string.IsNullOrEmpty(_rutaOrigenImagen))
                    {
                        rutaRelativaImagenBD = CopiarImagenACarpetaLocal(_rutaOrigenImagen);
                    }
                }

                // Crear un objeto Producto con los datos del formulario
                var producto = new Producto
                {
                    Id = _idSeleccionado,
                    IdCategoria = idCategoriaSeleccionada,
                    IdProveedor = idProveedorSeleccionado,
                    Nombre = nombreProducto,
                    Descripcion = descripcionProducto,
                    Precio = precio,
                    Stock = stock,
                    ImagenUrl = rutaRelativaImagenBD,
                    FechaPublicacion = fechaFinalPublicacion
                };

                // Guardar o actualizar el producto según corresponda
                if (_idSeleccionado == 0)
                {
                    // Nuevo registro
                    _productoService.Crear(producto);
                    MessageBox.Show("Producto registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualización de registro existente
                    _productoService.Actualizar(producto);
                    MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarGrilla();
                if (txtBusqueda != null) txtBusqueda.Clear();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de eliminar, elimina el producto seleccionado después de confirmar la acción
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Validar que el producto exista antes de eliminarlo y comprobar que no tenga pedidos asociados
            using var dbCtx = new AppDbContext();
            var productoAEliminar = dbCtx.Productos.Find(_idSeleccionado);
            if (productoAEliminar == null)
            {
                MessageBox.Show("El producto seleccionado ya no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarGrilla();
                LimpiarFormulario();
                return;
            }

            bool tienePedidosAsociados = dbCtx.DetallesPedidos.Any(dp => dp.IdProducto == _idSeleccionado);
            if (tienePedidosAsociados)
            {
                MessageBox.Show("No se puede eliminar el producto porque tiene pedidos o ventas asociadas en el historial.", "Restricción de integridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de eliminar el producto '{txtNombre.Text}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Si el usuario confirma la eliminación, se procede a eliminar el producto y su imagen asociada
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Eliminar la imagen asociada al producto si existe
                    if (!string.IsNullOrEmpty(productoAEliminar.ImagenUrl))
                    {
                        string rutaBase = Application.StartupPath;
                        string rutaImagenFisica = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images", productoAEliminar.ImagenUrl));
                        // Eliminar la imagen física si existe
                        if (File.Exists(rutaImagenFisica)) File.Delete(rutaImagenFisica);
                    }
                    // Eliminar el producto de la base de datos
                    _productoService.Eliminar(_idSeleccionado);
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarGrilla();
                    if (txtBusqueda != null) txtBusqueda.Clear();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Restricción de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de cancelar, limpia el formulario y cierra la ventana
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Close();
        }

        // Método para limpiar los controles del formulario y restablecer el estado inicial
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

            // Deshabilitar el botón de eliminar al limpiar el formulario
            if (btnEliminar != null)
            {
                btnEliminar.Enabled = false;
            }

            cmbCategoria.Focus();
        }

        // Método para copiar la imagen seleccionada a la carpeta local del proyecto y devolver el nombre del archivo copiado
        private string CopiarImagenACarpetaLocal(string rutaOrigen)
        {
            string rutaBase = Application.StartupPath;
            string carpetaDestino = Path.GetFullPath(Path.Combine(rutaBase, @"..\..\..\Images"));

            // Crear la carpeta de destino si no existe
            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            // Generar un nombre de archivo único para evitar conflictos
            string extension = Path.GetExtension(rutaOrigen);
            string nombreArchivoUnico = $"{Guid.NewGuid()}{extension}";
            string rutaDestinoCompleta = Path.Combine(carpetaDestino, nombreArchivoUnico);

            File.Copy(rutaOrigen, rutaDestinoCompleta, true);// Copia la imagen a la carpeta local, sobrescribiendo si ya existe
            return nombreArchivoUnico;
        }

        private void desktopLabel_Click(object sender, EventArgs e) { }

        private void lblBusqueda_Click(object sender, EventArgs e)
        {

        }
    }
}