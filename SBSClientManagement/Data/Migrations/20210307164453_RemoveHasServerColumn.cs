using Microsoft.EntityFrameworkCore.Migrations;

namespace SBSClientManagement.Data.Migrations
{
    public partial class RemoveHasServerColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasServer",
                table: "SQLServers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasServer",
                table: "SQLServers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
