using Microsoft.EntityFrameworkCore.Migrations;

namespace SBSClientManagement.Data.Migrations
{
    public partial class UpdatingModelRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Clients_ClientId",
                table: "Servers");

            migrationBuilder.DropForeignKey(
                name: "FK_SQLServers_Clients_ClientId",
                table: "SQLServers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vpns_Clients_ClientId",
                table: "Vpns");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Vpns",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "SQLServers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Servers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Clients_ClientId",
                table: "Servers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SQLServers_Clients_ClientId",
                table: "SQLServers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vpns_Clients_ClientId",
                table: "Vpns",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Clients_ClientId",
                table: "Servers");

            migrationBuilder.DropForeignKey(
                name: "FK_SQLServers_Clients_ClientId",
                table: "SQLServers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vpns_Clients_ClientId",
                table: "Vpns");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Vpns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "SQLServers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Servers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Clients_ClientId",
                table: "Servers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SQLServers_Clients_ClientId",
                table: "SQLServers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vpns_Clients_ClientId",
                table: "Vpns",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
