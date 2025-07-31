using Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;
using Compliance.Domain.Response;
using Newtonsoft.Json;
using Omni;
using System.Threading.Tasks;
using Xunit;

namespace Compliance.UnitTests.UseCases.SimSwap;

public class ProcessNotifySimSwapResultTests
{
    [Fact]
    public async Task ProcessVerifySimSwapShouldSucceedForValidPayload()
    {
        var fixture = await ComplianceFixture.CreateAsync();

        var payload = new BulkSimSwapResponse { SimSwapChecksPassed = true, Successful = true};

        var command = new ProcessNotifySimSwapResultCommand(
            "http://shouldsucceedurl",
            JsonConvert.SerializeObject(payload));

        var result = await fixture.Mediator.Send(command, ComplianceFixture.CreateCancellation());

        Assert.NotNull(result);
        Assert.False(result.HasError);
    }

    [Fact]
    public async Task ProcessVerifySimSwapShouldFailForAnInvalidPayload()
    {
        var fixture = await ComplianceFixture.CreateAsync();

        var command = new ProcessNotifySimSwapResultCommand(
            "http://shouldsucceedurl",
            "invalidPayload");

        var result = await fixture.Mediator.Send(command, ComplianceFixture.CreateCancellation());

        Assert.NotNull(result);
        Assert.True(result.HasError);
        Assert.True(result.Error is Error { ErrorCode: Shared.StatusCodes.INVALID_REQUEST });
    }
}
