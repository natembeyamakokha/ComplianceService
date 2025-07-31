using Compliance.Application.UseCases.SimSwap.ProcessVerifySimSwap;
using Compliance.Domain.Entity.SimSwapTasks;
using Compliance.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Compliance.UnitTests.Services;

[Collection("SequentialExecution")]
public class BackgroundJobRescheduledTaskTests : IAsyncLifetime
{
    private Guid _pendingTaskId;

    [Fact]
    public async Task ExpiredAndFailedTasksShouldBeUpdated()
    {
        var fixture = await ComplianceFixture.CreateAsync();

        await fixture.BackgroundSimSwapCheckService.ExecuteOperationAsync(ComplianceFixture.CreateCancellation(60));
        
        //Completed task assertion.
        var resechduledTask = fixture.SimSwapCheckTaskRepository.FindSingle(_pendingTaskId);
        Assert.NotNull(resechduledTask);
        Assert.Equal((int)SimSwapCheckTaskStatus.Pending, resechduledTask.TaskStatusId);
        Assert.True(resechduledTask.NextRunAt >= resechduledTask.LastRunAt.Value.AddMilliseconds(30000));
    }

    public async Task InitializeAsync()
    {
        var fixture = await ComplianceFixture.CreateAsync();
        CreatePendingTask(fixture);
    }

    private void CreatePendingTask(ComplianceFixture fixture)
    {
        List<string> phoneNumbers = new List<string>() { "251434343423", "251434343423" };
        
       var command = new ProcessVerifySimSwapCommand(
                30,
                "KE",
                phoneNumbers,
                "http://notifycallerurl/id", 
                "5434333433");

        var pendingTask = SimSwapCheckTask.Create(
                            (int)SimSwapCheckTaskStatus.Pending,
                            nameof(SimSwapCheckTaskStatus.Pending),
                            typeof(ProcessVerifySimSwapCommand).FullName,
                            JsonConvert.SerializeObject(command),
                            "KE",
                            "33242342",
                            "dfadf",
                            nameof(Operation.VerifySimSwap)
                            ).Value;

        fixture.SimSwapCheckTaskRepository.AddAndSaveChanges(pendingTask);
        _pendingTaskId = pendingTask.Id;
    }

    public async Task DisposeAsync()
    {
        var fixture = await ComplianceFixture.CreateAsync();
        var all = fixture.SimSwapCheckTaskRepository.Find().ToList();
        fixture.SimSwapCheckTaskRepository.RemoveAndSaveChanges(all);
        return;
    }
}
