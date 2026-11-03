# Documentación técnica de VentasOnline

## 1. Propósito y alcance

`VentasOnline` es una aplicación de escritorio de punto de venta para Windows. Permite mantener catálogos de categorías, proveedores, productos, clientes y usuarios, además de registrar pedidos, sus detalles, pagos y envíos.

La interfaz está implementada con Windows Forms en C#. Entity Framework Core accede a SQL Server LocalDB. Esta guía describe el código y el comportamiento que existe en el repositorio; distingue las funciones disponibles de las que todavía no están conectadas a la navegación.

## 2. Tecnologías y requisitos

| Componente | Uso |
|---|---|
| .NET 10 para Windows | Plataforma de compilación y ejecución (`net10.0-windows`). |
| Windows Forms | Formularios de escritorio, controles y diálogos. |
| Entity Framework Core 10 | Mapeo objeto-relacional, consultas y migraciones. |
| SQL Server LocalDB | Base de datos predeterminada indicada por `appsettings.json`. |
| BCrypt.Net-Next | Creación y verificación de hashes en `AuthService`. |
| Microsoft.Extensions.Configuration.Json | Lectura de la cadena de conexión JSON. |

Se necesita Windows, el SDK de .NET 10, SQL Server LocalDB y acceso a la base de datos configurada. Los paquetes NuGet están declarados en `VentasOnline.csproj`.

## 3. Estructura del proyecto

| Ruta | Responsabilidad |
|---|---|
| `Program.cs` | Inicializa Windows Forms, ejecuta la preparación de seguridad y abre el inicio de sesión. |
| `FrmMenu.cs`, `FrmMenu.Designer.cs` | Ventana principal, navegación y datos del usuario en sesión. |
| `Forms/` | Formularios de autenticación, catálogos y operaciones. Los archivos `*.Designer.cs` contienen los controles y el cableado de eventos generado por el diseñador de Windows Forms. |
| `Models/` | Entidades persistidas y relaciones del dominio. |
| `Dtos/LineaPedido.cs` | Elemento de trabajo que la pantalla de pedidos muestra y calcula antes de guardar. |
| `Services/` | Acceso a datos y reglas compartidas para autenticación, catálogos, usuarios y pedidos. |
| `Data/AppDbContext.cs` | `DbSet`, configuración de SQL Server, relaciones y datos iniciales de EF Core. |
| `Data/Seguridad.cs` | Completa contraseñas vacías o prepara un administrador de desarrollo si la tabla no contiene usuarios. |
| `Config/ConfiguracionApp.cs` | Carga la cadena de conexión desde `appsettings.json`. |
| `Migrations/` | Historial de cambios del modelo y snapshot utilizado por EF Core. |
| `Scripts/TiendaOnlineDb.sql` | Script SQL exportado de una versión inicial del esquema; no representa por sí solo el modelo actual completo. |
| `UI/LogoHelper.cs` | Carga el logotipo desde la carpeta de imágenes de la aplicación. |
| `Images/` | Imágenes de productos. |
| `Properties/Resources.resx` | Recursos del proyecto compilados por Windows Forms. |
| `appsettings.json` | Cadena de conexión predeterminada. Se copia al directorio de salida al compilar. |

## 4. Configuración, base de datos y ejecución

### Cadena de conexión

`ConfiguracionApp.ObtenerCadenaConexion` busca primero `appsettings.json` junto al ejecutable. Si no está allí, busca en el directorio de trabajo actual. El archivo y la cadena solicitada son obligatorios; si falta el archivo o la clave, la configuración produce un error.

La configuración que contiene actualmente el repositorio apunta a LocalDB:

```json
{
  "ConnectionStrings": {
    "TiendaOnlineDb": "Server=(localdb)\\MSSQLLocalDB;Database=TiendaOnlineDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
  }
}
```

Para otro servidor se debe cambiar esa cadena por la del entorno de destino. `AppDbContext` acepta `DbContextOptions<AppDbContext>` para recibir opciones desde otro punto de composición, pero el código actual crea habitualmente el contexto sin opciones y usa la configuración anterior.

### Migraciones y arranque

