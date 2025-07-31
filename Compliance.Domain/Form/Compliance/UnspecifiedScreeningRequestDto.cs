using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Domain.Form.Compliance
{
    public class UnspecifiedScreeningRequestDto
    {
        public string Name { get; set; }
        public string BankId { get; set; }
        public string CaseId { get; set; }

        // Secondary fields from config
        public string DocumentId { get; set; }
        public string DocumentIdCountry { get; set; }
        public string DocumentIdType { get; set; }
    }
}
