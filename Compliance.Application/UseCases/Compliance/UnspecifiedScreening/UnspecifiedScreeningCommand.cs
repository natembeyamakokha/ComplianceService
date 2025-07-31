using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compliance.Shared;
using MediatR;

namespace Compliance.Application.UseCases.Compliance.UnspecifiedScreening
{
    public class UnspecifiedScreeningCommand : IRequest<Result<UnspecifiedScreeningResult>>
    {
        public string BankId { get; set; }
        public string CaseId { get; set; }
        public string EntityName { get; set; }
        public string DocumentId { get; set; }
        public string DocumentIdType { get; set; }
        public string DocumentIdCountry { get; set; }
    }
}
