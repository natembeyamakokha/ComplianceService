using Omni;
using Autofac;
using MediatR;
using Newtonsoft.Json;
using Compliance.Domain.Enum;
using Omni.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Omni.Domain.Repositories.Enums;
using Compliance.Application.Contracts;
using Compliance.Domain.Configurations;
using Compliance.Domain.Entity.Interfaces;
using Compliance.Domain.Entity.SimSwapTasks;

namespace Compliance.Infrastructure.Services;

public class BackgroundSimSwapCheckService : IBackgroundSimSwapCheckService, IDisposable
{
    private readonly Random _jitter;
    private readonly int _nextRunHour;
    private readonly int _taskDelayInMs;
    private readonly IMediator _mediator;
    private readonly int _maxAttemptCount;
    private readonly int _maxTaskDelayInMs;
    private readonly int _takeTaskCount;
    private readonly TaskQueue[] _taskQueues;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BackgroundSimSwapCheckService> _logger;

    public BackgroundSimSwapCheckService(
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ILogger<BackgroundSimSwapCheckService> logger,
        SimSwapCheckTaskJobSetting simSwapCheckTaskJobSetting)
    {
        _logger = logger;
        _mediator = mediator;
        _jitter = new Random();
        _unitOfWork = unitOfWork;
        _taskQueues = Enumerable
            .Range(0, simSwapCheckTaskJobSetting.TaskQueueCount)
            .Select(t => new TaskQueue())
            .ToArray();

        _nextRunHour = simSwapCheckTaskJobSetting.NextRunHour;
        _maxAttemptCount = simSwapCheckTaskJobSetting.MaximumAttemptCount;
        _taskDelayInMs = simSwapCheckTaskJobSetting.IntervalMilliSeconds;
        _maxTaskDelayInMs = simSwapCheckTaskJobSetting.MaxIntervalMilliSeconds;
        _takeTaskCount = simSwapCheckTaskJobSetting.MaxTaskCount;
    }

    public async Task ExecuteOperationAsync(CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<ISimSwapCheckTaskRepository>();

        //TODO: We have to add the limit to the query
        var pendingTasks = (await repository
            .FindAsync(t =>
                t.NextRunAt <= DateTime.UtcNow
                && t.TaskStatusDescription == nameof(SimSwapCheckTaskStatus.Pending),
                QueryType.Tracking,
                cancellationToken: cancellationToken))
                .Take(_takeTaskCount);

        foreach (var pendingTask in pendingTasks)
        {
            UpdateTaskStatus(pendingTask);
            repository.Update(pendingTask);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var runningTask = pendingTasks.Where(t => t.TaskStatusDescription == nameof(TaskStatus.Running)).ToArray();

        if (runningTask is not null && runningTask.Length > 0)
            await ProcessTaskAsync(runningTask, cancellationToken);
    }

    private async ValueTask ProcessTaskAsync(SimSwapCheckTask[] runningTasks, CancellationToken cancellationToken)
    {
        var taskRepository = _unitOfWork.Repository<ISimSwapCheckTaskRepository>();

        //TODO: Create task buffer 
        //TODO: Create TaskQueue to hold running task

        int taskQueueIndex = 0;
        foreach (var task in runningTasks)
        {
            try
            {
                if (taskQueueIndex >= _taskQueues.Length)
                    taskQueueIndex = 0;

                _taskQueues[taskQueueIndex].Add(async (ct) =>
                {
                    await HandleTaskAsync(taskRepository, task, ct);
                }
                ,async callback =>
                {
                    if (callback.IsCompletedSuccessfully)
                    {
                        // no-ops
                        _logger.LogInformation("Competed successfully");
                    }
                    if (callback.IsFaulted)
                    {
                        var ex = callback.Exception;
                        _logger.LogError(ex, ex.Message);
                        //TODO: log exception
                    }
                });
                taskQueueIndex++;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        foreach (var taskQueue in _taskQueues)
            await taskQueue.Wait();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task HandleTaskAsync(ISimSwapCheckTaskRepository taskRepository, SimSwapCheckTask task, CancellationToken cancellationToken)
    {
        var appAssembly = typeof(IOnboardingModule).Assembly;
        Type commandType = appAssembly.GetType(task.CommandName)!;

        var command = JsonConvert.DeserializeObject(task.CommandPayload, commandType);

        var commandResult = await _mediator.Send(command, cancellationToken);

        var result = commandResult as Result<string>;
        if (result.HasError)
        {
            if (result.Error.IsTransient)
            {
                var delay = ComputeBackOffDelay(task.AttemptCount);
                task.RescheduleNextTaskRun(result.Error.FullMessage, delay);
                taskRepository.Update(task);
                return;
            }

            //Do we want to fail the here instead
            task.MarkTaskAsFailed(result.Error.FullMessage);
            taskRepository.Update(task);
            return;
        }

        task.MarkTaskAsCompleted(result.Value);
        taskRepository.Update(task);
    }

    private void UpdateTaskStatus(SimSwapCheckTask task)
    {
        if (IsExpired(task))
        {
            task.MarkTaskAsExpired();
            return;
        }

        if (MaximumAttemptReached(task))
        {
            task.MarkTaskAsFailed("Maximum try count reached");
            return;
        }

        task.MarkTaskAsRunning();
    }

    private bool MaximumAttemptReached(SimSwapCheckTask task)
    {
        return task.AttemptCount >= _maxAttemptCount;
    }

    private bool IsExpired(SimSwapCheckTask task)
    {
        return task.NextRunAt < DateTime.UtcNow.AddHours(-(_nextRunHour * _maxAttemptCount));
    }

    private int ComputeBackOffDelay(int retryCount)
    {
        var expDelay = Math.Pow(2, retryCount) * _taskDelayInMs;
        var randomJitter = _jitter.NextDouble();
        var delayInterval = expDelay * randomJitter;

        return (int)Math.Min(_maxTaskDelayInMs, delayInterval);
    }

    public void Dispose()
    {
        foreach (var taskQueue in _taskQueues)
        {
            taskQueue.Dispose();
        }
    }
}