El programa **no** ejecuta `Database.Migrate()` ni crea el esquema al arrancar. La base debe tener las tablas esperadas por el modelo antes de usar la aplicación. `Program.Main` llama `Seguridad.Ejecutar()` antes de mostrar el login; por ello, si no hay conexión o la tabla `Usuarios` no existe, el arranque muestra un error y termina.

Desde la carpeta del proyecto, el flujo habitual para preparar un entorno es:

```powershell
dotnet restore
dotnet ef database update
dotnet build
dotnet run
```

`dotnet ef` requiere que la herramienta de EF Core esté disponible en el entorno. En bases existentes, primero se debe respaldar la información y revisar las migraciones pendientes antes de aplicarlas. En particular, la migración que agrega proveedores asigna inicialmente `IdProveedor = 0` a los productos antiguos y después crea una clave foránea; una base con productos previos necesita que esos productos queden asociados a proveedores válidos antes de aplicar ese cambio, o la restricción puede impedir la migración.

`Scripts/TiendaOnlineDb.sql` representa el esquema de una versión inicial: contiene estructuras antiguas y no incluye el estado final actual de proveedores y usuario/pedido. Para una instalación actual se debe utilizar el modelo y las migraciones de EF Core, no asumir que ese script es equivalente a `AppDbContextModelSnapshot`.

### Compilar y ejecutar

El proyecto está configurado como ejecutable Windows (`WinExe`) con Windows Forms y tipos anulables habilitados:

```xml
<TargetFramework>net10.0-windows</TargetFramework>
<UseWindowsForms>true</UseWindowsForms>
<Nullable>enable</Nullable>
```

No se encontraron proyectos de pruebas automatizadas en la solución inspeccionada. La verificación habitual disponible en el proyecto es compilarlo y probar las operaciones con una base configurada.

## 5. Inicio, autenticación y sesión

### Secuencia de arranque

1. `ApplicationConfiguration.Initialize()` configura la aplicación WinForms.
2. `Seguridad.Ejecutar()` comprueba la tabla de usuarios y prepara contraseñas según las reglas descritas más adelante.
3. `FrmLogin` se muestra como diálogo. Si se cancela o se agotan los intentos, el proceso termina.
4. `FrmLogin` inicia el estado estático `Sesion` al autenticar correctamente.
5. Se abre `FrmMenu`; el formulario usa el usuario recibido o, si es `null`, recupera `Sesion.UsuarioActual`.

El constructor del menú aplica esta alternativa de usuario:

```csharp
public FrmMenu(Usuario usuarioActual)
{
    _usuarioActual = usuarioActual ?? Sesion.UsuarioActual;
    InitializeComponent();
    ConfigurePermissions();
}
```

En `Program.Main`, la variable local que se entrega al menú no se asigna desde `FrmLogin.UsuarioAutenticado`. El flujo actual funciona por el estado compartido que `FrmLogin` establece mediante `Sesion.Iniciar`; conservar ambas formas de pasar al usuario evitaría depender únicamente de estado estático.

### Verificación de credenciales

El login recorta espacios del correo, pero no de la contraseña. Tiene un máximo de tres intentos fallidos y presenta un mensaje genérico, sin distinguir si falló el correo o la contraseña. En la autenticación se busca al usuario por correo y BCrypt verifica la contraseña contra el hash guardado:

```csharp
var usuario = db.Usuarios
    .AsNoTracking()
    .FirstOrDefault(u => u.Correo == correo);

if (usuario == null || string.IsNullOrEmpty(usuario.Password))
    return null;

bool valida = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
return valida ? usuario : null;
```

`AuthService.GenerarHash` también ofrece la operación para generar hashes y utiliza un factor de trabajo de 11. Sin embargo, la pantalla `FrmUsuario` guarda directamente el texto de la contraseña y no llama ese método. Además, `AppDbContext` tiene un usuario semilla con contraseña en texto plano. Ambos caminos son incompatibles con la verificación BCrypt: las credenciales creadas por esos caminos pueden no permitir iniciar sesión. Esto debe corregirse antes de usar las cuentas como credenciales reales; la contraseña semilla no se reproduce en esta guía.

### Preparación de seguridad

`Seguridad.Ejecutar` tiene dos comportamientos:

- Si `Usuarios` está vacía, agrega un administrador local con una contraseña temporal de desarrollo almacenada como hash BCrypt.
- A los usuarios existentes cuyo campo de contraseña está vacío les asigna el hash de esa contraseña temporal.

