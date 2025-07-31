using Omni;
using Omni.Domain;
using Omni.Domain.Entities;
using Compliance.Domain.Enum;
using Compliance.Domain.Entity.SimSwapTask.Event;

namespace Compliance.Domain.Entity.SimSwapTasks;

public class SimSwapCheckTask : BaseEntity<Guid>
{
    public int TaskStatusId { get; private set; }
    public string TaskStatusDescription { get; private set; }
    public string CommandName { get; private set; }
    public string CommandPayload { get; private set; }
    public DateTime? LastRunAt { get; private set; }
    public DateTime NextRunAt { get; private set; }
    public int AttemptCount { get; private set; }
    public string CountryCode { get; private set; }
    public string CustomerId { get; private set; }
    public string Source { get; private set; }
    public string Data { get; private set; }
    public string ErrorMessage { get; private set; }
    public string Operation { get; private set; }
    public ICollection<SimSwapResultHistory> SimSwapResultHistories { get; private set; }

    private SimSwapCheckTask() { }

    private SimSwapCheckTask(
        int taskStatusId,
        string taskStatusDescription,
        string commandName,
        string commandPayload,
        string countryCode,
        string customerId,
        string source,
        string operation)
    {
        TaskStatusId = taskStatusId;
        TaskStatusDescription = taskStatusDescription;
        CommandName = commandName;
        CommandPayload = commandPayload;
        NextRunAt = DateTime.UtcNow;
        CountryCode = countryCode;
        CustomerId = customerId;
        Source = source;
        Operation = operation;
        //Add Task Created Event here.
    }

    public static Result<SimSwapCheckTask> Create(
        int taskStatusId,
        string taskStatusDescription,
        string commandName,
        string commandPayload,
        string countryCode,
        string customerId,
        string source,
        string operation)
    {
        var simSwapCheckTask = new SimSwapCheckTask(
            taskStatusId: taskStatusId,
            taskStatusDescription: taskStatusDescription,
            commandName: commandName,
            commandPayload: commandPayload,
            countryCode: countryCode,
            customerId: customerId,
            source: source,
            operation: operation);

        return Result<SimSwapCheckTask>
            .Create(simSwapCheckTask)
            .Validate(simSwapCheckTask.TaskStatusId.IsNotNull())
            .Validate(simSwapCheckTask.TaskStatusDescription.IsRequired())
            .Validate(simSwapCheckTask.CommandName.IsRequired())
            .Validate(simSwapCheckTask.CommandPayload.IsRequired())
            .Validate(simSwapCheckTask.CountryCode.IsRequired())
            .Validate(simSwapCheckTask.CustomerId.IsRequired())
            .Validate(simSwapCheckTask.Source.IsRequired())
            .Validate(simSwapCheckTask.Operation.IsRequired());
    }

    public void MarkTaskAsCompleted(string data)
    {
        Data = data;
        TaskStatusId = (int)SimSwapCheckTaskStatus.Completed;
        TaskStatusDescription = nameof(SimSwapCheckTaskStatus.Completed);
        LastRunAt = DateTime.UtcNow;

        AddEvent(new SimSwapTaskCompletedDomainEvent(Id, DateTime.UtcNow, TaskStatusDescription));
    }

    public void RescheduleNextTaskRun(string errorMessage, int runAfterMilliSecond)
    {
        ErrorMessage = errorMessage;
        LastRunAt = DateTime.UtcNow;
        NextRunAt = DateTime.UtcNow.AddMilliseconds(runAfterMilliSecond);
        TaskStatusId = (int)SimSwapCheckTaskStatus.Pending;
        TaskStatusDescription = nameof(SimSwapCheckTaskStatus.Pending);
    }

    public void MarkTaskAsExpired()
    {
        TaskStatusId = (int)SimSwapCheckTaskStatus.Expired;
        TaskStatusDescription = nameof(SimSwapCheckTaskStatus.Expired);

        AddEvent(new SimSwapTaskExpiredDomainEvent(Id, DateTime.UtcNow, TaskStatusDescription));
    }

    public void MarkTaskAsRunning()
    {
        AttemptCount++;
        LastRunAt = DateTime.UtcNow;
        TaskStatusId = (int)SimSwapCheckTaskStatus.Running;
        TaskStatusDescription = nameof(SimSwapCheckTaskStatus.Running);
    }

    public void MarkTaskAsFailed(string errorMessage)
    {
        TaskStatusId = (int)SimSwapCheckTaskStatus.Failed;
        TaskStatusDescription = nameof(SimSwapCheckTaskStatus.Failed);
        ErrorMessage = errorMessage;

        AddEvent(new SimSwapTaskFailedDomainEvent(Id, DateTime.UtcNow, TaskStatusDescription));
    }

    public void AddSimSwapTaskEvent(DomainEventBase @event)
    {
        AddEvent(@event);
    }
}