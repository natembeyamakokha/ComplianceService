using Compliance.Domain.Entity.Interfaces;
using Compliance.Domain.Entity.SimSwapTasks;
using Omni.Repository.EFCore.Repositories;

namespace Compliance.Infrastructure.Domain;

public class SimSwapResultHistoryRepository : Repository<UtilityServiceContext, SimSwapResultHistory, Guid>, ISimSwapResultHistoryRepository
{
    public SimSwapResultHistoryRepository(UtilityServiceContext dbContext) : base(dbContext)
    {        
    }
}
