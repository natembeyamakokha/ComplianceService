using Omni;
using Omni.Domain.Entities;

namespace Compliance.Domain.Entity.SimSwapTasks;

public class SimSwapResultHistory : BaseEntity<Guid>
{
    public Guid TaskId { get; private set; }
    public SimSwapCheckTask SimSwapCheckTask { get; set; }
    public string ResultData { get; private set; }

    private SimSwapResultHistory() { }

    private SimSwapResultHistory(
        Guid taskId,
        string resultData)
    {
        TaskId = taskId;
        ResultData = resultData;
    }

    public static Result<SimSwapResultHistory> Create(
         Guid taskId,
         string resultData)
    {
        var simSwapResultHistory = new SimSwapResultHistory(
            taskId: taskId,
            resultData: resultData);

        return Result<SimSwapResultHistory>
            .Create(simSwapResultHistory)
            .Validate(simSwapResultHistory.TaskId.IsNotNull())
            .Validate(simSwapResultHistory.ResultData.IsRequired());
    }
}