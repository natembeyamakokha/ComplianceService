using System.Text.Json.Serialization;

namespace Compliance.Infrastructure.Services.WorldCheck;

public class CaseScreeningState
{
    [JsonPropertyName("WATCHLIST")]
    public string WATCHLIST { get; set; }
}

public class Creator
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}

public class LastScreenedDatesByProviderType
{
    [JsonPropertyName("WATCHLIST")]
    public DateTime WATCHLIST { get; set; }

    [JsonPropertyName("CLIENT_WATCHLIST")]
    public DateTime CLIENTWATCHLIST { get; set; }
}


public class Result
{
    [JsonPropertyName("resultId")]
    public string ResultId { get; set; }

    [JsonPropertyName("referenceId")]
    public string ReferenceId { get; set; }

    [JsonPropertyName("matchScore")]
    public double MatchScore { get; set; }

    [JsonPropertyName("matchStrength")]
    public string MatchStrength { get; set; }

    [JsonPropertyName("matchedTerm")]
    public string MatchedTerm { get; set; }

    [JsonPropertyName("matchedTerms")]
    public List<MatchedTerm> MatchedTerms { get; set; }

    [JsonPropertyName("submittedTerm")]
    public string SubmittedTerm { get; set; }

    [JsonPropertyName("matchedNameType")]
    public string MatchedNameType { get; set; }

    [JsonPropertyName("secondaryFieldResults")]
    public List<object> SecondaryFieldResults { get; set; }

    [JsonPropertyName("sources")]
    public List<string> Sources { get; set; }

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; }

    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }

    [JsonPropertyName("modificationDate")]
    public DateTime ModificationDate { get; set; }

    [JsonPropertyName("lastAlertDate")]
    public DateTime LastAlertDate { get; set; }

    [JsonPropertyName("resolution")]
    public object Resolution { get; set; }

    [JsonPropertyName("resultReview")]
    public ResultReview ResultReview { get; set; }

    [JsonPropertyName("primaryName")]
    public string PrimaryName { get; set; }

    [JsonPropertyName("events")]
    public List<Event> Events { get; set; }

    [JsonPropertyName("countryLinks")]
    public List<CountryLink> CountryLinks { get; set; }

    [JsonPropertyName("identityDocuments")]
    public List<object> IdentityDocuments { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("providerType")]
    public string ProviderType { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("entityCreationDate")]
    public DateTime EntityCreationDate { get; set; }

    [JsonPropertyName("entityModificationDate")]
    public DateTime EntityModificationDate { get; set; }

    [JsonPropertyName("pepStatus")]
    public string PepStatus { get; set; }

    [JsonPropertyName("actionTypes")]
    public List<object> ActionTypes { get; set; }
}


public class WorldCheckResponse
{
    [JsonPropertyName("caseId")]
    public string CaseId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("providerTypes")]
    public List<string> ProviderTypes { get; set; }

    [JsonPropertyName("customFields")]
    public List<object> CustomFields { get; set; }

    [JsonPropertyName("secondaryFields")]
    public List<object> SecondaryFields { get; set; }

    [JsonPropertyName("groupId")]
    public string GroupId { get; set; }

    [JsonPropertyName("entityType")]
    public string EntityType { get; set; }

    [JsonPropertyName("caseSystemId")]
    public string CaseSystemId { get; set; }

    [JsonPropertyName("caseScreeningState")]
    public CaseScreeningState CaseScreeningState { get; set; }

    [JsonPropertyName("lifecycleState")]
    public string LifecycleState { get; set; }

    [JsonPropertyName("creator")]
    public Creator Creator { get; set; }

    [JsonPropertyName("modifier")]
    public Modifier Modifier { get; set; }

    [JsonPropertyName("assignee")]
    public object Assignee { get; set; }

    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }

    [JsonPropertyName("modificationDate")]
    public DateTime ModificationDate { get; set; }

    [JsonPropertyName("nameTransposition")]
    public bool NameTransposition { get; set; }

    [JsonPropertyName("outstandingActions")]
    public bool OutstandingActions { get; set; }

    [JsonPropertyName("lastScreenedDatesByProviderType")]
    public LastScreenedDatesByProviderType LastScreenedDatesByProviderType { get; set; }

    [JsonPropertyName("results")]
    public List<Result> Results { get; set; }
    public object Error { get; internal set; }
}

