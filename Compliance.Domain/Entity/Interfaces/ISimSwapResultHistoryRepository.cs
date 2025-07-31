using Compliance.Domain.Entity.SimSwapTask;
using Compliance.Domain.Entity.SimSwapTasks;
using Omni.Domain.Repositories;

namespace Compliance.Domain.Entity.Interfaces;

public interface ISimSwapResultHistoryRepository : IRepository<SimSwapResultHistory, Guid>
{
}