using Compliance.Domain.Entity.SimSwapTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Domain.Configurations.SimSwapTasks;

internal class SimSwapCheckTaskConfiguration : IEntityTypeConfiguration<SimSwapCheckTask>
{
    public void Configure(EntityTypeBuilder<SimSwapCheckTask> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(t => t.TaskStatusDescription).HasMaxLength(100);
        builder.Property(t => t.Source).HasMaxLength(100);
        builder.Property(t => t.Operation).HasMaxLength(100);
        builder.Property(t => t.Data).HasMaxLength(2000);
        builder.Property(t => t.CustomerId).HasMaxLength(100);
        builder.Property(t => t.CountryCode).HasMaxLength(25);
        builder.Property(t => t.CommandName).HasMaxLength(256);
        builder.Property(t => t.CommandPayload).HasMaxLength(1000);
        builder.Property(t => t.ErrorMessage).HasMaxLength(1000);

        builder.ToTable(nameof(SimSwapCheckTask), "UTS");
    }
}
