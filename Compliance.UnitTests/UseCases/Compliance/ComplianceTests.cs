using Xunit;
using System.Threading.Tasks;
using Compliance.UnitTests.Mock.Services;
using Compliance.Application.UseCases.Compliance.VerifyFraudStatus;
using System;

namespace Compliance.UnitTests.UseCases.Compliance
{
    public class ComplianceTests
    {
        //     [Theory]
        //     [InlineData(WorldCheckAPIMock.VALID_NAME, true)]
        //     [InlineData(WorldCheckAPIMock.INVALID_NAME, false)]
        //     public async Task VerifyFraudStatusAsync_WhenCalled_ReturnsAppropriateResult(string customerName, bool isCompliant)
        //     {
        //         using var fixture = await ComplianceFixture.CreateAsync();

        //         var mediator = fixture.Mediator;
        //         var cancellationToken = ComplianceFixture.CreateCancellation(60);

        //         var command = new VerifyFraudStatusCommand(customerName);

        //         var result = await mediator.Send(command, cancellationToken);

        //         Assert.NotNull(result);
        //         Assert.Equal(isCompliant, result.ResponseObject.IsCompliant);
        //     }

        //     [Fact]
        //     public async Task VerifyFraudStatusAsync_WhenCalled_ThrowsAnException()
        //     {
        //         using var fixture = await ComplianceFixture.CreateAsync();

        //         var mediator = fixture.Mediator;
        //         var cancellationToken = ComplianceFixture.CreateCancellation(60);

        //         var command = new VerifyFraudStatusCommand(WorldCheckAPIMock.ERROR_NAME);

        //         await Assert.ThrowsAnyAsync<Exception>(async () => await mediator.Send(command, cancellationToken));
        //     }
    }
}