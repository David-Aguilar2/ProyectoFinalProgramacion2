using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasOnline.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionUsuariosYEnvio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_IdUsuario",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Clientes_ClienteId",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Usuarios_UsuarioId",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vendedores_IdVendedor",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Vendedores_VendedorId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropIndex(
                name: "IX_Productos_VendedorId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Direcciones_ClienteId",
                table: "Direcciones");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_IdUsuario",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Direcciones");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Direcciones");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "IdVendedor",
                table: "Pedidos",
                newName: "IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_IdVendedor",
                table: "Pedidos",
                newName: "IX_Pedidos_IdUsuario");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Direcciones",
                newName: "IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_UsuarioId",
                table: "Direcciones",
                newName: "IX_Direcciones_IdCliente");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmpresaTransporte",
                table: "Envios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroSeguimiento",
                table: "Envios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Clientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Clientes",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categorias",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Correo", "Rol" },
                values: new object[] { "admin@tiendaOnline.sv", "Administrador" });

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Clientes_IdCliente",
                table: "Direcciones",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Clientes_IdCliente",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_IdUsuario",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EmpresaTransporte",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "NumeroSeguimiento",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Pedidos",
                newName: "IdVendedor");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos",
                newName: "IX_Pedidos_IdVendedor");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Direcciones",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_IdCliente",
                table: "Direcciones",
                newName: "IX_Direcciones_UsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Direcciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Direcciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categorias",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administradores_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendedores_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "Id", "IdUsuario" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Correo",
                value: "admin@mitiandapos.sv");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_VendedorId",
                table: "Productos",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_ClienteId",
                table: "Direcciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdUsuario",
                table: "Clientes",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administradores_IdUsuario",
                table: "Administradores",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_IdUsuario",
                table: "Vendedores",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_IdUsuario",
                table: "Clientes",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Clientes_ClienteId",
                table: "Direcciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Usuarios_UsuarioId",
                table: "Direcciones",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vendedores_IdVendedor",
                table: "Pedidos",
                column: "IdVendedor",
                principalTable: "Vendedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Vendedores_VendedorId",
                table: "Productos",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id");
        }
    }
}
