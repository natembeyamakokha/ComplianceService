using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Domain.Form.Compliance
{
    public class IndividualScreeningRequestDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(
            @"^[a-zA-Z\s\-'`\u00B4]+$",
            ErrorMessage = "Name can only contain letters, spaces, hyphens (-), apostrophes ('), backticks (`), and acute accents (´). No numbers or special characters allowed."
        )]
        public string Name { get; set; }

        [Required(ErrorMessage = "BankId is required.")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "BankId must be a 1 to 3-digit number (e.g., 54 for Kenya).")]
        public string BankId { get; set; }

        [Required(ErrorMessage = "CaseId is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "CaseId must be between 3 and 100 characters.")]
        public string CaseId { get; set; }

        // Optional Secondary Fields (from watchlist/clientWatchlist config)
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string CountryLocation { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nationality { get; set; }
        public string DocumentId { get; set; }
        public string DocumentIdCountry { get; set; }
        public string DocumentIdType { get; set; }
    }
}
