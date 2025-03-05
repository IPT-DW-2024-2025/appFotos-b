using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appFotos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCompras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompradorId",
                table: "Compras",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_CompradorId",
                table: "Compras",
                column: "CompradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Utilizadores_CompradorId",
                table: "Compras",
                column: "CompradorId",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Utilizadores_CompradorId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_CompradorId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "CompradorId",
                table: "Compras");
        }
    }
}
