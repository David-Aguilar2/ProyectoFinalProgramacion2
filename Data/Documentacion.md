# Documentación técnica de VentasOnline

## 1. Propósito y alcance

`VentasOnline` es una aplicación de escritorio de punto de venta para gestionar usuarios, clientes, categorías, productos, pedidos, pagos y envíos. Está implementada en C# con Windows Forms; Entity Framework Core persiste los datos en SQL Server.

Esta guía describe el comportamiento actual del código fuente, sus responsabilidades, reglas de negocio y ejemplos representativos. No incluye archivos `.slnx`, `.csproj`, `.resx`, `.user` ni `.sql`.

## 2. Componentes y tecnologías

| Componente | Responsabilidad |
|---|---|
| `Program.cs` | Arrancar Windows Forms y exigir autenticación antes de abrir la aplicación principal. |
| `Form1.cs` | Presentar la navegación, mostrar la sesión y abrir las pantallas funcionales. |
| `Forms` | Implementar la interacción, carga de datos, validación de interfaz y operaciones del negocio. |
| `Models` | Definir entidades y relaciones entre los conceptos del negocio. |
| `Services` | Centralizar el CRUD y las reglas compartidas para categorías, clientes y productos. |
| `Data\AppDbContext.cs` | Exponer las entidades a EF Core, configurar SQL Server y las relaciones. |
| `Config\ConfiguracionApp.cs` | Cargar la cadena de conexión desde configuración JSON. |
| `UI\LogoHelper.cs` | Cargar una imagen de logotipo desde el directorio de ejecución. |

## 3. Configurar

La aplicación requiere Windows y acceso a SQL Server LocalDB. La configuración actual de `appsettings.json` usa:

```json
{
  "ConnectionStrings": {
    "TiendaOnlineDb": "Server=(localdb)\\MSSQLLocalDB;Database=TiendaOnlineDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
  }
}
```

`ConfiguracionApp` busca primero `appsettings.json` junto al ejecutable y, si no está allí, intenta cargarlo desde el directorio de trabajo actual. El archivo se considera obligatorio y se produce una excepción si falta la cadena solicitada:

```csharp
IConfigurationRoot configuracion = new ConfigurationBuilder()
    .SetBasePath(rutaBase)
    .AddJsonFile(Archivo, optional: false, reloadOnChange: false)
    .Build();

return configuracion.GetConnectionString(nombre)
    ?? throw new InvalidOperationException(
        $"Falta la cadena '{nombre}' en {Archivo}.");
```

El arranque no crea ni actualiza la base de datos automáticamente. La pantalla principal consulta si puede conectarse y cuenta categorías y usuarios; la base de datos debe estar previamente preparada para que las consultas de la aplicación funcionen.

## 4. Arranque, autenticación y navegación

### Flujo del proceso

1. `Program.Main` configura la apariencia de Windows Forms.
2. Presenta `FrmLogin` mediante `ShowDialog()`.
3. Si el resultado es `DialogResult.OK`, obtiene `UsuarioAutenticado`.
4. Solo con un usuario autenticado inicia `Form1`.
5. Si el diálogo se cancela o se cierra sin autenticación, no se abre el formulario principal.

La decisión de no abrir la aplicación sin usuario se refleja en este patrón:

```csharp
using (FrmLogin formLogin = new FrmLogin())
{
    if (formLogin.ShowDialog() == DialogResult.OK)
    {
        usuarioAutenticado = formLogin.UsuarioAutenticado;
    }
}

if (usuarioAutenticado != null)
{
    Application.Run(new Form1(usuarioAutenticado));
}
```

### Inicio de sesión

`FrmLogin` recorta espacios del correo, exige correo y contraseña no vacíos, consulta usuarios sin seguimiento de cambios y busca una coincidencia exacta:

```csharp
using var contexto = new AppDbContext();
Usuario? usuario = contexto.Usuarios
    .AsNoTracking()
    .FirstOrDefault(u => u.Correo == correo && u.Password == password);

if (usuario is null)
{
    // Mostrar acceso denegado y permitir corregir la contraseña.
    return;
}

UsuarioAutenticado = usuario;
DialogResult = DialogResult.OK;
Close();
```

Si ocurre un error de base de datos o conexión, el formulario muestra el mensaje de error. Las contraseñas actualmente se comparan como texto plano; no se observa hash ni proceso de autenticación externo.

El modelo inicial de datos define un usuario de tipo Administrador:

