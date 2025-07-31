using Compliance.Domain.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Omni.Repository.EFCore;
using Compliance.Domain.Entity.SimSwapTasks;

namespace Compliance.Infrastructure.Domain
{
    public class UtilityServiceContext : OmniDbContextBase
    {
        private readonly ILoggerFactory _loggerFactory;

        public UtilityServiceContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public DbSet<Rules> Rules { get; set; }
        public DbSet<Journies> Journies { get; set; }
        public DbSet<Channels> Channels { get; set; }
        public DbSet<Subsidiaries> Subsidiaries { get; set; }
        public DbSet<SubsidiaryChannels> SubsidiaryChannels { get; set; }
        public DbSet<SubsidiaryRules> SubsidiaryRules { get; set; }
        public DbSet<SimSwapCheckTask> SimSwapCheckTasks { get; set; }
        public DbSet<SimSwapResultHistory> SimSwapResultHistories { get; set; }
    }

    public class UtilityServiceContextFactory : IDesignTimeDbContextFactory<UtilityServiceContext>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;

        public UtilityServiceContextFactory(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _loggerFactory = loggerFactory;
            _configuration = configuration;
        }

        public UtilityServiceContextFactory() { }

        public UtilityServiceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UtilityServiceContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

            return new UtilityServiceContext(optionsBuilder.Options, _loggerFactory);
        }
    }
}