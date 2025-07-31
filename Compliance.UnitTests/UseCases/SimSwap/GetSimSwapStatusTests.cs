using Xunit;
using System;
using System.Linq;
using Newtonsoft.Json;
using Compliance.Domain.Enum;
using System.Threading.Tasks;
using Compliance.Domain.Response;
using Compliance.Domain.Entity.SimSwapTasks;
using Compliance.Application.UseCases.SimSwap.GetSimSwapStatus;

namespace Compliance.UnitTests.UseCases.SimSwap;

[Collection("SequentialExecution")]
public class GetSimSwapStatusTests : IAsyncLifetime
{
    private Guid taskId;

    [Fact]
    public async Task GetSimSwapStatusShouldReturnResult()
    {
        var fixture = await ComplianceFixture.CreateAsync();

        var query = new GetSimSwapTaskStatusQuery(taskId);

        var result = await fixture.Mediator.Send(query, ComplianceFixture.CreateCancellation());

        Assert.NotNull(result);
        Assert.False(result.HasError);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task GetSimSwapStatusShouldReturnErrorNotFoundTask()
    {
        var fixture = await ComplianceFixture.CreateAsync();

        var query = new GetSimSwapTaskStatusQuery(Guid.NewGuid());

        var result = await fixture.Mediator.Send(query, ComplianceFixture.CreateCancellation());

        Assert.NotNull(result);
        Assert.True(result.HasError);
    }

    public async Task DisposeAsync()
    {
        var fixture = await ComplianceFixture.CreateAsync();
        var all = fixture.SimSwapCheckTaskRepository.Find().ToList();
        fixture.SimSwapCheckTaskRepository.RemoveAndSaveChanges(all);
        return;
    }

    public async Task InitializeAsync()
    {
        var fixture = await ComplianceFixture.CreateAsync();
        var successfulTask = SimSwapCheckTask
            .Create(
            (int)SimSwapCheckTaskStatus.Completed,
            nameof(SimSwapCheckTaskStatus.Completed),
            "commandname", "commandPayload", "KE", "53434234242", "Onboarding module", "operation").Value;

        var simSwapResponse = new SimSwapResponse { ApiReached = true, IsSuccessful = true, IsSwaped = false, PhoneNumber = "25453434343", LastSwap = DateTime.UtcNow.AddDays(-40) };

        Shared.Result<BulkSimSwapResponse> response = Shared.Result<BulkSimSwapResponse>.Success(
            new BulkSimSwapResponse
            {
                SimSwapChecksPassed = true,
                SimSwapResponse = [simSwapResponse],
                Successful = true
            }
        );

        successfulTask.MarkTaskAsCompleted(JsonConvert.SerializeObject(response.ResponseObject));

        fixture.SimSwapCheckTaskRepository.AddAndSaveChanges(successfulTask);

        taskId = successfulTask.Id;

        return;
    }
}
