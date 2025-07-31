using Compliance.Domain.Enum;
using Compliance.Domain.Mappers;

namespace Compliance.Domain.Entity.Interfaces
{
    public interface ISubsidiaryRulesRepository
    {
        Task<List<SubsidiaryRulesModel>> GetSubsidiaryRulesAsync(string journeyName, CountryCode countryCode);
    }
}