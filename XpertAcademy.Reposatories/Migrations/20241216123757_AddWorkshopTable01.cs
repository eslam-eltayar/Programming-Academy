using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertAcademy.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkshopTable01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Workshop",
                table: "Workshop");

            migrationBuilder.RenameTable(
                name: "Workshop",
                newName: "Workshops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workshops",
                table: "Workshops",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Workshops",
                table: "Workshops");

            migrationBuilder.RenameTable(
                name: "Workshops",
                newName: "Workshop");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workshop",
                table: "Workshop",
                column: "Id");
        }
    }
}