Este procedimiento no migra a BCrypt las contraseñas que ya contengan texto plano u otro valor no vacío. La contraseña temporal está escrita en el código y debe considerarse exclusiva del entorno de desarrollo; no se debe usar como contraseña de despliegue.

### Estado de sesión y permisos

`Sesion` almacena `UsuarioActual`, ofrece `Iniciar` y `Cerrar`, y expone `EsAdministrador`. `FrmMenu.ConfigurePermissions` usa comparación de rol sin distinguir mayúsculas y minúsculas, muestra nombre y rol y deshabilita el botón de productos para usuarios que no sean administradores.

La protección es solo visual: no hay autorización común en los servicios, y las pantallas pueden ser instanciadas desde código. Tampoco todos los accesos a una función están condicionados por rol en un mismo punto. `logoutButton_Click` cierra la ventana principal; no invoca `Sesion.Cerrar()`.

## 6. Persistencia con Entity Framework Core

### Contexto y consultas

`AppDbContext` expone estas colecciones:

```csharp
public DbSet<Usuario> Usuarios { get; set; }
public DbSet<Cliente> Clientes { get; set; }
public DbSet<Categoria> Categorias { get; set; }
public DbSet<Proveedores> Proveedores { get; set; }
public DbSet<Producto> Productos { get; set; }
public DbSet<Direccion> Direcciones { get; set; }
public DbSet<Pedido> Pedidos { get; set; }
public DbSet<DetallePedido> DetallesPedidos { get; set; }
public DbSet<Pago> Pagos { get; set; }
public DbSet<Envio> Envios { get; set; }
```

Si no recibió opciones externas, configura SQL Server con `ConfiguracionApp`. Los servicios y formularios suelen crear un contexto por operación y liberarlo con `using`. Para listas de solo lectura se usa `AsNoTracking`; `Include` carga explícitamente relaciones que se necesitan para mostrar, como la categoría o proveedor del producto:

```csharp
using var ctx = new AppDbContext();

var productos = ctx.Productos
    .Include(p => p.Categoria)
    .Include(p => p.Proveedor)
    .AsNoTracking()
    .OrderBy(p => p.Nombre)
    .ToList();
```

### Relaciones y borrado

| Relación | Cardinalidad | Borrado configurado |
|---|---|---|
| Cliente → Direcciones | Un cliente tiene varias direcciones. | Cascada en EF. |
| Cliente → Pedidos | Un cliente tiene varios pedidos. | Restringido para proteger historial. |
| Usuario → Pedidos | Un usuario puede registrar varios pedidos. | Restringido. |
| Categoría → Productos | Una categoría tiene varios productos. | Cascada por convención/configuración del modelo. |
| Proveedor → Productos | Un proveedor tiene varios productos. | Cascada en el modelo; el servicio bloquea el borrado si hay productos. |
| Pedido → Detalles | Un pedido tiene varias líneas. | Restringido. |
| Producto → Detalles | Un producto puede aparecer en varias líneas. | Restringido. |
| Pedido → Envíos | Un pedido puede tener envíos. | Restringido. |
| Dirección → Envíos | Una dirección puede usarse en varios envíos. | Restringido. |
| Pedido → Pagos | Un pedido puede tener pagos. | Cascada por convención. |

Ejemplo explícito de protección del historial de pedidos:

```csharp
modelBuilder.Entity<Pedido>()
    .HasOne(p => p.Cliente)
    .WithMany(c => c.Pedidos)
    .HasForeignKey(p => p.IdCliente)
    .OnDelete(DeleteBehavior.Restrict);
```

La base aplica además restricciones de claves foráneas. Las pantallas y servicios agregan verificaciones antes de borrar, pero una operación puede igualmente ser rechazada por una referencia existente.

## 7. Modelo de dominio

Las clases se encuentran en `Models/`. `Id` se reconoce como clave primaria por convención; los campos `Id...` y las propiedades de navegación representan relaciones. Las anotaciones (`Required`, `MaxLength`, `EmailAddress`, `Column`) configuran metadatos/columnas, pero no implican que todos los formularios validen todas las reglas de igual manera.

