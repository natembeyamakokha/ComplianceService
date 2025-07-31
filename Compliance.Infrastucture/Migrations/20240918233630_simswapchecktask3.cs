using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class simswapchecktask3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimSwapCheckTask",
                schema: "UTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskStatusId = table.Column<int>(type: "int", nullable: false),
                    TaskStatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRunAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextRunAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    AllowedSwapDaysCount = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallbackUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimSwapCheckTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimSwapCheckPhoneNumber",
                schema: "UTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Successful = table.Column<bool>(type: "bit", nullable: false),
                    IsSwapped = table.Column<bool>(type: "bit", nullable: false),
                    LastSwappedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "SimSwapResultHistory",
                schema: "UTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimSwapResultHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimSwapResultHistory_SimSwapCheckTask_TaskId",
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

            migrationBuilder.CreateIndex(
                name: "IX_SimSwapResultHistory_TaskId",
                schema: "UTS",
                table: "SimSwapResultHistory",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimSwapCheckPhoneNumber",
                schema: "UTS");

            migrationBuilder.DropTable(
                name: "SimSwapResultHistory",
                schema: "UTS");

            migrationBuilder.DropTable(
                name: "SimSwapCheckTask",
                schema: "UTS");
        }
    }
}
