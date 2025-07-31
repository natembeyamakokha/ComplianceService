using Compliance.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Domain.Configurations
{
    internal class SubsidiariesConfiguration : IEntityTypeConfiguration<Subsidiaries>
    {
        public void Configure(EntityTypeBuilder<Subsidiaries> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable(nameof(Subsidiaries), "UTS");
        }
    }
}