| Entidad | Datos principales | Relaciones |
|---|---|---|
| `Usuario` | Nombre, correo, contraseña, teléfono, rol y fecha de registro. Rol predeterminado: `Vendedor`. | Muchos pedidos. |
| `Cliente` | Nombre, teléfono y correo. | Muchas direcciones y pedidos. |
| `Categoria` | Nombre y descripción opcional. | Muchos productos. |
| `Proveedores` | Nombre, teléfono y correo. | Muchos productos. |
| `Producto` | Categoría, proveedor, nombre, descripción, precio, stock, imagen y fecha de publicación. | Una categoría y un proveedor; muchos detalles de pedido. |
| `Direccion` | Cliente, departamento y ubicación. | Un cliente; varios envíos. |
| `Pedido` | Cliente, usuario responsable, fecha, estado, costo de envío y total. | Muchos detalles, pagos y envíos. |
| `DetallePedido` | Pedido, producto, cantidad, precio unitario y subtotal. | Un pedido y un producto. |
| `Pago` | Pedido, método, monto, fecha y estado. | Un pedido. |
| `Envio` | Pedido, dirección, empresa de transporte, número de seguimiento, fecha y estado. | Un pedido y una dirección. |

Los importes se asignan como `decimal(10,2)`. Una línea calcula su subtotal multiplicando la cantidad por el precio:

```csharp
public class LineaPedido
{
    public int ProductoId { get; set; }
    public string Producto { get; set; } = "";
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnitario;
}
```

`LineaPedido` es un DTO de trabajo de la interfaz, no una entidad persistida. Al guardar, el servicio convierte esas líneas a `DetallePedido`.

## 8. Servicios y reglas de negocio

Los servicios encapsulan las operaciones de datos compartidas. Un patrón común es validar, abrir un contexto, consultar la entidad, cambiar propiedades y llamar `SaveChanges()`:

```csharp
using var ctx = new AppDbContext();
var entidad = ctx.Categorias.Find(id)
    ?? throw new InvalidOperationException("La categoría no existe.");

entidad.Nombre = categoria.Nombre;
entidad.Descripcion = categoria.Descripcion;
ctx.SaveChanges();
```

### `CategoriaService`

- `ObtenerTodas` devuelve categorías sin seguimiento ordenadas por nombre; `ObtenerPorId` busca por clave.
- `Crear` y `Actualizar` exigen nombre no vacío, máximo 50 caracteres y descripción de máximo 300. También impiden nombres repetidos ignorando mayúsculas/minúsculas.
- `Eliminar` rechaza la operación si hay productos asociados.

Ejemplo de regla de unicidad para una edición:

```csharp
bool existeNombre = ctx.Categorias.Any(c =>
    c.Nombre.ToLower() == categoria.Nombre.ToLower()
    && c.Id != categoria.Id);
```

Las pantallas de categorías, proveedores y clientes siguen el mismo patrón CRUD: construyen una entidad a partir de los controles y usan el id seleccionado para decidir entre crear o actualizar. El ejemplo de `FrmCategorias` de la sección 9 representa también ese flujo común; cada servicio aplica las reglas específicas de su entidad.

### `ClienteService`

- Lista clientes por nombre, busca por id y expone crear, actualizar y eliminar.
- Exige nombre, teléfono y correo no vacíos; limita el nombre a 100 caracteres.
- Evita correos duplicados sin distinguir mayúsculas/minúsculas y teléfonos duplicados exactos.
- No comprueba el formato del correo ni una longitud máxima de teléfono en el servicio. `FrmCliente` valida el formato del correo antes de llamar al servicio.
- No elimina clientes con pedidos.

### `ProveedoresService`

- Lista por nombre, busca por id y ofrece operaciones CRUD.
- Exige nombre y correo, limita el nombre a 100, correo a 150 y, si se proporciona, teléfono a 20 caracteres.
- Impide duplicar nombre o correo ignorando mayúsculas/minúsculas.
- Impide eliminar proveedores asociados a productos.

### `ProductosService`

- Lista los productos incluyendo categoría y proveedor; puede buscar un producto por id.
- Comprueba nombre obligatorio (máximo 100), id de categoría positivo, precio y stock no negativos, y descripción con máximo de 500 caracteres.
- Comprueba que exista la categoría y que no exista otro producto con el mismo nombre ignorando mayúsculas/minúsculas.
- Impide eliminar productos que aparezcan en detalles de pedidos.

