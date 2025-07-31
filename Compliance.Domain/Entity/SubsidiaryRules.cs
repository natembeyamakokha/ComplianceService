using Compliance.Shared.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance.Domain.Entity
{
    [Table(nameof(SubsidiaryRules), Schema = "UTS")]
    public class SubsidiaryRules : BaseEntity<long>
    {
        public long RuleId { get; private set; }
        public long SubsidiaryId { get; private set; }
        public long ChannelId { get; private set; }
        public long JourneyId { get; private set; }
        public bool IsApplicable { get; private set; }
        public object ConfigValue { get; private set; }
        public bool TerminateOnFailure { get; set; }

        private SubsidiaryRules(long channelId, long subsidiaryId, long ruleId, bool isApplicable, long journeyId = 0, object configValue = null)
        {
            ChannelId = channelId;
            SubsidiaryId = subsidiaryId;
            RuleId = ruleId;
            IsApplicable = isApplicable;
            ConfigValue = configValue;
            JourneyId = journeyId;
        }

        public SubsidiaryRules() { }

        public static SubsidiaryRules Create(long channelId, long subsidiaryId, long ruleId, bool isApplicable, long journeyId, object configValue = null)
        {
            return new SubsidiaryRules(channelId, subsidiaryId, ruleId, isApplicable, journeyId, configValue);
        }
    }
}