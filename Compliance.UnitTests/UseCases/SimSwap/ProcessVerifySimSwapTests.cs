using Xunit;
using System.Linq;
using Compliance.Domain.Enum;
using System.Threading.Tasks;
using System.Collections.Generic;
using Compliance.Application.UseCases.SimSwap.ProcessVerifySimSwap;
using Omni;

namespace Compliance.UnitTests.UseCases.SimSwap;

[Collection("SequentialExecution")]
public class ProcessVerifySimSwapTests : IAsyncLifetime
{
    [Fact]
    public async Task ProcessVerifySimSwapShouldSucceedAndCreateTask()
    {
        var fixture = await ComplianceFixture.CreateAsync();

        var command = new ProcessVerifySimSwapCommand(
            30,
            "KE",
            new List<string> { "2540675748", "2540675343" },
            "https://callbackurl",
            "3453452");

        var result = await fixture.Mediator.Send(command, ComplianceFixture.CreateCancellation());

        Assert.NotNull(result);
        Assert.False(result.HasError);

        var pendingNotifyTasks = fixture.SimSwapCheckTaskRepository.Find(t => t.TaskStatusDescription == nameof(SimSwapCheckTaskStatus.Pending));
        Assert.NotNull(pendingNotifyTasks);
        Assert.True(pendingNotifyTasks.Any());
    }

    [Fact]
    public async Task ProcessVerifySimSwapShouldFailForNotSupportedNumber()
    {
        var fixture = await ComplianceFixture.CreateAsync();

        var command = new ProcessVerifySimSwapCommand(
            30,
            "KE",
            new List<string> { "211212121", "211212121" },
            "https://callbackurl",
            "3453452");

        var result = await fixture.Mediator.Send(command, ComplianceFixture.CreateCancellation());

        Assert.NotNull(result);
        Assert.True(result.HasError);
        Assert.True(result.Error is Error {ErrorCode : Shared.StatusCodes.INVALID_REQUEST });
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        var fixture = await ComplianceFixture.CreateAsync();
        var all = fixture.SimSwapCheckTaskRepository.Find().ToList();
        fixture.SimSwapCheckTaskRepository.RemoveAndSaveChanges(all);
        return;
    }
}