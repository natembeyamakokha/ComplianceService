using Compliance.Shared;
using Compliance.Application.Commands;
using MediatR;

namespace Compliance.Application.UseCases.IndividualScreening
{
    public class IndividualScreeningCommand : IRequest<Result<IndividualScreeningResult>>
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
    }
}