| Campo | Valor |
|---|---|
| Nombre | `Administrador Principal` |
| Correo | `admin@tiendaOnline.sv` |
| Contraseña | `Admin123!` |
| Teléfono | `70000000` |
| Rol | `Administrador` |

### Ventana principal

`Form1` recibe el objeto `Usuario` autenticado. Configura los colores, estados de hover y selección de botones, presenta nombre y rol en la barra lateral y abre las pantallas como diálogos modales.

La regla de interfaz para el botón de productos es:

```csharp
bool esAdministrador = string.Equals(
    _usuarioActual.Rol,
    "Administrador",
    StringComparison.OrdinalIgnoreCase);

btnProducto.Enabled = esAdministrador;
sessionLabel.Text = $"{_usuarioActual.Nombre}\n({_usuarioActual.Rol})";
```

Esto solo deshabilita `btnProducto`. El botón `Inicio` también abre `FrmProductos` y no está condicionado por el rol, por lo que la restricción no cubre todas las rutas hacia esa pantalla.

La navegación configurada en el código:

| Elemento | Acción |
|---|---|
| Inicio | Abre `FrmProductos`. |
| Categoría | Abre `FrmCategorias`. |
| Producto | Abre `FrmProductos`. |
| Cliente | Abre `FrmCliente`. |
| Usuarios | Abre `FrmUsuario`. |
| Pedidos | Abre `FrmPedido`, pasando el usuario de la sesión. |
| Detalles de pedido | Abre `FrmDetallePedido`. |
| Envíos | Solo cambia la apariencia del botón seleccionado; no abre `FrmEnvio`. |
| Pagos | Solo cambia la apariencia del botón seleccionado; no abre `FrmPago`. |
| Archivo, Catálogos, Ayuda | Se ven en el menú superior, pero no tienen acciones conectadas en el código revisado. |

Las pantallas que sí se abren desde `Form1` se muestran así:

```csharp
private void OpenForm(Form form, Button selectedButton)
{
    SelectNavigationButton(selectedButton);
    using (form)
    {
        form.ShowDialog(this);
    }
}
```

Mientras el diálogo está abierto, la ventana principal no recibe interacción. En `Form1_Load` se consulta la conexión y se cuentan categorías y usuarios. La excepción de esa rutina se captura sin mostrarla ni registrarla.

## 5. Persistencia con Entity Framework Core

### Contexto

`AppDbContext` representa la sesión de EF Core y expone nueve colecciones `DbSet`: usuarios, clientes, categorías, productos, direcciones, pedidos, detalles, pagos y envíos. Acepta opciones inyectadas, pero cuando no están configuradas usa la cadena leída por `ConfiguracionApp`:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    if (!options.IsConfigured)
    {
        options.UseSqlServer(ConfiguracionApp.ObtenerCadenaConexion());
    }
}
```

En las pantallas y servicios el patrón más frecuente es crear un contexto por operación y liberarlo al terminar:

```csharp
using var ctx = new AppDbContext();
var categorias = ctx.Categorias
    .AsNoTracking()
    .OrderBy(categoria => categoria.Nombre)
    .ToList();
```

`AsNoTracking()` es apropiado para listas de consulta que no se editarán a través del mismo contexto. Para editar, los servicios vuelven a cargar la entidad con seguimiento usando `Find`.

### Relaciones y reglas de borrado

En `OnModelCreating` se definen explícitamente varias relaciones:

- `Cliente` tiene varias `Direccion`. La eliminación de un cliente puede eliminar en cascada sus direcciones.
- `Cliente` tiene varios `Pedido`. La relación usa `Restrict` para proteger el historial.
- `Usuario` tiene varios `Pedido`. También se restringe borrar un usuario referenciado.
- `Pedido` tiene varios `DetallePedido`. Se restringe el borrado de pedidos con detalles.
- `Producto` tiene varios `DetallePedido`. Se restringe borrar productos que forman parte de pedidos.
- `Pedido` puede tener varios `Envio` y `Direccion` varios envíos. Se restringe borrar entidades que estén referenciadas por envíos.

Ejemplo de relación cliente-direcciones:

```csharp
modelBuilder.Entity<Direccion>()
    .HasOne(d => d.Cliente)
    .WithMany(c => c.Direcciones)
    .HasForeignKey(d => d.IdCliente)
    .OnDelete(DeleteBehavior.Cascade);
