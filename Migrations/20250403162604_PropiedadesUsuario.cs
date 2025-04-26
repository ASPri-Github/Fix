using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApisConvenciones9.Migrations
{
    /// <inheritdoc />
    public partial class PropiedadesUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Hoteles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteles_UsuarioId",
                table: "Hoteles",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hoteles_AspNetUsers_UsuarioId",
                table: "Hoteles",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hoteles_AspNetUsers_UsuarioId",
                table: "Hoteles");

            migrationBuilder.DropIndex(
                name: "IX_Hoteles_UsuarioId",
                table: "Hoteles");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Hoteles");

            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");
        }
    }
}
