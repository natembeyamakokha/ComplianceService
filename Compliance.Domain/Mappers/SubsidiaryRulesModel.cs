namespace Compliance.Domain.Mappers
{
    public interface ISubsidiaryRulesModel
    {
        public long Id { get; set; }
        public long RuleId { get; set; }
        public bool IsApplicable { get; set; }
        public long ChannelId { get; set; }
        public long JourneyId { get; set; }
        public long SubsidiaryId { get; set; }
        public object ConfigValue { get; set; }
        public string ChannelName { get; set; }
        public string RuleName { get; set; }
        public string JourneyName { get; set; }
        public string SubsidiaryName { get; set; }
        public string SubsidiaryCode { get; set; }
        public bool TerminateOnFailure { get; set; }
    }
    
    public class SubsidiaryRulesModel : ISubsidiaryRulesModel
    {
        public long Id { get; set; }
        public long RuleId { get; set; }
        public bool IsApplicable { get; set; }
        public long ChannelId { get; set; }
        public long JourneyId { get; set; }
        public long SubsidiaryId { get; set; }
        public object ConfigValue { get; set; }
        public string ChannelName { get; set; }
        public string RuleName { get; set; }
        public string JourneyName { get; set; }
        public string SubsidiaryName { get; set; }
        public string SubsidiaryCode { get; set; }
        public bool TerminateOnFailure { get; set; }
    }
}