```

Ejemplo de relación pedido-usuario, que protege pedidos históricos:

```csharp
modelBuilder.Entity<Pedido>()
    .HasOne(p => p.Usuario)
    .WithMany(u => u.Pedidos)
    .HasForeignKey(p => p.IdUsuario)
    .OnDelete(DeleteBehavior.Restrict);
```

El contexto también define el usuario inicial mediante `HasData`. Las reglas de relación de categoría-producto y pedido-pago siguen la configuración por convenciones reflejada en el modelo.

## 6. Modelo de dominio

Las entidades están en `Models`. `Id` es la clave principal convencional; las relaciones usan claves como `IdCliente`, `IdPedido` e `IdProducto` y propiedades de navegación.

| Entidad | Campos importantes | Relaciones |
|---|---|---|
| `Usuario` | Nombre, correo, contraseña, teléfono, rol y fecha de registro. Rol predeterminado: `Vendedor`. | Muchos pedidos. |
| `Cliente` | Nombre, teléfono y correo. | Muchas direcciones y pedidos. |
| `Direccion` | Id de cliente, departamento y ubicación. | Un cliente; varios envíos. |
| `Categoria` | Nombre y descripción opcional. | Muchos productos. |
| `Producto` | Categoría, nombre, descripción, precio, stock, imagen y fecha de publicación. | Una categoría; muchos detalles de pedido. |
| `Pedido` | Cliente, usuario, fecha, estado, costo de envío y total. | Muchos detalles, pagos y envíos. |
| `DetallePedido` | Pedido, producto, cantidad, precio unitario y subtotal. | Un pedido y un producto. |
| `Pago` | Pedido, método, monto, fecha y estado. | Un pedido. |
| `Envio` | Pedido, dirección, empresa, seguimiento, fecha y estado. | Un pedido y una dirección. |

Las anotaciones declaran campos requeridos, longitudes máximas y formatos de correo. Los montos utilizan `decimal(10,2)`. Por ejemplo:

```csharp
[Required]
[MaxLength(100)]
public string Nombre { get; set; }

[Required]
[Column(TypeName = "decimal(10,2)")]
public decimal Precio { get; set; }
```

Las anotaciones documentan y configuran el modelo de EF Core, pero no sustituyen las validaciones adicionales hechas por servicios o formularios. La cobertura de validación no es idéntica en todas las entidades.

## 7. Servicios y operaciones CRUD

Los servicios existentes son `CategoriaService`, `ClienteService` y `ProductosService`. Cada uno administra un contexto por operación, valida reglas del negocio y produce excepciones específicas para que el formulario pueda mostrar el motivo del rechazo.

### Categorías

`CategoriaService` permite listar, consultar, crear, actualizar y eliminar. La validación exige nombre no vacío, longitud máxima de 50 caracteres y descripción de hasta 300:

```csharp
private void ValidarCategoria(Categoria categoria)
{
    if (string.IsNullOrWhiteSpace(categoria.Nombre))
        throw new ArgumentException("El nombre de la categoría es obligatorio.");

    if (categoria.Nombre.Length > 50)
        throw new ArgumentException("El nombre no debe exceder los 50 caracteres.");

    if (!string.IsNullOrEmpty(categoria.Descripcion)
        && categoria.Descripcion.Length > 300)
        throw new ArgumentException("La descripción no puede exceder los 300 caracteres.");
}
```

Antes de crear o actualizar se comprueba que no exista otra categoría con el mismo nombre ignorando mayúsculas/minúsculas. Al actualizar, la consulta excluye el identificador actual. Antes de borrar se comprueba que no existan productos vinculados. La verificación de duplicados en una actualización se realiza así:

```csharp
bool existeNombre = ctx.Categorias.Any(c =>
    c.Nombre.ToLower() == categoria.Nombre.ToLower()
    && c.Id != categoria.Id);
```

La condición de eliminación consulta la relación con productos:

```csharp
bool tieneProductos = ctx.Productos.Any(p => p.IdCategoria == id);
if (tieneProductos)
{
    throw new InvalidOperationException(
        "No se puede eliminar la categoría porque tiene productos vinculados.");
}
```

### Clientes

`ClienteService` permite listar por nombre, buscar por id y ejecutar el CRUD. El nombre es obligatorio y no puede superar 100 caracteres; correo y teléfono son requeridos. Comprueba duplicidad de correo sin distinguir mayúsculas/minúsculas y de teléfono exacto. En actualización no compara contra el mismo cliente. No valida internamente la longitud del teléfono ni el formato del correo: el formato se comprueba en `FrmCliente` con `EmailAddressAttribute`.

Al actualizar, la búsqueda de duplicados excluye el cliente actual:

```csharp
bool existeCorreo = ctx.Clientes.Any(c =>
    c.Correo.ToLower() == cliente.Correo.ToLower()
    && c.Id != cliente.Id);
