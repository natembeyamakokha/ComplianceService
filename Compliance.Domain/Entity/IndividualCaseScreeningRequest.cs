using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Domain.Entity
{
    public class IndividualCaseScreeningRequest
    {
        public string Gender { get; set; }
        public string BankId { get; set; }
        public string CaseId { get; set; }
        public string CustomerName { get; set; }
        public string Nationality { get; set; }
        public string DocumentId { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string CountryLocation { get; set; }
        public string DocumentIdType { get; set; }
        public string DocumentIdCountry { get; set; }
        public string EntityType { get; set; } = "INDIVIDUAL";// put as constant value other than string ~~~
        public string ResolutionStatus { get; set; }
    }
}