La entidad `Producto` limita `Descripcion` a 255 caracteres, aunque el servicio permite hasta 500. Por ello, textos que superen 255 pueden pasar la validación del servicio pero fallar al guardarse en el esquema actual.

La pantalla valida que se seleccione un proveedor y el modelo requiere `IdProveedor`, pero `ProductosService.Crear` no comprueba explícitamente la existencia del proveedor antes de guardar. Además, `Actualizar` no copia `IdProveedor` al producto cargado: seleccionar un proveedor distinto en la interfaz no cambia la relación en base de datos.

### `UsuarioService`

Incluye métodos CRUD con validaciones básicas de nombre, correo y teléfono y comprobación de correo repetido. No lo utiliza `FrmUsuario`, que opera directamente con `AppDbContext`. El servicio tampoco implementa adecuadamente el guardado de todos los campos del formulario: `Actualizar` solo copia nombre y correo, y la validación de pedidos en `Eliminar` consulta `IdCliente` en vez de `IdUsuario`. No se debe tratar este servicio como la ruta funcional completa de gestión de usuarios.

### `PedidoService`

`FrmPedido` usa este servicio para cargar clientes, usuarios, productos con stock, pedidos e información detallada, y para registrar, actualizar o eliminar pedidos.

- `Registrar` requiere al menos una línea, vuelve a leer los productos, comprueba el stock, descuenta inventario, crea los detalles con precio de catálogo y registra el pedido.
- `Actualizar` devuelve al inventario las cantidades de las líneas anteriores, las reemplaza por las líneas nuevas y descuenta el stock de estas.
- `Eliminar` elimina las líneas y el pedido y devuelve las cantidades al inventario.
- `SaveChanges()` es quien persiste las modificaciones; no se declara una transacción explícita en el servicio.

La actualización del pedido reemplaza cliente, usuario, detalles y total, pero no actualiza estado ni costo de envío. El total que calcula el servicio al registrar o actualizar es la suma de subtotales.

Antes de descontar inventario, `CrearDetalles` comprueba el stock leído de la base de datos. El ejemplo resume esa regla:

```csharp
if (producto.Stock < linea.Cantidad)
{
    throw new InvalidOperationException("Stock insuficiente.");
}

producto.Stock -= linea.Cantidad;
```

## 9. Formularios y funcionamiento de la interfaz

### Login — `FrmLogin`

Limpia el mensaje al abrir, permite mostrar/ocultar la contraseña y solicita correo y contraseña. Una autenticación válida llama `Sesion.Iniciar` y cierra con `DialogResult.OK`. Tras tres intentos fallidos establece `DialogResult.Cancel`; los errores de acceso a datos se muestran como error en pantalla.

### Menú principal — `FrmMenu`

Los formularios funcionales se abren como diálogos modales mediante `ShowDialog(this)`. El método también cambia el color del botón seleccionado:

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

La pantalla muestra nombre y rol del usuario. La consulta de conexión y conteos ejecutada al cargar captura excepciones sin mostrarlas ni registrarlas; por eso un fallo allí no se comunica al usuario.

### Categorías — `FrmCategorias`

Carga `CategoriaService.ObtenerTodas()` en una grilla. Seleccionar una fila copia id, nombre y descripción al formulario. `Nuevo` limpia la selección y prepara un alta; `Guardar` determina alta o edición por `_idSeleccionado`; `Eliminar` requiere selección y confirmación; `Cancelar` limpia y cierra.

El evento de guardar delega al servicio, que centraliza las validaciones:

```csharp
if (_idSeleccionado == 0)
{
    _categoriaService.Crear(categoria);
}
else
{
    categoria.Id = _idSeleccionado;
    _categoriaService.Actualizar(categoria);
}
```

### Proveedores — `FrmProveedores`

Muestra el listado, oculta la navegación `Productos` si aparece en la grilla y permite seleccionar, crear, editar y eliminar proveedores. El formulario recorta los campos y delega al `ProveedoresService`; el servicio valida valores duplicados y evita borrar un proveedor usado por productos. Las operaciones de borrado solicitan confirmación. `Cancelar` limpia y cierra.