bool existeTelefono = ctx.Clientes.Any(c =>
    c.Telefono == cliente.Telefono
    && c.Id != cliente.Id);
```

La comprobación de relaciones antes de borrar es:

```csharp
bool tienePedidos = ctx.Pedidos.Any(p => p.IdCliente == id);
if (tienePedidos)
{
    throw new InvalidOperationException(
        "No se puede eliminar el cliente porque tiene pedidos vinculados.");
}
```

### Productos

`ProductosService` lista productos incluyendo su categoría, consulta producto por id, crea, actualiza y elimina. Las validaciones requieren nombre (máximo 100), categoría con id positivo, precio y stock no negativos; la descripción tiene límite de 500 caracteres. En creación y edición también comprueba que exista la categoría y que no exista otro producto con el mismo nombre ignorando mayúsculas/minúsculas.

La lectura relacionada se hace con `Include`:

```csharp
return ctx.Productos
    .Include(producto => producto.Categoria)
    .AsNoTracking()
    .OrderBy(producto => producto.Nombre)
    .ToList();
```

La eliminación no se permite si el producto ya figura en un detalle de pedido:

```csharp
bool tieneVentas = ctx.DetallesPedidos.Any(
    detalle => detalle.IdProducto == id);

if (tieneVentas)
{
    throw new InvalidOperationException(
        "No se puede eliminar el producto porque tiene historial de ventas asociado.");
}
```

### Uso desde las pantallas

`FrmCategorias`, `FrmCliente` y `FrmProductos` llaman los servicios para evitar repetir la persistencia CRUD. Por ejemplo, el formulario de categorías decide si crea o actualiza según si el identificador está vacío:

```csharp
if (string.IsNullOrEmpty(txtId.Text))
{
    _categoriaService.Crear(categoria);
}
else
{
    categoria.Id = int.Parse(txtId.Text);
    _categoriaService.Actualizar(categoria);
}
```

`FrmUsuario`, `FrmPedido`, `FrmDetallePedido`, `FrmPago` y `FrmEnvio` usan `AppDbContext` directamente; no hay servicios equivalentes para esas operaciones.

## 8. Descripción técnica de las pantallas

### `FrmCategorias`

Carga categorías desde `CategoriaService` y enlaza la lista con `dgvCategorias`. Al seleccionar una fila copia `Id`, `Nombre` y `Descripcion` a los campos. `Nuevo` limpia los campos, quita la selección de la grilla y enfoca el nombre; `Cancelar` limpia, pero no cierra esta ventana. Guardar recorta los campos y crea o edita según si hay un id. Después de guardar o borrar, la grilla se vuelve a cargar y el formulario se limpia. Eliminar exige una selección, pide confirmación y muestra la razón si la operación está restringida. La colección de productos no se presenta en la grilla. Los errores al cargar se muestran como mensajes de error.

### `FrmCliente`

Carga los clientes sin seguimiento de cambios, ordenados por nombre e id, y pinta las filas en una grilla manual. Muestra el total de clientes y al seleccionar una fila rellena los campos y conserva el id para distinguir alta (`0`) de edición. `Nuevo` limpia la selección y enfoca el nombre. Al guardar recorta nombre, correo y teléfono y valida el correo con `EmailAddressAttribute` antes de llamar `ClienteService`; este completa la validación y comprueba duplicados. Después de guardar, vuelve a cargar la lista y limpia el formulario. Eliminar exige selección y confirmación; si existen pedidos, presenta la restricción del servicio. `Cancelar` limpia los campos y cierra la ventana. Los errores de carga muestran la excepción interna cuando está disponible.

### `FrmProductos`

Al cargar llena el selector de categorías y la grilla. El primer valor del selector es un marcador sin categoría válida. La grilla presenta nombre de categoría, datos de producto y fecha formateada; oculta los ids, pero conserva el id del producto para la selección. Al seleccionar una fila, vuelve a consultar el producto completo y transfiere los datos al formulario. Al elegir una imagen, guarda temporalmente la ruta original y muestra el nombre del archivo.

Al guardar:

1. Comprueba categoría válida y que el valor de stock sea entero.
2. Si edita, busca el producto actual y conserva la fecha de publicación.
3. Si se seleccionó una imagen, la copia a `Images` con un GUID y cambia el nombre guardado.
4. Si no se seleccionó una imagen al editar, conserva la ruta de imagen existente.
5. Construye el modelo `Producto` y delega alta o edición a `ProductosService`.

La ruta física se genera desde `Application.StartupPath` y apunta a la carpeta `Images` en la raíz del proyecto. En la eliminación el formulario intenta borrar el archivo antes de llamar al servicio; si el servicio impide borrar el producto porque tiene ventas, el archivo podría haber sido borrado aunque el registro permanezca. Es una limitación del orden actual de operaciones.

`Cancelar` limpia los campos y restablece categoría, precio, existencias e imagen; `Nuevo` hace lo mismo y enfoca el selector de categoría. El formulario comprueba que la categoría no sea el marcador y que el valor de stock sea entero. El servicio admite descripción de hasta 500 caracteres, aunque el modelo `Producto` declara un máximo de 255; los textos de 256 a 500 pueden superar la validación del servicio y fallar después al persistir.

### `FrmUsuario`

La pantalla carga la lista en `_usuarios` con `AsNoTracking`, ordenada por nombre, y muestra cantidad de registros. La grilla es de solo lectura y selección de una fila; presenta id, nombre, correo, teléfono y rol. Al seleccionar una fila rellena los campos y deja vacía la contraseña para no mostrarla. Los roles del selector son Administrador y Vendedor, además de un marcador inicial que no se puede guardar.

Guardar recorta nombre, correo y teléfono y exige que estén presentes, que el rol se haya elegido y que el correo tenga formato válido. Busca el usuario por id seleccionado; si no hay selección, busca por correo. Esto hace que guardar sin una fila seleccionada actualice la cuenta si ese correo ya existe o cree una nueva si no existe. Para una cuenta nueva exige contraseña y asigna fecha de registro. Para una cuenta existente solo reemplaza la contraseña cuando el campo no queda vacío. No hay una comprobación explícita de correo duplicado cuando se edita otro usuario seleccionado.

La búsqueda distingue entre la fila seleccionada y el guardado por correo:

```csharp
Usuario? usuario = _usuarioSeleccionadoId.HasValue
    ? db.Usuarios.Find(_usuarioSeleccionadoId.Value)
    : db.Usuarios.FirstOrDefault(item => item.Correo == correo);
