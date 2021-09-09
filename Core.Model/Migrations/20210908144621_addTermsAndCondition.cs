using Microsoft.EntityFrameworkCore.Migrations;



namespace Core.Model.Migrations
{
    public partial class addTermsAndCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TermsAndConditions",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TermsAndConditions",
                table: "Settings");
        }
    }
}
