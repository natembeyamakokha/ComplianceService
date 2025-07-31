using Xunit;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using Compliance.Domain.Response;
using Compliance.UnitTests.Mock.Services;
using Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares;

namespace Compliance.UnitTests
{
    public class OnboardingTests
    {
        [Theory]
        [InlineData("UG", "25635709876", "25", "OnboardingJourneyResponse")]
        [InlineData("SS", "21106709876", "25", "OnboardingJourneyResponse")]
        [InlineData("DRC", "24306709876", "25", "OnboardingJourneyResponse")]
        [InlineData("TZ", "25506709876", "25", "OnboardingJourneyResponse")]
        [InlineData("RW", "25086709876", "25", "OnboardingJourneyResponse")]
        [InlineData("KE", "25406709876", "25", "OnboardingJourneyResponse")]
        public async Task WhenCalled_ReturnsAnOnboardingJourneyResponse(string countryCode, string phoneNumber, string age, string expectedResult)
        {

            using var fixture = await ComplianceFixture.CreateAsync();

            var onboardingModule = fixture.OnboardingModule;

            var countryCodeEnum = Enum.Parse<CountryCode>(countryCode, ignoreCase: true);
            {
                var request = OnboardingJourneyRequest.Create(
                    Convert.ToInt32(age),
                    "USSD",
                    countryCodeEnum,
                    [
                        new()
                        {
                            IsMain = true,
                            PhoneNumber = phoneNumber
                        }
                    ]);

                var result = await onboardingModule.ApplyAsync(request, default);

                Assert.NotNull(result);
                Assert.True(result.GetType().Name == expectedResult);
            }
        }


        [Theory]
        [InlineData("UG", "UgandaOnboardingJourneyRequest")]
        [InlineData("KE", "KenyaOnboardingJourneyRequest")]
        [InlineData("TZ", "TanzaniaOnboardingJourneyRequest")]
        [InlineData("RW", "RwandaOnboardingJourneyRequest")]
        [InlineData("DRC", "DRCOnboardingJourneyRequest")]
        [InlineData("SS", "SouthSudanOnboardingJourneyRequest")]
        public async Task WhenCalled_ReturnsTheAppropriateProviderRequest(string countryCode, string expectedResult)
        {

            using var fixture = await ComplianceFixture.CreateAsync();

            var factory = fixture.OnboardingMapperFactory;
            var countryCodeEnum = Enum.Parse<CountryCode>(countryCode, ignoreCase: true);

            var request = OnboardingJourneyRequest.Create(25, "USSD", countryCodeEnum, new System.Collections.ObjectModel.Collection<Phone>{
                new() {
                    PhoneNumber = "25406709876", IsMain = true
                }
            });

            var result = factory.CreateOnboardingJourneyRequest(request);

            Assert.NotNull(result);
            Assert.True(result.GetType().Name == expectedResult);
        }

        [Fact]
        public async Task WhenAllRulesAppliesDRC_ReturnsAppropriateResult()
        {

            using var fixture = await ComplianceFixture.CreateAsync();

            var onboardingModule = fixture.OnboardingModule;

            var countryCodeEnum = Enum.Parse<CountryCode>("DRC", ignoreCase: true);
            {
                var request = OnboardingJourneyRequest.Create(30, "USSD", countryCodeEnum, new System.Collections.ObjectModel.Collection<Phone>{
                new() {
                    PhoneNumber = "25406709876", IsMain = true
                }
            });

                var result = await onboardingModule.ApplyAsync(request, default);
                var rules = result.Rules;

                Assert.NotNull(result);
                Assert.NotNull(rules);
                //Assert.True(result.AllRulesPassed);
                Assert.True(rules.Where(x => x.Name == nameof(AllowedSubsidiary)).FirstOrDefault().Result == nameof(RuleResult.Succeed));
                Assert.True(rules.Where(x => x.Name == nameof(BlockOnRegistration)).FirstOrDefault().Result == nameof(RuleResult.Succeed));
                Assert.True(rules.Where(x => x.Name == nameof(CoolOff)).FirstOrDefault().Result == nameof(RuleResult.Succeed));
                Assert.True(rules.Where(x => x.Name == nameof(MaximumAge)).FirstOrDefault().Result == nameof(RuleResult.Succeed));
                //Assert.True(rules.Where(x => x.Name == nameof(Simswap)).FirstOrDefault().Result == nameof(RuleResult.Succeed));
                Assert.NotNull(rules.Where(x => x.Name == nameof(CoolOff)).FirstOrDefault().ResultValue);
            }
        }