```

La decisión de exigir contraseña únicamente en altas y conservar la contraseña existente cuando el campo queda vacío es:

```csharp
bool esNuevo = usuario == null;
if (esNuevo && string.IsNullOrWhiteSpace(password))
{
    // Informar que la contraseña es obligatoria y cancelar el guardado.
    return;
}

usuario ??= new Usuario();
usuario.Nombre = nombre;
usuario.Correo = correo;
usuario.Telefono = telefono;
usuario.Rol = cmbRol.SelectedItem!.ToString()!;
if (!string.IsNullOrWhiteSpace(password))
{
    usuario.Password = password;
}
```

`Nuevo` limpia campos, quita la selección y enfoca el nombre. El texto de contraseña nunca se rellena desde la grilla. Después de guardar se vuelve a cargar la lista y se selecciona el usuario guardado. `Cancelar` cierra la ventana. Eliminar localiza por id seleccionado o correo escrito, pide confirmación y refresca la lista si tiene éxito; una restricción de integridad, como pedidos asociados, se muestra como error.

El archivo de lógica contiene un método `ConfigureVisualLayout()` con ajustes visuales y creación dinámica de paneles, pero el constructor no lo invoca: la ventana usa el diseño definido por `InitializeComponent()`. Por tanto, esos ajustes escritos en el método no se aplican al abrir el formulario.

### `FrmPedido`: composición del pedido

El usuario actual puede recibirse por el constructor. Al cargar, el formulario rellena el selector de clientes, usuarios y productos con stock positivo. El usuario autenticado queda seleccionado en el combo de usuario cuando corresponde. El identificador y la fecha se presentan como campos de solo lectura. Los estados disponibles son Pendiente, Procesando, Completado y Cancelado.

La lista provisional `_detalles` vive en memoria hasta guardar. `Agregar` requiere un producto, acumula cantidades si el mismo producto ya se agregó, usa el precio cargado en el selector como precio unitario, calcula subtotal y restablece el control de cantidad a uno. Antes de aceptar compara la cantidad acumulada con el stock disponible. `Eliminar` quita de la lista provisional el producto de la fila seleccionada; no cambia el stock de la base porque aún no se ha guardado el pedido. `Nuevo` descarta los detalles pendientes, limpia el id, restablece fecha, cantidad, costo de envío y estado.

Al añadir:

```csharp
int cantidadEnPedido = detalleExistente?.Cantidad ?? 0;
if (cantidadEnPedido + cantidad > producto.Stock)
{
    // Mostrar stock insuficiente y no modificar el detalle.
    return;
}

