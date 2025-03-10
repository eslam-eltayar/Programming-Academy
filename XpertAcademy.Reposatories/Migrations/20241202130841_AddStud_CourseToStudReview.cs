using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertAcademy.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddStud_CourseToStudReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stud_Course",
                table: "Stud_Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stud_Course",
                table: "Stud_Reviews");
        }
    }
}
