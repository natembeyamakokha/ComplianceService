using Compliance.Application.Settings;
using Compliance.Domain.Entity;
using Compliance.Infrastructure.Services.WorldCheck.IndividualScreeningRequestAndResponse;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Compliance.Infrastructure.Services.WorldCheck;

public class ApiHeaders(CaseScreeningSettings settings)
{
    private readonly string _apiSecretKey = settings.ApiSecret;
    private readonly string _apiKey = settings.ApiKey;
    private readonly string _apiUrl = settings.GroupId;

    public string ApiKey { get; set; } = settings.ApiKey;
    public string ContentType { get; set; } = "application/json";
    public string HeaderToSign { get; set; }

    public string GenerateSignature(string method, string url, string date, string body = null)
    {
        HeaderToSign = $"(request-target) host date";
        HeaderToSign = string.IsNullOrWhiteSpace(body) ? HeaderToSign : HeaderToSign + " content-type content-length";

        string stringToSign = $"(request-target): {method.ToLower()} {url}\n" +
                              $"host: api-worldcheck.refinitiv.com\n" +
                              $"date: {date}";

        stringToSign = string.IsNullOrWhiteSpace(body) ? stringToSign : stringToSign +
                              $"\ncontent-type: {ContentType}\n" +
                              $"content-length: {GetContentLength(body)}\n" +
                              $"{body}";

        return ComputeHMACSHA256Signature(stringToSign);
    }

    private string ComputeHMACSHA256Signature(string stringToSign)
    {
        using HMACSHA256 hmac = new(Encoding.ASCII.GetBytes(_apiSecretKey));
        byte[] signatureBytes = hmac.ComputeHash(Encoding.ASCII.GetBytes(stringToSign));
        return Convert.ToBase64String(signatureBytes);
    }

    public static int GetContentLength(string data)
    {
        if (data == null)
            return 0;

        return Encoding.UTF8.GetByteCount(data);
    }

    public static string SerializeToExactJson(string groupId, string entityType, Dictionary<string, string> caseScreeningState,
        List<string> providerTypes, string name, bool nameTransposition)
    {
        var options = new JsonWriterOptions
        {
            Indented = true
        };

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, options);

        writer.WriteStartObject();

        writer.WriteString("groupId", groupId);
        writer.WriteString("entityType", entityType);
        writer.WriteNull("caseId");

        writer.WritePropertyName("caseScreeningState");
        writer.WriteStartObject();
        foreach (var kvp in caseScreeningState)
        {
            writer.WriteString(kvp.Key, kvp.Value);
        }
        writer.WriteEndObject();

        writer.WritePropertyName("providerTypes");
        writer.WriteStartArray();
        foreach (var item in providerTypes)
        {
            writer.WriteStringValue(item);
        }
        writer.WriteEndArray();

        writer.WriteString("name", name);
        writer.WriteBoolean("nameTransposition", nameTransposition);
        writer.WriteNull("secondaryFields");
        writer.WriteNull("customFields");

        // End the JSON object
        writer.WriteEndObject();
        writer.Flush();

        // Convert the stream to a properly formatted string
        return Encoding.UTF8.GetString(stream.ToArray());
    }
}