using Compliance.Domain.Entity.SimSwapTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Domain.Configurations.SimSwapTasks;

internal class SimSwapResultHistoryConfiguration : IEntityTypeConfiguration<SimSwapResultHistory>
{
    public void Configure(EntityTypeBuilder<SimSwapResultHistory> builder)
    {
        builder.HasKey(m => m.Id);
        builder.ToTable(nameof(SimSwapResultHistory), "UTS");

        builder.Property(h => h.ResultData).HasMaxLength(2000);

        // Configuring the foreign key
        builder.HasOne(e => e.SimSwapCheckTask)
               .WithMany(t => t.SimSwapResultHistories)
               .HasForeignKey(e => e.TaskId);
    }
}
