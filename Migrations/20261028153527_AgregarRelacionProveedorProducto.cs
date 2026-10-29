using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasOnline.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRelacionProveedorProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProveedor",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdProveedor",
                table: "Productos",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_IdProveedor",
                table: "Productos",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_IdProveedor",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Productos_IdProveedor",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "IdProveedor",
                table: "Productos");
        }
    }
}
