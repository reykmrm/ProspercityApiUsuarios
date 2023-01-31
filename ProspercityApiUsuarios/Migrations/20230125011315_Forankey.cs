using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProspercityApiUsuarios.Migrations
{
    /// <inheritdoc />
    public partial class Forankey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_TipoUsuario_IdTipoUsuarioId",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "IdTipoUsuarioId",
                table: "Usuario",
                newName: "IdTipoUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_IdTipoUsuarioId",
                table: "Usuario",
                newName: "IX_Usuario_IdTipoUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_TipoUsuario_IdTipoUsuario",
                table: "Usuario",
                column: "IdTipoUsuario",
                principalTable: "TipoUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_TipoUsuario_IdTipoUsuario",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "IdTipoUsuario",
                table: "Usuario",
                newName: "IdTipoUsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_IdTipoUsuario",
                table: "Usuario",
                newName: "IX_Usuario_IdTipoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_TipoUsuario_IdTipoUsuarioId",
                table: "Usuario",
                column: "IdTipoUsuarioId",
                principalTable: "TipoUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
