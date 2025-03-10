using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertAcademy.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseAndReviewsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stud_Course",
                table: "Stud_Reviews");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Stud_Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stud_Reviews_CourseId",
                table: "Stud_Reviews",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stud_Reviews_Courses_CourseId",
                table: "Stud_Reviews",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stud_Reviews_Courses_CourseId",
                table: "Stud_Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Stud_Reviews_CourseId",
                table: "Stud_Reviews");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Stud_Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Stud_Course",
                table: "Stud_Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
