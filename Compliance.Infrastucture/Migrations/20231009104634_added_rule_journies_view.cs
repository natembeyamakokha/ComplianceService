using Microsoft.EntityFrameworkCore.Migrations;

namespace Compliance.Infrastructure.Migrations
{
    public partial class added_rule_journies_view : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR ALTER VIEW [UTS].[OnboardingJourneyRules]
                            AS
                            SELECT
                            X.Id,
                            X.RuleId,
                            X.IsApplicable,
                            X.ChannelId,
                            X.JourneyId,
                            X.SubsidiaryId,
                            X.ConfigValue,
                            Y.Name AS ChannelName,
                            Z.Name AS RuleName,
                            A.Name AS JourneyName,
                            B.Name AS SubsidiaryName,
                            B.Code AS SubsidiaryCode
                            FROM [UTS].[SubsidiaryRules] AS X,
                            [UTS].[Channels] Y,
                            [UTS].[Rules] Z, 
                            [UTS].[Journies] A,
                            [UTS].[Subsidiaries] B
                            WHERE X.RuleId = Z.Id
                            AND X.ChannelId = Y.Id
                            AND X.JourneyId = A.Id
                            AND X.SubsidiaryId = B.Id
                            AND X.IsDeleted = 0";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
