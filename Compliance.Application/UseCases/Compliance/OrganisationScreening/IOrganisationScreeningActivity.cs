using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Application.UseCases.Compliance.OrganisationScreening
{
    public interface IOrganisationScreeningActivity
    {
        Task<OrganisationScreeningResult> OrganisationProcessScreeningAsync(OrganisationScreeningCommand command);
    }
}
