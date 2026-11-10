using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
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

            // Asegurar que el botón de eliminar inicie deshabilitado
            if (btnEliminar != null)
            {
                btnEliminar.Enabled = false;
            }

            // 💡 Suscribir el evento de búsqueda en tiempo real por correo
            if (txtBusqueda != null)
            {
                txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            }
        }

        // Cargar todos los proveedores en el DataGridView filtrando opcionalmente por correo
        private void CargarGrilla(string filtroCorreo = "")
        {
            try
            {
                // Obtiene la lista completa de proveedores desde la base de datos a través del servicio
                var lista = _proveedorService.ObtenerTodas();

                // Si hay texto en el buscador, filtramos por el correo (ignorando mayúsculas/minúsculas)
                if (!string.IsNullOrWhiteSpace(filtroCorreo))
                {
                    lista = lista.Where(p => p.Correo != null && p.Correo.Contains(filtroCorreo, StringComparison.OrdinalIgnoreCase)).ToList();
                }

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

        // 💡 Evento que se ejecuta al escribir en el txtBusqueda
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string textoFiltro = txtBusqueda.Text.Trim();
            CargarGrilla(textoFiltro);
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

                // Habilitar el botón de eliminar al seleccionar un proveedor válido
                if (btnEliminar != null)
                {
                    btnEliminar.Enabled = true;
                }
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
                string correo = txtCorreo.Text.Trim().ToLowerInvariant(); // Normalización a minúsculas

                // Validación obligatoria: El nombre del proveedor no puede estar vacío ni contener únicamente espacios
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("El nombre del proveedor es obligatorio y no puede contener únicamente espacios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Validación: Longitud máxima del nombre (100 caracteres)
                if (nombre.Length > 100)
                {
                    MessageBox.Show("El nombre del proveedor no puede superar los 100 caracteres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Validación: El teléfono es obligatorio
                if (string.IsNullOrWhiteSpace(telefono))
                {
                    MessageBox.Show("El teléfono del proveedor es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }

                // Validación: El teléfono debe contener únicamente números y exactamente 8 dígitos
                if (!System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d{8}$"))
                {
                    MessageBox.Show("El teléfono debe contener únicamente números y tener una longitud exacta de 8 dígitos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }

                // Validación: El correo electrónico es obligatorio
                if (string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show("El correo electrónico del proveedor es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                // Validación: Longitud máxima del correo (150 caracteres)
                if (correo.Length > 150)
                {
                    MessageBox.Show("El correo electrónico no puede superar los 150 caracteres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                // Validación: El correo electrónico debe tener un formato válido
                if (!new EmailAddressAttribute().IsValid(correo))
                {
                    MessageBox.Show("Escribe un correo electrónico válido para el proveedor.", "Correo no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                // Validaciones de duplicados en la base de datos
                var listaExistente = _proveedorService.ObtenerTodas();

                if (listaExistente.Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) && p.Id != _idSeleccionado))
                {
                    MessageBox.Show("Ya existe otro proveedor registrado con este mismo nombre.", "Nombre duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (listaExistente.Any(p => p.Correo != null && p.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase) && p.Id != _idSeleccionado))
                {
                    MessageBox.Show("Ya existe otro proveedor registrado con este mismo correo electrónico.", "Correo duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
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

                // Actualiza la tabla, limpia el buscador y restablece los campos tras una operación exitosa
                CargarGrilla();
                if (txtBusqueda != null) txtBusqueda.Clear();
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

                    // Refresca la grilla, limpia el buscador y el formulario
                    CargarGrilla();
                    if (txtBusqueda != null) txtBusqueda.Clear();
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

            // Deshabilitar el botón de eliminar al limpiar el formulario
            if (btnEliminar != null)
            {
                btnEliminar.Enabled = false;
            }

            txtNombre.Focus();
        }
    }
}