        [Fact]
        public async Task WhenSubsidiaryAllowedRuleIsFalseUganda_ReturnsAllowedSubsidiaryFailed()
        {

            using var fixture = await ComplianceFixture.CreateAsync();

            var onboardingModule = fixture.OnboardingModule;

            var countryCodeEnum = Enum.Parse<CountryCode>("UG", ignoreCase: true);
            {
                var request = OnboardingJourneyRequest.Create(25, "USSD", countryCodeEnum,
                    [
                        new()
                        {
                            PhoneNumber = "25406709876", IsMain = true
                        }
                    ]);
                var result = await onboardingModule.ApplyAsync(request, default);
                var rules = result.Rules;
                Assert.NotNull(result);
                Assert.NotNull(rules);
                Assert.False(result.AllRulesPassed);
                Assert.Contains(rules.Where(x => x.Name != nameof(AllowedSubsidiary)), x => x.Result != nameof(RuleResult.Succeed) && x.Result != nameof(RuleResult.Failed));
                Assert.True(rules.Where(x => x.Name == nameof(AllowedSubsidiary)).FirstOrDefault().Result == nameof(RuleResult.Failed));
            }
        }

        [Fact]
        public async Task WhenBlockOnRegistrationIsFalseForKenya_ReturnsBlockOnRegistrationFailed()
        {
            using var fixture = await ComplianceFixture.CreateAsync();

            var onboardingModule = fixture.OnboardingModule;

            var countryCodeEnum = Enum.Parse<CountryCode>("KE", ignoreCase: true);

            var request = OnboardingJourneyRequest.Create(30, "USSD", countryCodeEnum, new System.Collections.ObjectModel.Collection<Phone>{
                new() {
                    PhoneNumber = "25406709876", IsMain = true
                }
            });

            var result = await onboardingModule.ApplyAsync(request, default);

            Console.WriteLine(JsonConvert.SerializeObject(result.Rules));

            var rules = result.Rules;
            Assert.NotNull(result);
            Assert.NotNull(rules);
            Assert.False(result.AllRulesPassed);
            Assert.Equal(rules.FirstOrDefault(x => x.Name == nameof(Simswap)).Result, RuleResult.NotApplied.ToString());
            Assert.Equal(rules.FirstOrDefault(x => x.Name == nameof(MaximumAge)).Result, RuleResult.NotApplied.ToString());
            Assert.Equal(rules.FirstOrDefault(x => x.Name == nameof(BlockOnRegistration)).Result, RuleResult.Failed.ToString());
        }

        [Fact]
        public async Task WhenCoolOffIsCheckedTanzania_ReturnsCoolOffValue()
        {
            using var fixture = await ComplianceFixture.CreateAsync();

            var onboardingModule = fixture.OnboardingModule;

            var countryCodeEnum = Enum.Parse<CountryCode>("TZ", ignoreCase: true);

            var request = OnboardingJourneyRequest.Create(30, "USSD", countryCodeEnum,
                [
                    new()
                    {
                        PhoneNumber = "25406709876", IsMain = true
                    }
                ]);

            var result = await onboardingModule.ApplyAsync(request, default);

            Console.WriteLine(JsonConvert.SerializeObject(result.Rules));

            var rules = result.Rules;
            Assert.NotNull(result);
            Assert.NotNull(rules);
            Assert.False(result.AllRulesPassed);
            Assert.True(rules.Where(x => x.Name == nameof(CoolOff)).FirstOrDefault().Result == nameof(RuleResult.Succeed));
            Assert.NotNull(rules.Where(x => x.Name == nameof(CoolOff)).FirstOrDefault().ResultValue);
        }

