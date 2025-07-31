using System.Text.Json.Serialization;

namespace Compliance.Infrastructure.Services.WorldCheck;


    public class Address
    {
        [JsonPropertyName("city")]
        public object City { get; set; }

        [JsonPropertyName("country")]
        public Country Country { get; set; }

        [JsonPropertyName("postCode")]
        public object PostCode { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("street")]
        public object Street { get; set; }
    }

    public class Country
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class CountryLink
    {
        [JsonPropertyName("country")]
        public Country Country { get; set; }

        [JsonPropertyName("countryText")]
        public string CountryText { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Event
    {
        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("allegedAddresses")]
        public List<object> AllegedAddresses { get; set; }

        [JsonPropertyName("day")]
        public int? Day { get; set; }

        [JsonPropertyName("fullDate")]
        public string FullDate { get; set; }

        [JsonPropertyName("month")]
        public int? Month { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }
    }

    public class Field
    {
        [JsonPropertyName("typeId")]
        public string TypeId { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("dateTimeValue")]
        public string DateTimeValue { get; set; }
    }

    public class MatchedTerm
    {
        [JsonPropertyName("term")]
        public string Term { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("isPartOfUpdatedResults")]
        public bool IsPartOfUpdatedResults { get; set; }
    }

    public class Modifier
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

    public class Resolution
    {
        [JsonPropertyName("statusId")]
        public string StatusId { get; set; }

        [JsonPropertyName("riskId")]
        public string RiskId { get; set; }

        [JsonPropertyName("reasonId")]
        public string ReasonId { get; set; }

        [JsonPropertyName("resolutionRemark")]
        public string ResolutionRemark { get; set; }

        [JsonPropertyName("resolutionDate")]
        public DateTime ResolutionDate { get; set; }
    }

    public class ResultReview
    {
        [JsonPropertyName("reviewRequired")]
        public bool ReviewRequired { get; set; }

        [JsonPropertyName("reviewRequiredDate")]
        public DateTime ReviewRequiredDate { get; set; }

        [JsonPropertyName("reviewRemark")]
        public object ReviewRemark { get; set; }

        [JsonPropertyName("reviewDate")]
        public object ReviewDate { get; set; }
    }

    public class NameResultResponse
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
        public List<SecondaryFieldResult> SecondaryFieldResults { get; set; }

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

        [JsonPropertyName("modifier")]
        public Modifier Modifier { get; set; }

        [JsonPropertyName("resolution")]
        public Resolution Resolution { get; set; }

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

    public class SecondaryFieldResult
    {
        [JsonPropertyName("field")]
        public Field Field { get; set; }

        [JsonPropertyName("typeId")]
        public string TypeId { get; set; }

        [JsonPropertyName("submittedValue")]
        public string SubmittedValue { get; set; }

        [JsonPropertyName("submittedDateTimeValue")]
        public string SubmittedDateTimeValue { get; set; }

        [JsonPropertyName("matchedValue")]
        public string MatchedValue { get; set; }

        [JsonPropertyName("matchedDateTimeValue")]
        public string MatchedDateTimeValue { get; set; }

        [JsonPropertyName("fieldResult")]
        public string FieldResult { get; set; }
    }