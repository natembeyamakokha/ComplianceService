using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compliance.Domain.Enum;

namespace Compliance.Domain.Entity
{

    public class CaseScreeningRequest
    {
        public string BankId { get; set; }
        public string CaseId { get; set; }
        public string Name { get; set; }
        public CaseScreeningState CaseScreeningState { get; set; }
        public ScreeningEntityType EntityType { get; set; }

        // Individual
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PlaceOfBirth { get; set; }
        public string CountryLocation { get; set; }

        // Organisation
        public string RegisteredCountry { get; set; }

        // Vessel
        public string IMONumber { get; set; }

        // Document (shared)
        public string DocumentId { get; set; }
        public string DocumentIdType { get; set; }
        public string DocumentIdCountry { get; set; }

        // Custom Fields (if needed)
        public Dictionary<string, string> CustomFields { get; set; } = new();
    }

    public class CaseScreeningState
    {
        public string Watchlist { get; set; }
    }

}