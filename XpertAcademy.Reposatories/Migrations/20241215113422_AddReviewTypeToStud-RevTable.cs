using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertAcademy.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewTypeToStudRevTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stud_Reviews_Courses_CourseId",
                table: "Stud_Reviews");

            migrationBuilder.RenameColumn(
                name: "Review",
                table: "Stud_Reviews",
                newName: "ReviewType");

            migrationBuilder.AlterColumn<string>(
                name: "Stud_SM_Link",
                table: "Stud_Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Stud_Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ReviewAR",
                table: "Stud_Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReviewEN",
                table: "Stud_Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Stud_Reviews_Courses_CourseId",
                table: "Stud_Reviews",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stud_Reviews_Courses_CourseId",
                table: "Stud_Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewAR",
                table: "Stud_Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewEN",
                table: "Stud_Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewType",
                table: "Stud_Reviews",
                newName: "Review");

            migrationBuilder.AlterColumn<string>(
                name: "Stud_SM_Link",
                table: "Stud_Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Stud_Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stud_Reviews_Courses_CourseId",
                table: "Stud_Reviews",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
