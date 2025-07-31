using Compliance.Application.UseCases.OrganisationScreening;

namespace Compliance.Infrastructure.Activities
{
    public class OrganisationScreeningActivity : IOrganisationScreeningActivity
    {
        public Task<OrganisationScreeningResult> OrganisationProcessScreening(OrganisationScreeningCommand request)
        {
            throw new System.NotImplementedException();
        }
    }
}