        [Fact]
        public async Task WhenMaximumAgeIsGreaterThanConfigRwanda_ReturnsMaximumAgeFailed()
        {
            using var fixture = await ComplianceFixture.CreateAsync();

            var onboardingModule = fixture.OnboardingModule;

            var countryCodeEnum = Enum.Parse<CountryCode>("RW", ignoreCase: true);

            var request = OnboardingJourneyRequest.Create(70, "USSD", countryCodeEnum, new System.Collections.ObjectModel.Collection<Phone>{
                new() {
                    PhoneNumber = "25406709876", IsMain = true
                }
            });

            var result = await onboardingModule.ApplyAsync(request, default);

            Console.WriteLine(JsonConvert.SerializeObject(result.Rules));

            var rules = result.Rules;
            Assert.NotNull(result);
            Assert.NotNull(rules);
            Assert.False(result.AllRulesPassed);
            Assert.True(rules.Where(x => x.Name == nameof(MaximumAge)).FirstOrDefault().Result == nameof(RuleResult.Failed));
        }

        [Fact]
        public async Task WhenSimSwapIsNotApplicableRwanda_ReturnsSimswapCheckNotApplied()
        {
            using var fixture = await ComplianceFixture.CreateAsync();

            var onboardingModule = fixture.OnboardingModule;

            var countryCodeEnum = Enum.Parse<CountryCode>("SS", ignoreCase: true);

            var request = OnboardingJourneyRequest.Create(70, "USSD", countryCodeEnum, new System.Collections.ObjectModel.Collection<Phone>{
                new() {
                    PhoneNumber = "25406709876", IsMain = true
                }
            });

            var result = await onboardingModule.ApplyAsync(request, default);

            Console.WriteLine(JsonConvert.SerializeObject(result.Rules));

            var rules = result.Rules;
            Assert.NotNull(result);
            Assert.NotNull(rules);
            Assert.False(result.AllRulesPassed);
            Assert.True(rules.Where(x => x.Name == nameof(Simswap)).FirstOrDefault().Result == nameof(RuleResult.NotApplied));
        }

        #region WorldCheck API Test
        // [Fact]
        // public async Task WhenWorldCheckIsEnabled_ReturnsWorldCheckSuccessful()
        // {
        //     using var fixture = await ComplianceFixture.CreateAsync();

        //     var onboardingModule = fixture.OnboardingModule;

        //     var countryCodeEnum = Enum.Parse<CountryCode>("TZ", ignoreCase: true);

        //     var request = OnboardingJourneyRequest.Create(70, "USSD", countryCodeEnum, new System.Collections.ObjectModel.Collection<Phone>{
        //         new() {
        //             PhoneNumber = "25406709876", IsMain = true
        //         }
        //     }, customerName: WorldCheckAPIMock.VALID_NAME);

        //     var result = await onboardingModule.ApplyAsync(request, default);

        //     Console.WriteLine(JsonConvert.SerializeObject(result.Rules));

        //     var rules = result.Rules;
        //     Assert.NotNull(result);
        //     Assert.NotNull(rules);
        //     Assert.False(result.AllRulesPassed);
        //     Assert.True(rules.Where(x => x.Name == BaseWorldCheckRuleValidator.WORLD_CHECK).FirstOrDefault()?.Result == nameof(RuleResult.Succeed));
        // }

        // [Fact]
        // public async Task WhenWorldCheckIsDisabledKE_ReturnsWorldCheckNotApplied()
        // {
        //     using var fixture = await ComplianceFixture.CreateAsync();

        //     var onboardingModule = fixture.OnboardingModule;

        //     var countryCodeEnum = Enum.Parse<CountryCode>("KE", ignoreCase: true);

        //     var request = OnboardingJourneyRequest.Create(70, "USSD", countryCodeEnum, new System.Collections.ObjectModel.Collection<Phone>{
        //         new() {
        //             PhoneNumber = "25406709876", IsMain = true
        //         }
        //     });

        //     var result = await onboardingModule.ApplyAsync(request, default);

        //     Console.WriteLine(JsonConvert.SerializeObject(result.Rules));

        //     var rules = result.Rules;
        //     Assert.NotNull(result);
        //     Assert.NotNull(rules);
        //     Assert.False(result.AllRulesPassed);
        //     Assert.True(rules.Where(x => x.Name == BaseWorldCheckRuleValidator.WORLD_CHECK).FirstOrDefault().Result == nameof(RuleResult.NotApplied));
        //}
        #endregion
    }
}