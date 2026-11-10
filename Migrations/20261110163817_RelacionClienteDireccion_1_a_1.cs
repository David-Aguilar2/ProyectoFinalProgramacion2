using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasOnline.Migrations
{
    /// <inheritdoc />
    public partial class RelacionClienteDireccion_1_a_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Direcciones_IdCliente",
                table: "Direcciones");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_IdCliente",
                table: "Direcciones",
                column: "IdCliente",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Direcciones_IdCliente",
                table: "Direcciones");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Correo", "FechaRegistro", "Nombre", "Password", "Rol", "Telefono" },
                values: new object[] { 1, "admin@tiendaOnline.sv", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrador Principal", "Admin123!", "Administrador", "70000000" });

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_IdCliente",
                table: "Direcciones",
                column: "IdCliente");
        }
    }
}
