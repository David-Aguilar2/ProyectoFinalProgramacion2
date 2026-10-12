using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasOnline.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionEnIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Pedidos_PedidoId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_CategoriaId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_PedidoId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Pagos");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdCategoria",
                table: "Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdPedido",
                table: "Pagos",
                column: "IdPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Pedidos_IdPedido",
                table: "Pagos",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_IdCategoria",
                table: "Productos",
                column: "IdCategoria",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Pedidos_IdPedido",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_IdCategoria",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_IdCategoria",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_IdPedido",
                table: "Pagos");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_PedidoId",
                table: "Pagos",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Pedidos_PedidoId",
                table: "Pagos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_CategoriaId",
                table: "Productos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
