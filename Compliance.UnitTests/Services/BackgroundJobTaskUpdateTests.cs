using Xunit;
using System;
using System.Threading.Tasks;
using Compliance.Domain.Enum;
using Compliance.Domain.Entity.SimSwapTasks;

namespace Compliance.UnitTests.Services;

[Collection("SequentialExecution")]
public class BackgroundJobTaskUpdateTests : IAsyncLifetime
{
    private Guid _failedTaskId;
    private Guid _expiredTaskId;

    [Fact]
    public async Task ExpiredAndFailedTasksShouldBeUpdated()
    {
        var fixture = await ComplianceFixture.CreateAsync();
        var repository = fixture.SimSwapCheckTaskRepository;
        var simSwapCheckWorkerService = fixture.BackgroundSimSwapCheckService;

        await simSwapCheckWorkerService.ExecuteOperationAsync(ComplianceFixture.CreateCancellation());

        var expiredTask = repository.FindSingle(_expiredTaskId);

        Assert.NotNull(expiredTask);
        Assert.Equal(nameof(SimSwapCheckTaskStatus.Expired), expiredTask.TaskStatusDescription);

        var failedTask = repository.FindSingle(_failedTaskId);

        Assert.NotNull(failedTask);
        Assert.Equal(nameof(SimSwapCheckTaskStatus.Failed), failedTask.TaskStatusDescription);
    }

    public async Task InitializeAsync()
    {
        var fixture = await ComplianceFixture.CreateAsync();
        CreateFailedTask(fixture);
        CreateExpiredTask(fixture);
    }

    private void CreateFailedTask(ComplianceFixture fixture)
    {
        var failedTaskResult = SimSwapCheckTask
            .Create(
            (int)SimSwapCheckTaskStatus.Pending,
            nameof(SimSwapCheckTaskStatus.Pending),
            "command",
            "payload",
            "KE",
            "33242342",
            "dfadf",
            nameof(Operation.NotifySimSwapResult));

        Assert.False(failedTaskResult.HasError);

        var failedTask = failedTaskResult.Value;
        for (int i = 0; i < 10; i++)
        {
            failedTask.MarkTaskAsRunning();
            failedTask.RescheduleNextTaskRun(nameof(SimSwapCheckTaskStatus.Pending), -1);
        }

        fixture.SimSwapCheckTaskRepository.AddAndSaveChanges(failedTask);
        _failedTaskId = failedTask.Id;
    }

    private void CreateExpiredTask(ComplianceFixture fixture)
    {
        var expiredTaskResult = SimSwapCheckTask
            .Create(
            (int)SimSwapCheckTaskStatus.Pending,
            nameof(SimSwapCheckTaskStatus.Pending),
            "command",
            "payload",
            "KE",
            "33242342",
            "dfadf",
            nameof(Operation.NotifySimSwapResult));

        Assert.False(expiredTaskResult.HasError);

        var expiredTask = expiredTaskResult.Value;
        expiredTask.RescheduleNextTaskRun(nameof(SimSwapCheckTaskStatus.Expired), -(6 * 8 * 60 * 60 * 1000) - 1000);
        fixture.SimSwapCheckTaskRepository.AddAndSaveChanges(expiredTask);
        _expiredTaskId = expiredTask.Id;
    }

    public async Task DisposeAsync()
    {
        using var fixture = await ComplianceFixture.CreateAsync();
        var repository = fixture.SimSwapCheckTaskRepository;
        var cancellationToken = ComplianceFixture.CreateCancellation(10);

        var simswapChecks = await repository.FindAsync(cancellationToken: cancellationToken);
        await repository.RemoveAndSaveChangesAsync(simswapChecks, cancellationToken);
    }
}
