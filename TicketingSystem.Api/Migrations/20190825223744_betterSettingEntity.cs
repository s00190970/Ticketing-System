using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketingSystem.Api.Migrations
{
    public partial class betterSettingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanModifyCustomerName",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CanModifyServiceType",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CanModifyTicketType",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Settings");

            migrationBuilder.AddColumn<bool>(
                name: "CanModifyCustomerName",
                table: "Settings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanModifyServiceType",
                table: "Settings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanModifyTicketType",
                table: "Settings",
                nullable: false,
                defaultValue: false);
        }
    }
}
