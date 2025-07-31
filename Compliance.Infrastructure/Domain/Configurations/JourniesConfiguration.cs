using Compliance.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Domain.Configurations
{
    internal class JourniesConfiguration : IEntityTypeConfiguration<Journies>
    {
        public void Configure(EntityTypeBuilder<Journies> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable(nameof(Journies), "UTS");
        }
    }
}