### Clientes — `FrmCliente`

Consulta y pinta los clientes en una grilla manual, ordenados por nombre e id. La selección carga nombre, teléfono y correo; el conteo se presenta en pantalla. El formulario recorta los campos, comprueba el formato de correo con `EmailAddressAttribute` y delega duplicados y persistencia a `ClienteService`. No permite borrar clientes que tengan pedidos. `Nuevo` limpia para crear y `Cancelar` limpia y cierra.

### Productos — `FrmProductos`

Al cargar obtiene categorías y proveedores para sus combos, agrega a ambos una opción inicial no válida y muestra los productos con nombre de categoría y proveedor. Los ids se ocultan en la grilla. Seleccionar una fila carga la entidad y sus datos al formulario.

Al elegir una imagen se guarda temporalmente la ruta original. Al guardar:

1. Exige una categoría y un proveedor válidos.
2. Al crear, copia la imagen elegida con nombre GUID a `Images`; al editar, conserva la imagen existente si no se elige una nueva.
3. Mantiene la fecha de publicación al editar.
4. Delega alta o edición a `ProductosService`.

Ejemplo de nombre único para la copia:

```csharp
string nombreArchivoUnico = $"{Guid.NewGuid()}{extension}";
string rutaDestinoCompleta = Path.Combine(carpetaDestino, nombreArchivoUnico);
File.Copy(rutaOrigen, rutaDestinoCompleta, true);
```

El formulario borra el archivo de imagen anterior antes de confirmar la edición en la base. También intenta borrar el archivo al iniciar la eliminación antes de preguntar al servicio si existen ventas. Si falla el guardado o el servicio rechaza el borrado, el archivo puede quedar eliminado mientras el registro todavía lo referencia.

### Usuarios — `FrmUsuario`

Lista usuarios ordenados por nombre y permite seleccionar, crear, editar y eliminar. La contraseña nunca se rellena en la grilla; para un usuario existente, dejar el campo vacío conserva el valor persistido. Para uno nuevo se exige una contraseña. Nombre, correo, teléfono y rol son obligatorios en la interfaz, y el correo se valida con `EmailAddressAttribute`.

La pantalla consulta y guarda directamente con `AppDbContext`; no usa `UsuarioService`. La contraseña se asigna literalmente desde el campo, sin llamar a `AuthService.GenerarHash`, lo que causa la incompatibilidad BCrypt descrita en la sección de autenticación. La restricción de claves foráneas puede impedir borrar usuarios vinculados con pedidos.

El comportamiento de asignación actual es:

```csharp
if (!string.IsNullOrWhiteSpace(password))
{
    usuario.Password = password;
}
```

En una modificación, el bloque solo se ejecuta si se escribió una contraseña nueva; el valor que se asigna sigue siendo texto plano.

### Pedidos — `FrmPedido`

Carga clientes, usuarios y productos con stock disponible. El combo de usuario está deshabilitado y se selecciona a partir del usuario de sesión. `dgvPedidos` muestra historial con id, fecha, cliente y total; al seleccionar un pedido, carga sus líneas y habilita la eliminación.

Las líneas nuevas se mantienen en una `BindingList<LineaPedido>` hasta guardar. Añadir producto suma la cantidad a la línea existente de ese producto o crea una nueva con el precio de catálogo:

```csharp
var existente = _lineas.FirstOrDefault(l => l.ProductoId == producto.Id);
if (existente != null)
{
    existente.Cantidad += cantidad;
}
else
{
    _lineas.Add(new LineaPedido
    {
        ProductoId = producto.Id,
        Producto = producto.Nombre,
        Cantidad = cantidad,
        PrecioUnitario = producto.Precio
    });
}
```

El total visible suma `LineaPedido.Subtotal`. Guardar registra un pedido nuevo o actualiza el seleccionado; eliminar solicita confirmación y repone el stock usando `PedidoService`. La selección de una línea también intenta cargar la imagen del producto. En la actualización no se editan estado ni costo de envío desde esta pantalla.

### Detalles de pedido — `FrmDetallePedido`

Carga el catálogo de productos y permite consultar líneas para un id de pedido. Al abrir sin id inicial, intenta elegir el pedido de id más alto. Es necesario cargar un pedido existente antes de guardar; Enter o salir del campo carga sus detalles.

