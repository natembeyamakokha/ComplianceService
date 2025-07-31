using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Domain.Form.Compliance
{
    public class VesselScreeningRequestDto
    {
        public string Name { get; set; }
        public string BankId { get; set; }
        public string CaseId { get; set; }

        // Secondary fields from config (IMO_NUMBER with regex [0-9]{7})
        [RegularExpression(@"^[0-9]{7}$", ErrorMessage = "IMO Number must be exactly 7 digits.")]
        public string IMONumber { get; set; }// Must be exactly 7 digits
    }
}
