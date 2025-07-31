using Omni.Factory;
using Compliance.Domain.Enum;

namespace Compliance.Infrastructure.Providers.Onboarding;

internal record OnboardingSelector(CountryCode CountryCode) : BaseSelector;
