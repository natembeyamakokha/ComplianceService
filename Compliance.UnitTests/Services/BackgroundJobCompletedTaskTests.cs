using Xunit;
using System;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Compliance.Domain.Enum;
using System.Collections.Generic;
using Compliance.Domain.Entity.Interfaces;
using Compliance.Domain.Entity.SimSwapTasks;
using Compliance.Application.UseCases.SimSwap.ProcessVerifySimSwap;

namespace Compliance.UnitTests.Services;

[Collection("SequentialExecution")]
public class BackgroundJobCompletedTaskTests : IAsyncLifetime
{
    private Guid _pendingTaskId;

    [Fact]
    public async Task ExpiredAndFailedTasksShouldBeUpdated()
    {
        using var fixture = await ComplianceFixture.CreateAsync();
        var simSwapCheckTaskRepository = fixture.SimSwapCheckTaskRepository;
        var simSwapCheckWorkerService = fixture.BackgroundSimSwapCheckService;

        await simSwapCheckWorkerService.ExecuteOperationAsync(ComplianceFixture.CreateCancellation(30));

        //Completed task assertion.
        var completedTask = await simSwapCheckTaskRepository.FindSingleAsync(_pendingTaskId);
        Assert.NotNull(completedTask);
        Assert.Equal(nameof(SimSwapCheckTaskStatus.Completed), completedTask.TaskStatusDescription);

        //verify task created assertion
        var verifyTask = await simSwapCheckTaskRepository.FindSingleAsync(t => t.Operation == nameof(Operation.NotifySimSwapResult));
        Assert.NotNull(verifyTask);
        Assert.Equal(nameof(SimSwapCheckTaskStatus.Pending), verifyTask.TaskStatusDescription);
    }

    public async Task InitializeAsync()
    {
        var fixture = await ComplianceFixture.CreateAsync();
        var repository = fixture.SimSwapCheckTaskRepository;
        var canllationToken = ComplianceFixture.CreateCancellation(10);

        await CreatePendingTaskAsync(repository, canllationToken);
    }

    private async Task CreatePendingTaskAsync(ISimSwapCheckTaskRepository repository, CancellationToken cancellationToken)
    {
        List<string> phoneNumbers = ["2540675748", "2540675745"];

        var command = new ProcessVerifySimSwapCommand(
            30,
            "KE",
            phoneNumbers,
            "http://notifycallerurl/id",
            "5434333433");

        var pendingTaskResult = SimSwapCheckTask
            .Create(
            (int)SimSwapCheckTaskStatus.Pending,
            nameof(SimSwapCheckTaskStatus.Pending),
            typeof(ProcessVerifySimSwapCommand).FullName,
            JsonConvert.SerializeObject(command),
            "KE",
            "33242342",
            "dfadf",
            nameof(Operation.VerifySimSwap));

        Assert.False(pendingTaskResult.HasError);

        await repository.AddAndSaveChangesAsync(pendingTaskResult.Value, cancellationToken);
        _pendingTaskId = pendingTaskResult.Value.Id;
    }

    public async Task DisposeAsync()
    {
        using var fixture = await ComplianceFixture.CreateAsync();
        var repository = fixture.SimSwapCheckTaskRepository;
        var cancellationToken = ComplianceFixture.CreateCancellation();

        var simswapChecks = await repository.FindAsync(cancellationToken: cancellationToken);
        await repository.RemoveAndSaveChangesAsync(simswapChecks, cancellationToken);
    }
}
