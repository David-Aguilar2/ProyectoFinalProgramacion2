IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Categorias] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(50) NOT NULL,
        [Descripcion] nvarchar(300) NOT NULL,
        CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Usuarios] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(100) NOT NULL,
        [Correo] nvarchar(100) NOT NULL,
        [Password] nvarchar(255) NOT NULL,
        [Telefono] nvarchar(15) NOT NULL,
        [FechaRegistro] datetime2 NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Administradores] (
        [Id] int NOT NULL IDENTITY,
        [IdUsuario] int NOT NULL,
        CONSTRAINT [PK_Administradores] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Administradores_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Clientes] (
        [Id] int NOT NULL IDENTITY,
        [IdUsuario] int NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Clientes_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Vendedores] (
        [Id] int NOT NULL IDENTITY,
        [IdUsuario] int NOT NULL,
        CONSTRAINT [PK_Vendedores] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Vendedores_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Direcciones] (
        [Id] int NOT NULL IDENTITY,
        [IdUsuario] int NOT NULL,
        [UsuarioId] int NOT NULL,
        [Departamento] nvarchar(100) NOT NULL,
        [Ubicacion] nvarchar(255) NOT NULL,
        [ClienteId] int NULL,
        CONSTRAINT [PK_Direcciones] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Direcciones_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]),
        CONSTRAINT [FK_Direcciones_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Pedidos] (
        [Id] int NOT NULL IDENTITY,
        [IdCliente] int NOT NULL,
        [IdVendedor] int NOT NULL,
        [Fecha] datetime2 NOT NULL,
        [Estado] nvarchar(20) NOT NULL,
        [CostoEnvio] decimal(10,2) NOT NULL,
        [Total] decimal(10,2) NOT NULL,
        CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Pedidos_Clientes_IdCliente] FOREIGN KEY ([IdCliente]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Pedidos_Vendedores_IdVendedor] FOREIGN KEY ([IdVendedor]) REFERENCES [Vendedores] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Productos] (
        [Id] int NOT NULL IDENTITY,
        [IdCategoria] int NOT NULL,
        [CategoriaId] int NOT NULL,
        [Nombre] nvarchar(100) NOT NULL,
        [Descripcion] nvarchar(255) NOT NULL,
        [Precio] decimal(10,2) NOT NULL,
        [Stock] int NOT NULL,
        [ImagenUrl] nvarchar(255) NOT NULL,
        [FechaPublicacion] datetime2 NOT NULL,
        [VendedorId] int NULL,
        CONSTRAINT [PK_Productos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Productos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Productos_Vendedores_VendedorId] FOREIGN KEY ([VendedorId]) REFERENCES [Vendedores] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Envios] (
        [Id] int NOT NULL IDENTITY,
        [IdPedido] int NOT NULL,
        [IdDireccion] int NOT NULL,
        [FechaEnvio] datetime2 NOT NULL,
        [Estado] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Envios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Envios_Direcciones_IdDireccion] FOREIGN KEY ([IdDireccion]) REFERENCES [Direcciones] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Envios_Pedidos_IdPedido] FOREIGN KEY ([IdPedido]) REFERENCES [Pedidos] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [Pagos] (
        [Id] int NOT NULL IDENTITY,
        [IdPedido] int NOT NULL,
        [PedidoId] int NOT NULL,
        [Metodo] nvarchar(50) NOT NULL,
        [Monto] decimal(10,2) NOT NULL,
        [FechaPago] datetime2 NOT NULL,
        [Estado] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Pagos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Pagos_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE TABLE [DetallesPedidos] (
        [Id] int NOT NULL IDENTITY,
        [IdPedido] int NOT NULL,
        [IdProducto] int NOT NULL,
        [Cantidad] int NOT NULL,
        [PrecioUnitario] decimal(10,2) NOT NULL,
        [SubTotal] decimal(10,2) NOT NULL,
        CONSTRAINT [PK_DetallesPedidos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DetallesPedidos_Pedidos_IdPedido] FOREIGN KEY ([IdPedido]) REFERENCES [Pedidos] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DetallesPedidos_Productos_IdProducto] FOREIGN KEY ([IdProducto]) REFERENCES [Productos] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Correo', N'FechaRegistro', N'Nombre', N'Password', N'Telefono') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
        SET IDENTITY_INSERT [Usuarios] ON;
    EXEC(N'INSERT INTO [Usuarios] ([Id], [Correo], [FechaRegistro], [Nombre], [Password], [Telefono])
    VALUES (1, N''admin@mitiandapos.sv'', ''2026-01-01T00:00:00.0000000'', N''Administrador Principal'', N''Admin123!'', N''70000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Correo', N'FechaRegistro', N'Nombre', N'Password', N'Telefono') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
        SET IDENTITY_INSERT [Usuarios] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IdUsuario') AND [object_id] = OBJECT_ID(N'[Administradores]'))
        SET IDENTITY_INSERT [Administradores] ON;
    EXEC(N'INSERT INTO [Administradores] ([Id], [IdUsuario])
    VALUES (1, 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IdUsuario') AND [object_id] = OBJECT_ID(N'[Administradores]'))
        SET IDENTITY_INSERT [Administradores] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Administradores_IdUsuario] ON [Administradores] ([IdUsuario]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Clientes_IdUsuario] ON [Clientes] ([IdUsuario]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_DetallesPedidos_IdPedido] ON [DetallesPedidos] ([IdPedido]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_DetallesPedidos_IdProducto] ON [DetallesPedidos] ([IdProducto]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Direcciones_ClienteId] ON [Direcciones] ([ClienteId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Direcciones_UsuarioId] ON [Direcciones] ([UsuarioId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Envios_IdDireccion] ON [Envios] ([IdDireccion]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Envios_IdPedido] ON [Envios] ([IdPedido]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Pagos_PedidoId] ON [Pagos] ([PedidoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Pedidos_IdCliente] ON [Pedidos] ([IdCliente]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Pedidos_IdVendedor] ON [Pedidos] ([IdVendedor]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Productos_CategoriaId] ON [Productos] ([CategoriaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Productos_VendedorId] ON [Productos] ([VendedorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Vendedores_IdUsuario] ON [Vendedores] ([IdUsuario]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260927205709_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260927205709_InitialCreate', N'10.0.12');
END;

COMMIT;
GO

