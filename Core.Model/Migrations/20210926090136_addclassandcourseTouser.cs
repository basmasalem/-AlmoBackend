using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Model.Migrations
{
    public partial class addclassandcourseTouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Users");
        }
    }
}
