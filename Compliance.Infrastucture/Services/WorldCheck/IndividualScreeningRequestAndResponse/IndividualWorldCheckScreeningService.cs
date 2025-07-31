using Compliance.Application.Settings;
using Compliance.Infrastructure.Proxy;

namespace Compliance.Infrastructure.Services.WorldCheck.IndividualScreeningRequestAndResponse
{
    public class IndividualWorldCheckScreeningService(
        IWorldCheckApi individualWorldCheckApi,
        ApiHeaders apiHeaders,
        CaseScreeningSettings caseScreeningSettings) : IIndividualWorldCheckScreeningService
    {
        private readonly IWorldCheckApi _individualWorldCheckApi = individualWorldCheckApi;
        private readonly ApiHeaders _apiHeaders = apiHeaders;
        private readonly CaseScreeningSettings _caseScreeningSettings = caseScreeningSettings;

        public async Task<WorldCheckResponse> ScreenIndividualAsync(IndividualWorldCheckScreeningRequest request)
        {
            var url = "/v2/cases/screeningRequest";
            var method = "POST";

            var date = DateTime.UtcNow.ToString("r");

            var payload = ApiHeaders.SerializeToExactJson(_caseScreeningSettings.GroupId,
                request.EntityType,
                request.CaseScreeningState,
                request.ProviderTypes,
                request.Name,
                request.NameTransposition);

            string signature = _apiHeaders.GenerateSignature(method, url, date, payload);

            var response = await _individualWorldCheckApi.ValidateCustomerFraudStatusAsync(
                date,
                _apiHeaders.ContentType,
                $"Signature keyId=\"{_apiHeaders.ApiKey}\",algorithm=\"hmac-sha256\",headers=\"{_apiHeaders.HeaderToSign}\",signature=\"{signature}\"",
                ApiHeaders.GetContentLength(payload),
                payload,
                CancellationToken.None);

            return response;
        }
    }
}
