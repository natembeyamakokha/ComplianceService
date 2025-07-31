using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compliance.Shared;
using MediatR;

namespace Compliance.Application.UseCases.VesselScreening
{
    public class VesselScreeningCommand : IRequest<Result<VesselScreeningResult>>
    {
        public string BankId { get; set; }
        public string CaseId { get; set; }
        public string VesselName { get; set; }
        public string IMONumber { get; set; } // Must be 7-digit number
    }
}
