using Compliance.Domain.Entity.Interfaces;
using Compliance.Domain.Entity.SimSwapTasks;
using Compliance.Infrastructure.Domain;
using Omni.Repository.EFCore.Repositories;

namespace Compliance.Infrastructure.Domain;

internal sealed class SimSwapCheckTaskRepository : Repository<UtilityServiceContext, SimSwapCheckTask, Guid>, ISimSwapCheckTaskRepository
{
    public SimSwapCheckTaskRepository(UtilityServiceContext dbContext) : base(dbContext)
    {         
    }
}
