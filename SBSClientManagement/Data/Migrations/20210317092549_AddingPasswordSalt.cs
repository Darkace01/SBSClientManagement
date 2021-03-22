using Microsoft.EntityFrameworkCore.Migrations;

namespace SBSClientManagement.Data.Migrations
{
    public partial class AddingPasswordSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Vpns",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Servers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Vpns");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Servers");
        }
    }
}
