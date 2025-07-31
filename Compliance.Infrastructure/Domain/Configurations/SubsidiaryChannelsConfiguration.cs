using Compliance.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Domain.Configurations
{
    internal class SubsidiaryChannelsConfiguration : IEntityTypeConfiguration<SubsidiaryChannels>
    {
        public void Configure(EntityTypeBuilder<SubsidiaryChannels> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable(nameof(SubsidiaryChannels), "UTS");
        }
    }
}