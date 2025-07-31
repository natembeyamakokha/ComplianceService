using Compliance.Shared.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance.Domain.Entity
{
    [Table(nameof(SubsidiaryChannels), Schema = "UTS")]
    public class SubsidiaryChannels : BaseEntity<long>
    {
        public long ChannelId { get; private set; }
        public long SubsidiaryId { get; private set; }

        private SubsidiaryChannels(long channelId, long subsidiaryId)
        {
            ChannelId = channelId;
            SubsidiaryId = subsidiaryId;
        }

        public SubsidiaryChannels() { }

        public static SubsidiaryChannels Create(long channelId, long subsidiaryId)
        {
            return new SubsidiaryChannels(channelId, subsidiaryId);
        }
    }
}