Se puede agregar una línea o editar la seleccionada. Cambiar producto propone el precio de catálogo, pero el precio unitario es editable. Guardar usa una transacción de base de datos: al editar repone la cantidad anterior y descuenta la nueva; si cambia el producto, devuelve el stock al producto anterior. El subtotal se calcula como cantidad por precio y el total del pedido se recalcula sumando los detalles más el costo de envío.

Al guardar una línea existente, la operación agrupa la actualización de stock, detalle y total para confirmar los cambios juntos:

```csharp
using var transaccion = db.Database.BeginTransaction();
productoNuevo.Stock -= cantidad;
detalleExistente.Cantidad = cantidad;
detalleExistente.PrecioUnitario = precioUnitario;
detalleExistente.SubTotal = cantidad * precioUnitario;
db.SaveChanges();
pedido.Total = db.DetallesPedidos
    .Where(detalle => detalle.IdPedido == idPedido)
    .Sum(detalle => detalle.SubTotal) + pedido.CostoEnvio;
db.SaveChanges();
transaccion.Commit();
```

Al eliminar una línea, la pantalla confirma la acción, devuelve la cantidad al inventario, elimina el detalle y recalcula el total en una transacción. Esta pantalla modifica líneas e inventario; no cambia el cliente, usuario, estado o costo de envío del pedido.

### Pagos — `FrmPago`

Carga los pagos por id descendente y, si existe, propone el pedido más reciente. Al cargar un pedido válido propone su total como monto; al seleccionar un pago existente conserva el monto de ese pago. Método y estado se eligen de listas predefinidas, pero un valor histórico que no figure en ellas se agrega al combo para permitir editarlo.

Guardar exige que se haya cargado un pedido existente y que se seleccionen método y estado; según el id de pago crea o actualiza. El monto es editable y no se valida contra el total del pedido. Eliminar requiere confirmación. Esta pantalla está implementada, pero el botón Pagos del menú principal no la abre.

El guardado asigna al pago el pedido, método, monto, fecha y estado seleccionados:

```csharp
pago.IdPedido = idPedido;
pago.Metodo = metodo;
pago.Monto = numMonto.Value;
pago.FechaPago = dtpFechaPago.Value;
pago.Estado = estado;
db.SaveChanges();
```

### Envíos — `FrmEnvio`

Carga los envíos por id descendente y propone el pedido más reciente. Al cargar un pedido obtiene solo direcciones de su cliente. Antes de guardar valida que el pedido se haya cargado, que exista una dirección, empresa de transporte y número de seguimiento, y vuelve a comprobar en base de datos que la dirección corresponda al cliente del pedido:

```csharp
bool direccionPerteneceAlCliente = db.Direcciones.Any(direccion =>
    direccion.Id == idDireccion
    && direccion.IdCliente == pedido.IdCliente);
```

Si existe un id de envío, actualiza; de otro modo, inserta. El formulario también permite eliminar con confirmación. La lista de empresas y estados está definida en el código. No existe una pantalla de mantenimiento de direcciones en la navegación actual. El botón Envíos del menú solo cambia su estado visual y no abre este formulario.

## 10. Navegación disponible actualmente

| Elemento | Comportamiento |
|---|---|
| Categoría | Abre `FrmCategorias`. |
| Proveedores | Abre `FrmProveedores`. |
| Producto | Abre `FrmProductos`; el botón se deshabilita para roles distintos de Administrador. |
| Cliente | Abre `FrmCliente`. |
| Usuarios | Abre `FrmUsuario`. |
| Pedidos | Abre `FrmPedido` y pasa el usuario de sesión. |
| Detalles de pedido | Abre `FrmDetallePedido`, también desde el submenú Operaciones. |
| Pagos | Cambia la apariencia del botón, pero no abre `FrmPago`. |
| Envíos | Cambia la apariencia del botón, pero no abre `FrmEnvio`. |
| Archivo, Catálogos y Ayuda | Se muestran en la barra superior sin acciones conectadas en el código observado. |

## 11. Imágenes, recursos y archivos auxiliares

`FrmProductos` mantiene las imágenes de productos como archivos y guarda en el campo `ImagenUrl` el nombre generado, no los bytes de la imagen. La selección de imagen copia el archivo a una carpeta `Images` calculada desde la ruta de ejecución.

