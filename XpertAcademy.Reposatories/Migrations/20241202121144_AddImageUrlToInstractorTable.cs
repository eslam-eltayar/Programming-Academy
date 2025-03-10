using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertAcademy.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToInstractorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Instractors",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Instractors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Instractors");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Instractors",
                newName: "Description");
        }
    }
}
