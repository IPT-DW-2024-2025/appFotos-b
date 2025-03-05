using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appFotos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoFks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Gostos",
                table: "Gostos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Gostos",
                newName: "FotografiaFk");

            migrationBuilder.AlterColumn<int>(
                name: "FotografiaFk",
                table: "Gostos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "UtilizadorFk",
                table: "Gostos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaFk",
                table: "Fotografias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DonoFk",
                table: "Fotografias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gostos",
                table: "Gostos",
                columns: new[] { "UtilizadorFk", "FotografiaFk" });

            migrationBuilder.CreateTable(
                name: "ComprasFotografias",
                columns: table => new
                {
                    ListaComprasId = table.Column<int>(type: "INTEGER", nullable: false),
                    ListaFotografiasId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprasFotografias", x => new { x.ListaComprasId, x.ListaFotografiasId });
                    table.ForeignKey(
                        name: "FK_ComprasFotografias_Compras_ListaComprasId",
                        column: x => x.ListaComprasId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComprasFotografias_Fotografias_ListaFotografiasId",
                        column: x => x.ListaFotografiasId,
                        principalTable: "Fotografias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gostos_FotografiaFk",
                table: "Gostos",
                column: "FotografiaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografias_CategoriaFk",
                table: "Fotografias",
                column: "CategoriaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografias_DonoFk",
                table: "Fotografias",
                column: "DonoFk");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasFotografias_ListaFotografiasId",
                table: "ComprasFotografias",
                column: "ListaFotografiasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotografias_Categorias_CategoriaFk",
                table: "Fotografias",
                column: "CategoriaFk",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fotografias_Utilizadores_DonoFk",
                table: "Fotografias",
                column: "DonoFk",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gostos_Fotografias_FotografiaFk",
                table: "Gostos",
                column: "FotografiaFk",
                principalTable: "Fotografias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gostos_Utilizadores_UtilizadorFk",
                table: "Gostos",
                column: "UtilizadorFk",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotografias_Categorias_CategoriaFk",
                table: "Fotografias");

            migrationBuilder.DropForeignKey(
                name: "FK_Fotografias_Utilizadores_DonoFk",
                table: "Fotografias");

            migrationBuilder.DropForeignKey(
                name: "FK_Gostos_Fotografias_FotografiaFk",
                table: "Gostos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gostos_Utilizadores_UtilizadorFk",
                table: "Gostos");

            migrationBuilder.DropTable(
                name: "ComprasFotografias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gostos",
                table: "Gostos");

            migrationBuilder.DropIndex(
                name: "IX_Gostos_FotografiaFk",
                table: "Gostos");

            migrationBuilder.DropIndex(
                name: "IX_Fotografias_CategoriaFk",
                table: "Fotografias");

            migrationBuilder.DropIndex(
                name: "IX_Fotografias_DonoFk",
                table: "Fotografias");

            migrationBuilder.DropColumn(
                name: "UtilizadorFk",
                table: "Gostos");

            migrationBuilder.DropColumn(
                name: "CategoriaFk",
                table: "Fotografias");

            migrationBuilder.DropColumn(
                name: "DonoFk",
                table: "Fotografias");

            migrationBuilder.RenameColumn(
                name: "FotografiaFk",
                table: "Gostos",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Gostos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gostos",
                table: "Gostos",
                column: "Id");
        }
    }
}
