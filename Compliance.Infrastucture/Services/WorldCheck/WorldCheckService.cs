using Omni;
using Omni.Extensions;
using Compliance.Infrastructure.Proxy;
using Compliance.Application.Settings;
using Newtonsoft.Json;

namespace Compliance.Infrastructure.Services.WorldCheck;

public partial class WorldCheckService1(IWorldCheckApi worldCheckApi, ResolutionMessageSettings resolutionMessageSettings, ApiHeaders apiHeaders) : IWorldCheckService
{
    private readonly IWorldCheckApi _worldCheckApi = worldCheckApi;
    private readonly ResolutionMessageSettings _resolutionMessageSettings = resolutionMessageSettings;
    private readonly ApiHeaders _apiHeaders = apiHeaders;

    public async Task<Result<bool>> ValidateCustomerFraudStatusAsync(WorldCheckRequests request, CancellationToken cancellationToken)
    {
        var url = "/v2/cases/screeningRequest";
        var method = "POST";

        var date = DateTime.UtcNow.ToString("r");

        var payload = ApiHeaders.SerializeToExactJson(request.GroupId,
            request.EntityType,
            request.CaseScreeningState,
            request.ProviderTypes,
            request.Name,
            request.NameTransposition);

        string signature = _apiHeaders.GenerateSignature(method, url, date, payload);

        var response = await _worldCheckApi.ValidateCustomerFraudStatusAsync(
            date,
            _apiHeaders.ContentType,
            $"Signature keyId=\"{_apiHeaders.ApiKey}\",algorithm=\"hmac-sha256\",headers=\"{_apiHeaders.HeaderToSign}\",signature=\"{signature}\"",
            ApiHeaders.GetContentLength(payload),
            payload,
            cancellationToken);

        if (response == null)
            return new Error("Service unavailable", "500", false);

        if (response.Results == null || response.Results.Count == 0)
            return true;

        await Task.Delay(TimeSpan.FromSeconds(60), cancellationToken);

        var caseSystemId = response.CaseSystemId;

        var resultUrl = $"/v2/cases/{caseSystemId}/results";
        var resultMethod = "GET";
        date = DateTime.UtcNow.ToString("r");

        string resultSignature = _apiHeaders.GenerateSignature(resultMethod, resultUrl, date);

        var nameResultResponses = await _worldCheckApi.ValidateResultAsync(
        date,
        $"Signature keyId=\"{_apiHeaders.ApiKey}\",algorithm=\"hmac-sha256\",headers=\"{_apiHeaders.HeaderToSign}\",signature=\"{resultSignature}\"",
        caseSystemId,
        cancellationToken);

        if (nameResultResponses == null || nameResultResponses.Count == 0)
            return new Error("No matches found", "404", false);

        var nameResultResponse = nameResultResponses.FirstOrDefault();

        if (nameResultResponse?.Resolution == null)
            return new Error("Incomplete resolution match data", "422", false);

        var statusId = nameResultResponse.Resolution.StatusId;
        var reasonId = nameResultResponse.Resolution.ReasonId;

        var statusMessage = _resolutionMessageSettings.StatusMessages
            .FirstOrDefault(x => x.Value == statusId).Key;
        var reason = _resolutionMessageSettings.ReasonMessages
            .FirstOrDefault(x => x.Value == reasonId).Key;

        if (string.IsNullOrEmpty(statusMessage) || string.IsNullOrEmpty(reason))
            return new Error("Resolution details not configured", "500", false);

        return IsCompliant(statusMessage, reason, nameResultResponse.Categories);
    }

    private static bool IsCompliant(string statusMessage, string reason, List<string> categories)
    {
        bool isComplaint = false;
        switch (statusMessage)
        {
            case "FALSE":
                isComplaint = true;
                break;
            case "POSITIVE":
                if (categories != null)
                {
                    if (categories.Contains("sanctions") ||
                        categories.Contains("blacklist") ||
                        categories.Contains("prohibited"))
                    {
                        isComplaint = false;
                    }
                    if (categories.Contains("special interest categories") ||
                        categories.Contains("pep"))
                    {
                        isComplaint = true;
                    }
                }

                if (!string.IsNullOrEmpty(reason) && reason.StartsWith("prohibited", StringComparison.OrdinalIgnoreCase))
                    isComplaint = false;
                break;
            case "POSSIBLE":
                break;
            case "UNSPECIFIED":
                break;
            default:
                break;
        }

        return isComplaint;
    }
}