detalleExistente.Cantidad += cantidad;
detalleExistente.SubTotal =
    detalleExistente.Cantidad * detalleExistente.PrecioUnitario;
```

El total se recalcula sumando los subtotales y el costo de envío:

```csharp
decimal totalProductos = _detalles.Sum(detalle => detalle.SubTotal);
lblTotal.Text = $"Total: {totalProductos + numCostoEnvio.Value:C2}";
```

Al guardar se exige cliente, usuario y al menos un detalle. Se abre una transacción, se consultan nuevamente las existencias y se descuenta el stock en la base. Esta segunda comprobación evita basarse solamente en la lista cargada cuando pudo cambiar el inventario:

```csharp
using var db = new AppDbContext();
using var transaction = db.Database.BeginTransaction();
var idsProductos = _detalles
    .Select(detalle => detalle.IdProducto)
    .ToList();
var productosActuales = db.Productos
    .Where(producto => idsProductos.Contains(producto.Id))
    .ToDictionary(producto => producto.Id);

foreach (var detalle in _detalles)
{
    if (!productosActuales.TryGetValue(detalle.IdProducto, out var producto)
        || producto.Stock < detalle.Cantidad)
    {
        throw new InvalidOperationException(
            "El producto ya no tiene stock suficiente.");
    }

    producto.Stock -= detalle.Cantidad;
}

db.Pedidos.Add(pedido);
db.SaveChanges();
transaction.Commit();
```

El pedido guarda fecha actual, estado elegido, costo de envío, total y colección de detalles con precio y subtotal. Si el guardado falla, la transacción se libera sin confirmar el descuento parcial. Tras el registro, el formulario muestra el id del pedido creado, actualiza fecha, limpia el pedido de trabajo y vuelve a cargar el combo de productos para reflejar el stock actualizado. El botón `Cancelar` cierra la pantalla; no existe en este formulario edición ni borrado de pedidos.

### `FrmDetallePedido`: edición de líneas e inventario

Permite escribir un id de pedido y cargarlo con Enter o al salir del campo. Si el formulario no recibe un id inicial, intenta seleccionar el pedido más reciente. Al cambiar el id sin cargarlo, invalida la selección de detalles y limpia los campos para evitar operar sobre un pedido anterior. Al cargar detalles incluye el producto para mostrar su nombre; si existe al menos una fila, selecciona la primera.

El selector incluye todos los productos, incluso los que actualmente tienen stock cero; la disponibilidad se comprueba al guardar. El botón `Nuevo` conserva el pedido cargado, pero prepara un detalle nuevo: vacía la selección, pone id de detalle a cero, cantidad a uno y precio inicial según el producto seleccionado. El precio unitario es editable y cambiar producto, cantidad o precio actualiza el subtotal en pantalla, por lo que se puede guardar un precio distinto del precio de catálogo. El control de cantidad exige mínimo uno; precio y subtotal usan decimales con dos posiciones y el subtotal es de solo lectura. Al seleccionar una línea se cargan sus valores en el formulario.

Al guardar se exige que el pedido indicado ya esté cargado y exista un producto seleccionado. Se vuelve a consultar pedido, detalle y producto dentro de una transacción. Cuando se modifica un detalle existente, se devuelve al stock la cantidad que se había descontado previamente, y después se descuenta la cantidad nueva. Si cambia el producto, primero se restituye el producto anterior:

```csharp
if (detalleExistente != null
    && detalleExistente.IdProducto == idProducto)
{
    int stockDisponible = productoNuevo.Stock + detalleExistente.Cantidad;
    if (cantidad > stockDisponible)
        throw new InvalidOperationException("Stock insuficiente.");

    productoNuevo.Stock = stockDisponible - cantidad;
}
else
{
    if (detalleExistente != null)
    {
        var productoAnterior =
            db.Productos.Find(detalleExistente.IdProducto);
        if (productoAnterior == null)
            throw new InvalidOperationException(
                "No se encontró el producto asociado al detalle actual.");

        productoAnterior.Stock += detalleExistente.Cantidad;
    }

    if (cantidad > productoNuevo.Stock)
        throw new InvalidOperationException("Stock insuficiente.");

    productoNuevo.Stock -= cantidad;
}
```

Tras guardar o borrar, recalcula el total del pedido con la suma de subtotales persistidos más el costo de envío y confirma la transacción. Al eliminar, pide confirmación y devuelve la cantidad a inventario. Si faltan el pedido o producto relacionado, detiene la operación y muestra el error. `Cancelar` cierra el formulario. La pantalla permite modificar líneas existentes y agregar/quitar líneas, pero no modifica aquí el cliente, vendedor, estado o costo de envío del pedido.

### `FrmEnvio`

Al abrir, carga el pedido más reciente y su lista de direcciones, si existe, además de los envíos ordenados por id descendente. La grilla muestra id de envío, id de pedido, empresa, guía y estado. Al seleccionar una fila, llena el formulario, carga las direcciones del pedido y añade a los combos los valores de empresa o estado existentes si ya no están entre las opciones predefinidas. Al escribir un id de pedido, carga las direcciones cuyo `IdCliente` coincide con el cliente del pedido. Las direcciones se muestran como `Departamento, Ubicacion`; Enter o salir del campo confirma/carga el pedido y cambiar el número invalida la dirección mostrada.

Al guardar valida que el pedido se haya cargado, que exista una dirección seleccionada, que se haya elegido empresa y que el número de seguimiento no esté vacío. Vuelve a verificar en base de datos que la dirección corresponda al cliente del pedido:

```csharp
bool direccionPerteneceAlCliente = db.Direcciones.Any(direccion =>
    direccion.Id == idDireccion
    && direccion.IdCliente == pedido.IdCliente);

