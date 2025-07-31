using System.Text.Json.Serialization;

namespace Compliance.Infrastructure.Services.WorldCheck.IndividualScreeningRequestAndResponse
{
    public class IndividualWorldCheckScreeningRequest
    {
        [JsonPropertyName("groupId")]
        public string GroupId { get; }

        [JsonPropertyName("entityType")]
        public string EntityType { get; }

        [JsonPropertyName("caseId")]
        public string CaseId { get; }

        [JsonPropertyName("caseScreeningState")]
        public Dictionary<string, string> CaseScreeningState { get; }

        [JsonPropertyName("providerTypes")]
        public List<string> ProviderTypes { get; }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("nameTransposition")]
        public bool NameTransposition { get; } = false;

        [JsonPropertyName("secondaryFields")]
        public List<object> SecondaryFields { get; }

        [JsonPropertyName("customFields")]
        public List<object> CustomFields { get; }

        public IndividualWorldCheckScreeningRequest(
            string name,
            string entityType,
            string caseId,
            Dictionary<string, string> caseScreeningState,
            List<string> providerTypes,
            bool nameTransposition,
            List<object> secondaryFields,
            List<object> customFields)
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
            CaseId = caseId ?? string.Empty;
            CaseScreeningState = caseScreeningState ?? new Dictionary<string, string>
        {
            {"WATCHLIST", "INITIAL"}
        };
            ProviderTypes = providerTypes ?? new List<string> { "CLIENT_WATCHLIST", "WATCHLIST" };
            Name = name ?? throw new ArgumentNullException(nameof(name));
            NameTransposition = nameTransposition;
            SecondaryFields = secondaryFields ?? new List<object>();
            CustomFields = customFields ?? new List<object>();
        }

        // Optional: Factory method for convenience
        public static IndividualWorldCheckScreeningRequest Create(
            string name,
            string entityType,
            string caseId = null,
            Dictionary<string, string> caseScreeningState = null,
            List<string> providerTypes = null,
            bool nameTransposition = false,
            List<object> secondaryFields = null,
            List<object> customFields = null)
        {
            return new IndividualWorldCheckScreeningRequest(
                name,
                entityType,
                caseId,
                caseScreeningState,
                providerTypes,
                nameTransposition,
                secondaryFields,
                customFields);
        }
    }

}