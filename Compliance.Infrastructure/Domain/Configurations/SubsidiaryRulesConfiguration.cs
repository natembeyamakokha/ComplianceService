using Newtonsoft.Json;
using Compliance.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Domain.Configurations
{
    internal class SubsidiaryRulesConfiguration : IEntityTypeConfiguration<SubsidiaryRules>
    {
        public void Configure(EntityTypeBuilder<SubsidiaryRules> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(
                pt => pt.ConfigValue
                ).HasConversion(c => JsonConvert.SerializeObject(c),
                t => JsonConvert.DeserializeObject<object>(t));


            builder.ToTable(nameof(SubsidiaryRules), "UTS");
        }
    }
}