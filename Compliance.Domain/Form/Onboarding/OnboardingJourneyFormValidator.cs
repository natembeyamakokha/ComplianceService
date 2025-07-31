using FluentValidation;
using Compliance.Domain.Enum;

namespace Compliance.Domain.Form;

public class OnboardingJourneyFormValidator : AbstractValidator<OnboardingJourneyForm>
{
    public OnboardingJourneyFormValidator()
    {
        RuleFor(x => x.CountryCode)
        .NotEmpty()
        .WithMessage("country code is required")
        .Custom((instance, context) =>
        {
            var valid = System.Enum.TryParse<CountryCode>(instance, ignoreCase: true, out CountryCode result);
            if (!valid)
                context.AddFailure("country code not allowed");
        });

        RuleFor(x => x.Age)
        .NotEmpty()
        .WithMessage("age is required")
        .Custom((instance, context) =>
        {
            if (instance <= 0)
                context.AddFailure("age should be greater than zero");
        });

        RuleFor(x => x.Channel)
        .NotEmpty()
        .WithMessage("channel is required");

        RuleFor(x => x.PhoneNumbers)
        .NotEmpty()
        .WithMessage("phone number is required")
        .Custom((instance, context) =>
        {
            if (!instance.Any())
                context.AddFailure("phone number is required");
        });
    }
}