if (!direccionPerteneceAlCliente)
{
    throw new InvalidOperationException(
        "La dirección seleccionada no pertenece al cliente del pedido.");
}
```

Si el id de envío es positivo, actualiza el envío existente; en caso contrario crea uno. El identificador es de solo lectura. `Nuevo` limpia identificador, pedido, dirección, empresa, guía y fecha; `Cancelar` cierra la pantalla. `Eliminar` solicita confirmación y vuelve a cargar la grilla. Empresas disponibles: DHL Express, FedEx, UPS, Correos de El Salvador y Cargo Expreso. Estados: Preparación, En Tránsito, Entregado y Cancelado.

Al guardar se persisten `IdPedido`, `IdDireccion`, empresa, número de guía, estado y fecha. La validación utiliza el pedido recuperado desde la base y comprueba la dirección contra el cliente de ese pedido, no solo contra el valor mostrado en pantalla.

### `FrmPago`

Carga pagos del más reciente al más antiguo; la grilla muestra id, pedido, método, monto formateado como moneda, estado y fecha. Al seleccionar una fila, rellena los campos, vuelve a comprobar el pedido asociado y conserva el monto del pago seleccionado. Si un método o estado guardado no forma parte de las opciones actuales, lo agrega al combo para poder editar el registro sin perder ese valor.

Al introducir un id de pedido, Enter o salir del campo comprueba que exista y, por defecto, propone su total como monto. Al seleccionar un pago existente se consulta el pedido sin sobrescribir el monto del pago. El monto permanece editable y el programa no exige que coincida con el total del pedido. `Nuevo` quita selección y limpia ids y monto, deja método sin seleccionar, fecha actual y estado Pendiente. Métodos predefinidos: Efectivo, Tarjeta de crédito, Transferencia y PayPal. Estados: Pendiente, Completado, Fallido y Reembolsado.

Guardar exige pedido cargado, método y estado seleccionados. Después busca si el pago seleccionado existe para actualizarlo o crea un nuevo pago:

```csharp
Pago pago;
if (idPago > 0)
{
    pago = db.Pagos.FirstOrDefault(item => item.Id == idPago)
        ?? throw new InvalidOperationException(
            "El pago seleccionado ya no existe.");
}
else
{
    pago = new Pago();
    db.Pagos.Add(pago);
}

