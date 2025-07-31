using Compliance.Shared;
using Compliance.Domain.Form;
using Compliance.Domain.Enum;
using Compliance.Domain.Response;
using Compliance.Application.Commands;
using System.Collections.ObjectModel;

namespace Compliance.Application.UseCases.Onboarding.VerifyUserDetails;

public record VerifyUserDetailsCommand(int Age, string Channel, CountryCode CountryCode, Collection<Phone> PhoneNumbers, string CustomerName) : CommandBase<Result<OnboardingJourneyResponse>>
{ }
