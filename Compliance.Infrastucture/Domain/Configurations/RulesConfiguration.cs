using Compliance.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Domain.Configurations
{
    internal class RulesConfiguration : IEntityTypeConfiguration<Rules>
    {
        public void Configure(EntityTypeBuilder<Rules> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable(nameof(Rules), "UTS");
        }
    }
}