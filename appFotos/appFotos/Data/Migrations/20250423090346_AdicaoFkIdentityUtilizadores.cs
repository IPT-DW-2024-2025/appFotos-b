using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appFotos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoFkIdentityUtilizadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserName",
                table: "Utilizadores",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ficheiro",
                table: "Fotografias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityUserName",
                table: "Utilizadores");

            migrationBuilder.AlterColumn<string>(
                name: "Ficheiro",
                table: "Fotografias",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
