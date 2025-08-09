using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionIdToCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carritos_AspNetUsers_UsuarioId",
                table: "Carritos");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Carritos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Carritos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carritos_AspNetUsers_UsuarioId",
                table: "Carritos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carritos_AspNetUsers_UsuarioId",
                table: "Carritos");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Carritos");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Carritos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carritos_AspNetUsers_UsuarioId",
                table: "Carritos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
