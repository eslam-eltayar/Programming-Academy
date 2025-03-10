using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertAcademy.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class EditCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "TitleEN");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "TitleAR");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "CourseContents",
                newName: "TitleEN");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CourseContents",
                newName: "TitleAR");

            migrationBuilder.AddColumn<string>(
                name: "CourseStatus",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExternalDescriptionAR",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExternalDescriptionEN",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternalDescriptionAR",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternalDescriptionEN",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "CourseContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "CourseContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseStatus",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ExternalDescriptionAR",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ExternalDescriptionEN",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InternalDescriptionAR",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InternalDescriptionEN",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "CourseContents");

            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "CourseContents");

            migrationBuilder.RenameColumn(
                name: "TitleEN",
                table: "Courses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleAR",
                table: "Courses",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "TitleEN",
                table: "CourseContents",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleAR",
                table: "CourseContents",
                newName: "Description");
        }
    }
}
