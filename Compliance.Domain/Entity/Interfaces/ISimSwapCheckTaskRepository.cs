using Compliance.Domain.Entity.SimSwapTasks;
using Omni.Domain.Repositories;

namespace Compliance.Domain.Entity.Interfaces;

public interface ISimSwapCheckTaskRepository : IRepository<SimSwapCheckTask, Guid>
{
}