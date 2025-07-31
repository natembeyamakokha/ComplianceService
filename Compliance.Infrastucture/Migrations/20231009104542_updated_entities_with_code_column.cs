using Microsoft.EntityFrameworkCore.Migrations;

namespace Compliance.Infrastructure.Migrations
{
    public partial class updated_entities_with_code_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "UTS",
                table: "Subsidiaries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "UTS",
                table: "Rules",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "UTS",
                table: "Journies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "UTS",
                table: "Channels",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "UTS",
                table: "Subsidiaries");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "UTS",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "UTS",
                table: "Journies");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "UTS",
                table: "Channels");
        }
    }
}
