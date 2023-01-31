using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProspercityApiUsuarios.Migrations
{
    /// <inheritdoc />
    public partial class addUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Usuario");
        }
    }
}
