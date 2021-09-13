using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Model.Migrations
{
    public partial class updateuserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCardCVV",
                table: "SubscribeRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCardNumber",
                table: "SubscribeRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCardCVV",
                table: "SubscribeRequests");

            migrationBuilder.DropColumn(
                name: "UserCardNumber",
                table: "SubscribeRequests");
        }
    }
}