`LogoHelper.LoadLogo()` busca `Images/SivarPOSLogo.png` junto al ejecutable. Si no existe, devuelve `null`; si existe, crea una copia en memoria para que el archivo original no quede bloqueado. El proyecto declara copiar ese PNG al directorio de salida, pero en el inventario actual de `Images/` no aparece un archivo con ese nombre, por lo que el helper puede devolver `null`.

Los archivos `.resx` y `*.Designer.cs` contienen recursos y controles generados para WinForms. Normalmente se editan mediante el diseñador de Visual Studio, no a mano.

## 12. Migraciones y evolución del esquema

`Migrations/` conserva las migraciones y `AppDbContextModelSnapshot.cs` describe el modelo que EF Core considera actual. Las migraciones forman parte del historial de base de datos; no son formularios ni se ejecutan automáticamente al abrir la aplicación.

Algunas migraciones presentes tienen métodos `Up` y `Down` vacíos. La migración de relación proveedor-producto agrega la tabla `Proveedores`, agrega `IdProveedor` a `Productos` y crea una clave foránea. Por esto, el SQL de `Scripts/TiendaOnlineDb.sql`, que proviene de la creación inicial, no debe confundirse con el esquema final.

El usuario inicial configurado mediante `HasData` en `AppDbContext` conserva una contraseña de texto plano, aunque el login actual espera un hash BCrypt. Las migraciones que aplican esa semilla pueden, por tanto, dejar una cuenta incompatible con el mecanismo de login. También debe revisarse la preparación de contraseñas antes de entregar una base a usuarios.

## 13. Limitaciones y puntos importantes del comportamiento actual

1. **Contraseñas:** `AuthService` verifica BCrypt, pero el formulario de usuarios y el usuario semilla guardan/definen texto sin BCrypt. `Seguridad` solo convierte contraseñas vacías, no cualquier contraseña existente.
2. **Autorización:** el menú deshabilita Productos según rol, pero no hay una autorización central por operación o servicio.
3. **Proveedores en edición de producto:** el formulario permite seleccionar proveedor, pero `ProductosService.Actualizar` no asigna `IdProveedor`.
4. **Longitud de descripción:** el servicio permite 500 caracteres y el modelo/columna de producto limita a 255.
5. **Archivos de imagen:** el borrado y reemplazo de imágenes ocurre antes de saber si la operación de base de datos tendrá éxito.
6. **Pagos y envíos:** sus formularios existen, pero los botones del menú no están conectados a ellos.
7. **Direcciones:** los envíos requieren direcciones existentes, pero no hay una pantalla de mantenimiento disponible desde el menú.
8. **Pedidos:** el formulario de pedidos permite editar y eliminar pedidos además de crearlos; esas operaciones pueden reponer o descontar stock. El detalle se puede editar por separado.
9. **Precio de venta:** `PedidoService` toma el precio consultado al cargar el producto en la pantalla; la consulta de inventario vuelve a validar stock al guardar, pero no vuelve a validar el precio.
10. **Edición de pedido:** el servicio reemplaza las líneas y recalcula total, pero no edita estado ni costo de envío.

## 14. Guía breve para extender el proyecto

- Agregar una entidad nueva requiere crear el modelo, exponer un `DbSet` en `AppDbContext`, configurar relaciones si no bastan las convenciones, generar una migración y aplicarla.
- Para un CRUD coherente, mantener la validación y reglas de negocio en un servicio reutilizable y dejar al formulario la lectura de controles, mensajes y selección.
- Usar `AsNoTracking()` en lecturas que no se van a modificar dentro del mismo contexto; usar una entidad con seguimiento para actualizar y llamar a `SaveChanges()`.
- Para operaciones que ajusten más de un registro relacionado (por ejemplo, pedido y stock), agrupar los cambios en una transacción y volver a validar las condiciones en base de datos.
- No guardar contraseñas directamente. Generar y verificar hashes mediante `AuthService`, y actualizar de forma consistente el seed y las cuentas existentes.
- Validar las longitudes y relaciones tanto en formulario como en servicio/modelo para que la regla de interfaz no permita valores que el esquema rechazará.
