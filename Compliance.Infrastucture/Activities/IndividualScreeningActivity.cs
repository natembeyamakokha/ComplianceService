using Compliance.Domain.Entity;
using Compliance.Application.UseCases.Compliance.IndividualScreening;
using Compliance.Infrastructure.Services.WorldCheck.IndividualScreeningRequestAndResponse;
using Compliance.Infrastructure.Services.WorldCheck;

namespace Compliance.Infrastructure.Activities
{
    public class IndividualScreeningActivity(IIndividualWorldCheckScreeningService individualWorldCheckScreeningService) : IIndividualScreeningActivity
    {
        private readonly IIndividualWorldCheckScreeningService _IIndividualWorldCheckScreeningService = individualWorldCheckScreeningService;


        public async Task<IndividualScreeningResult> IndividualProcessScreeningAsync(IndividualScreeningCommand request)
        {

            var nationalityIso3 = string.IsNullOrEmpty(request.Nationality)
                    ? "UNKNOWN"
                    : CountryKey.GetCountryKey(request.Nationality) ?? "UNKNOWN";

            var secondaryFields = new List<SecondaryField>();

            if (!string.IsNullOrEmpty(request.Gender))
                secondaryFields.Add(SecondaryField.CreateValueField("SFCT_1", request.Gender.ToUpper()));

            if (!string.IsNullOrEmpty(request.DateOfBirth))
                secondaryFields.Add(SecondaryField.CreateDateField("SFCT_2", request.DateOfBirth));

            if (!string.IsNullOrEmpty(request.CountryLocation))
                secondaryFields.Add(SecondaryField.CreateValueField("SFCT_3", request.CountryLocation.ToUpper()));

            if (!string.IsNullOrEmpty(request.PlaceOfBirth))
                secondaryFields.Add(SecondaryField.CreateValueField("SFCT_4", request.PlaceOfBirth.ToUpper()));

            secondaryFields.Add(SecondaryField.CreateValueField("SFCT_5", nationalityIso3.ToUpper()));

            if (!string.IsNullOrEmpty(request.DocumentId))
                secondaryFields.Add(SecondaryField.CreateValueField("SFCT_191", request.DocumentId));

            if (!string.IsNullOrEmpty(request.DocumentIdCountry))
                secondaryFields.Add(SecondaryField.CreateValueField("SFCT_192", request.DocumentIdCountry));

            if (!string.IsNullOrEmpty(request.DocumentIdType))
                secondaryFields.Add(SecondaryField.CreateValueField("SFCT_193", request.DocumentIdType));

            var screeningRequest = IndividualWorldCheckScreeningRequest.Create(
                name: request.CustomerName,
                entityType: "INDIVIDUAL",
                caseId: request.CaseId,
                caseScreeningState: new Dictionary<string, string> { { "WATCHLIST", "INITIAL" } },
                providerTypes: ["WATCHLIST", "CLIENT_WATCHLIST"],
                nameTransposition: false,
                secondaryFields: [.. secondaryFields.Cast<object>()],
                customFields: []
            );

            var response = await _IIndividualWorldCheckScreeningService.ScreenIndividualAsync(screeningRequest);

            if (response == null || response?.Error != null)
            {
                return new IndividualScreeningResult
                {
                    Code = -1,
                    CaseId = request.CaseId,
                    CaseSystemId = "0",
                    Result = "No response from WorldCheck service",
                    Successful = false,
                    StatusMessage = "Error",
                    StatusCode = "99"
                };
            }

            if (response?.Results == null || !response.Results.Any())
            {
                return HandleNoMatchResponse(request, response);
            }

            return HandleMatchFoundResponse(request, response);
        }

        private static IndividualScreeningResult HandleMatchFoundResponse(IndividualScreeningCommand request, WorldCheckResponse response)
        {
            return new IndividualScreeningResult
            {
                Code = 1,
                CaseId = response.CaseId ?? request.CaseId,
                CaseSystemId = response.CaseSystemId,
                Result = "Possible Matches found, undergoing Review by Compliance",
                StatusMessage = "Success",
                StatusCode = "00"
            };
        }

        private static IndividualScreeningResult HandleNoMatchResponse(IndividualScreeningCommand request, WorldCheckResponse response)
        {
            return new IndividualScreeningResult
            {
                Code = 0,
                CaseId = request.CaseId,
                CaseSystemId = response.CaseSystemId,
                Result = "No Matches Found",
                Successful = true,
                StatusMessage = "Success",
                StatusCode = "00"
            };
        }
    }
}