pago.IdPedido = idPedido;
pago.Metodo = metodo;
pago.Monto = numMonto.Value;
pago.FechaPago = dtpFechaPago.Value;
pago.Estado = estado;
db.SaveChanges();
```

El formulario puede editar y eliminar pagos, solicitando confirmación antes de borrar.

## 9. Imágenes, recursos y archivos auxiliares

### Imágenes de productos

El diálogo de selección de imagen admite JPG, JPEG, PNG, BMP y WEBP. `FrmProductos` copia la imagen seleccionada a `Images` con un nombre único generado con `Guid`, y persiste ese nombre en `Producto.ImagenUrl`. Al ejecutar el borrado, intenta borrar la imagen asociada del disco.

### Logotipo

`LogoHelper.LoadLogo()` construye la ruta `Images\SivarPOSLogo.png` junto al ejecutable. Si no encuentra ese archivo devuelve `null`; si lo encuentra crea un `Bitmap` a partir de una imagen temporal. Esto permite cerrar el archivo original después de cargarlo:

```csharp
using var source = Image.FromFile(path);
return new Bitmap(source);
```

El archivo de imagen que aparece en el inventario revisado tiene nombre GUID y extensión JPG; representa un multímetro. No es el archivo `SivarPOSLogo.png` buscado por el helper; ese archivo no aparece en los elementos revisados.

### Archivos auxiliares

- `.gitignore` excluye `bin`, `obj` y `.vs`.
- `.gitattributes` aplica normalización automática de finales de línea y contiene ejemplos de configuración de diff y merge.
- `Properties\Resources.Designer.cs` es código generado para acceder a recursos tipados; el archivo serializado que define dichos recursos queda fuera del alcance.
- Los archivos `*.Designer.cs` de formularios contienen la construcción visual y el cableado de eventos generado por Windows Forms.

## 10. Comportamiento actual y límites conocidos

- El arranque no aplica automáticamente las actualizaciones de base de datos.
- La autenticación compara contraseñas de texto plano.
- La restricción de rol de productos solo deshabilita `btnProducto`; el botón `Inicio` también abre la misma pantalla sin aplicar esa condición.
- Los botones de menú de Envíos y Pagos no abren sus formularios, aunque `FrmEnvio` y `FrmPago` sí están implementados.
- No existe una pantalla dedicada a crear o mantener direcciones. El formulario de envíos consume direcciones previamente existentes.
- `FrmPedido` registra pedidos nuevos; no implementa edición o eliminación de pedidos. Los detalles sí se pueden editar por separado.
- Los pagos pueden tener un monto diferente del total del pedido.
- Al guardar un pedido se refresca el stock desde la base, pero el precio unitario procede del producto cargado previamente en el formulario y no se vuelve a consultar antes de guardar.
- La verificación de conexión de `Form1_Load` captura errores sin notificación ni registro.
- Al reemplazar la imagen de un producto, el archivo anterior se elimina antes de confirmar el guardado en la base; si el guardado falla, el producto puede conservar una ruta de imagen que ya no existe. En el borrado ocurre una situación similar si el servicio impide eliminar un producto con ventas.
- Varias reglas se reparten entre anotaciones, servicios y pantallas; no hay un validador central común para todas las entidades.

## 11. Inventario funcional documentado

### Inicio y configuración

- `Program.cs`
- `Form1.cs` y `Form1.Designer.cs`
- `Data\AppDbContext.cs`
- `Config\ConfiguracionApp.cs`
- `appsettings.json`

### Modelos

- `Models\Categoria.cs`
- `Models\Cliente.cs`
- `Models\DetallePedido.cs`
- `Models\Direccion.cs`
- `Models\Envio.cs`
- `Models\Pago.cs`
- `Models\Pedido.cs`
- `Models\Producto.cs`
- `Models\Usuario.cs`

### Servicios

- `Services\CategoriaService.cs`
- `Services\ClienteService.cs`
- `Services\ProductosService.cs`

### Pantallas

- `Forms\FrmCategorias.cs` y su diseñador.
- `Forms\FrmCliente.cs` y su diseñador.
- `Forms\FrmDetallePedido.cs` y su diseñador.
- `Forms\FrmEnvio.cs` y su diseñador.
- `Forms\FrmLogin.cs` y su diseñador.
- `Forms\FrmPago.cs` y su diseñador.
- `Forms\FrmPedido.cs` y su diseñador.
- `Forms\FrmProductos.cs` y su diseñador.
- `Forms\FrmUsuario.cs` y su diseñador.

### Interfaz y recursos

- `UI\LogoHelper.cs`
- `Properties\Resources.Designer.cs`
- `Images\b7b104a4-fa30-444b-9271-9820ea69db56.jpg`
- `.gitignore`
- `.gitattributes`
