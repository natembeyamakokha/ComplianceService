using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CleanUpSimSwapTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimSwapCheckPhoneNumber",
                schema: "UTS");

            migrationBuilder.DropColumn(
                name: "AllowedSwapDaysCount",
                schema: "UTS",
                table: "SimSwapCheckTask");

            migrationBuilder.RenameColumn(
                name: "CallbackUrl",
                schema: "UTS",
                table: "SimSwapCheckTask",
                newName: "CommandPayload");

            migrationBuilder.AddColumn<string>(
                name: "CommandName",
                schema: "UTS",
                table: "SimSwapCheckTask",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommandName",
                schema: "UTS",
                table: "SimSwapCheckTask");

            migrationBuilder.RenameColumn(
                name: "CommandPayload",
                schema: "UTS",
                table: "SimSwapCheckTask",
                newName: "CallbackUrl");

            migrationBuilder.AddColumn<int>(
                name: "AllowedSwapDaysCount",
                schema: "UTS",
                table: "SimSwapCheckTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SimSwapCheckPhoneNumber",
                schema: "UTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSwapped = table.Column<bool>(type: "bit", nullable: false),
                    LastSwappedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Successful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimSwapCheckPhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimSwapCheckPhoneNumber_SimSwapCheckTask_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "UTS",
                        principalTable: "SimSwapCheckTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimSwapCheckPhoneNumber_TaskId",
                schema: "UTS",
                table: "SimSwapCheckPhoneNumber",
                column: "TaskId");
        }
    }
}
