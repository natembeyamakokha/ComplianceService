using Microsoft.EntityFrameworkCore.Migrations;

namespace Compliance.Infrastructure.Migrations
{
    public partial class added_column_terminate_on_failure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TerminateOnFailure",
                schema: "UTS",
                table: "SubsidiaryRules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TerminateOnFailure",
                schema: "UTS",
                table: "SubsidiaryRules");
        }
    }
}
