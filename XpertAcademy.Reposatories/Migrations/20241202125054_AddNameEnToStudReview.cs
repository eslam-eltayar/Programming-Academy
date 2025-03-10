using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertAcademy.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddNameEnToStudReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stud_Name",
                table: "Stud_Reviews",
                newName: "Stud_NameEN");

            migrationBuilder.AddColumn<string>(
                name: "Stud_NameAR",
                table: "Stud_Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stud_NameAR",
                table: "Stud_Reviews");

            migrationBuilder.RenameColumn(
                name: "Stud_NameEN",
                table: "Stud_Reviews",
                newName: "Stud_Name");
        }
    }
}
