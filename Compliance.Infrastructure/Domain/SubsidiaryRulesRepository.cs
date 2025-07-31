using Dapper;
using Compliance.Domain.Enum;
using Compliance.Domain.Mappers;
using Compliance.Shared.DataAccess;
using Compliance.Domain.Entity.Interfaces;

namespace Compliance.Infrastructure.Domains
{
    public class SubsidiaryRulesRepository : ISubsidiaryRulesRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public SubsidiaryRulesRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<SubsidiaryRulesModel>> GetSubsidiaryRulesAsync(string journeyName, CountryCode countryCode)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql = @"SELECT * FROM [UTS].[OnboardingJourneyRules]
                        WHERE JourneyName = @JourneyName AND SubsidiaryCode = @SubsidiaryCode";
            var result = await connection.QueryAsync<SubsidiaryRulesModel>(sql, new
            {
                JourneyName = journeyName,
                SubsidiaryCode = countryCode.ToString()
            });

            return result.AsList();
        }
    }
}