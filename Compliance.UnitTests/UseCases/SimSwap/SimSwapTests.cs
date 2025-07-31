using Xunit;
using System;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using System.Threading.Tasks;

namespace Compliance.UnitTests
{
    public class SimSwapTests
    {
        [Theory]
        [InlineData("25634987688", "UG", "MtnUgandaSimSwapRequest")]
        [InlineData("25406709878", "KE", "EquitelKenyaSimSwapRequest")]
        [InlineData("25407646372", "KE", "SafaricomSimSwapRequest")]
        [InlineData("25080454333", "RW", "AirtelRwandaSimSwapRequest")]
        [InlineData("25012345667", "RW", "MtnRwandaSimSwapRequest")]
        public async Task WhenCalled_CreatesProviderSpecificRequest(string phoneNumber, string countryCode, string expectedResult)
        {
            using var fixture = await ComplianceFixture.CreateAsync();

            //var mapper = fixture.Mapper;
            var telcoResolver = fixture.TelcoResolver;
            var simSwapRequestFactory = fixture.RequestFactory;

            var request = new SimSwapRequest
            {
                RequestId = Guid.NewGuid().ToString(),
                PhoneNumber = phoneNumber,
                CountryCode = System.Enum.Parse<Domain.Enum.CountryCode>(countryCode, true)
            };

            telcoResolver.TryResolve(request.CountryCode, request.PhoneNumber, out Telco telco);

            var result = simSwapRequestFactory.CreateTelcoProviderRequest(request, telco);

            Assert.NotNull(result);
            Assert.True(result.GetType().Name == expectedResult);
        }
    }
}