using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Application.UseCases.Compliance.OrganisationScreening
{
    public class OrganisationScreeningResult
    {
        public int Code { get; set; }
        public string CaseId { get; set; }
        public string CaseSystemId { get; set; }
        public string Result { get; set; }
        public bool Successful { get; set; }
        public string StatusMessage { get; set; }
        public string StatusCode { get; set; }
    }
}
