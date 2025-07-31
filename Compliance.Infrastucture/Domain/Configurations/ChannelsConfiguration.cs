using Compliance.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Domain.Configurations
{
    internal class ChannelsConfiguration : IEntityTypeConfiguration<Channels>
    {
        public void Configure(EntityTypeBuilder<Channels> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable(nameof(Channels), "UTS");
        }
    }
}