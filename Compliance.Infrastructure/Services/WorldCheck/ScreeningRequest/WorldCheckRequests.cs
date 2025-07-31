using System.Text.Json.Serialization;

namespace Compliance.Infrastructure.Services.WorldCheck;

public class WorldCheckRequests
{
    [JsonPropertyName("groupId")]
    public string GroupId { get; set; }

    [JsonPropertyName("entityType")]
    public string EntityType { get; set; }

    [JsonPropertyName("caseId")]
    public string CaseId { get; set; }

    [JsonPropertyName("caseScreeningState")]
    public Dictionary<string, string> CaseScreeningState { get; set; }

    [JsonPropertyName("providerTypes")]
    public List<string> ProviderTypes { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("nameTransposition")]
    public bool NameTransposition { get; set; } = false;

    [JsonPropertyName("secondaryFields")]
    public List<object> SecondaryFields { get; set; }

    [JsonPropertyName("customFields")]
    public List<object> CustomFields { get; set; }

    private WorldCheckRequests(string name, string groupId, string entityType)
    {
        Name = name;
        ProviderTypes = new List<string> {
            "CLIENT_WATCHLIST", "WATCHLIST"
        };
        CaseScreeningState = new Dictionary<string, string>
        {
            {"WATCHLIST", "INITIAL"}
        };
        GroupId = groupId;
        EntityType = entityType;
    }

    public static WorldCheckRequests Create(string name, string groupId, string entityType)
        => new(name, groupId, entityType);
}