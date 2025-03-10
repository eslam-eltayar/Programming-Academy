using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertAcademy.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class EditInstractoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Instractors",
                newName: "NameEN");

            migrationBuilder.RenameColumn(
                name: "About",
                table: "Instractors",
                newName: "NameAR");

            migrationBuilder.AddColumn<string>(
                name: "AboutAR",
                table: "Instractors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AboutEN",
                table: "Instractors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutAR",
                table: "Instractors");

            migrationBuilder.DropColumn(
                name: "AboutEN",
                table: "Instractors");

            migrationBuilder.RenameColumn(
                name: "NameEN",
                table: "Instractors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameAR",
                table: "Instractors",
                newName: "About");
        }
    }
}
