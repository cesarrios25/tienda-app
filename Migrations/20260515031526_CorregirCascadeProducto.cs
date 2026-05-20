using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaApp.Migrations
{
    /// <inheritdoc />
    public partial class CorregirCascadeProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagenProducto_Productos_IdProducto",
                table: "ImagenProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Productos_IdProducto",
                table: "Stocks");

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Productos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "DescuentoPrecio",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagenProducto_Productos_IdProducto",
                table: "ImagenProducto",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Productos_IdProducto",
                table: "Stocks",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagenProducto_Productos_IdProducto",
                table: "ImagenProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Productos_IdProducto",
                table: "Stocks");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Marca",
                keyValue: null,
                column: "Marca",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Productos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "DescuentoPrecio",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagenProducto_Productos_IdProducto",
                table: "ImagenProducto",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Productos_IdProducto",
                table: "Stocks",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
