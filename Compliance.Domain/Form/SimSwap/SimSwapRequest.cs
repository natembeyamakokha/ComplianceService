using Omni;
using Omni.Features;
using Compliance.Shared;
using FluentValidation;
using Compliance.Domain.Enum;
using System.Text.Json.Serialization;

namespace Compliance.Domain.Form
{
    public class SimSwapRequest : IEntity
    {
        public string Key => RequestId;
        public long Id { get; }
        public string RequestId { get; set; }
        public string PhoneNumber { get; set; }
        public CountryCode CountryCode { get; set; }
        public int AllowedSwappedDays { get; set; } = 180;
        public IFeatureRequest Request { get; set; }

        public IEntity Clone()
        {
            return new SimSwapRequest()
            {
                CountryCode = CountryCode,
                Request = Request,
                PhoneNumber = PhoneNumber,
                RequestId = RequestId
            };
        }

        public static SimSwapRequest Create(string phoneNumber, CountryCode countryCode, int allowedSwappedDays)
        {
            return new()
            {
                PhoneNumber = phoneNumber,
                CountryCode = countryCode,
                AllowedSwappedDays = allowedSwappedDays
            };
        }
    }

    public class SimSwapForm
    {
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public int AllowedSwappedDays { get; set; } = 180;

        [JsonIgnore]
        public CountryCode CountryCodeEnum
        {
            get
            {
                return System.Enum.Parse<CountryCode>(CountryCode, ignoreCase: true);
            }
        }

        public void Validate()
        {
            var validator = new SimSwapFormValidator();
            var result = validator.Validate(this);
            if (!result.IsValid) throw new SimSwapException(result.Errors);
        }
    }

    public class SimSwapFormValidator : AbstractValidator<SimSwapForm>
    {
        public SimSwapFormValidator()
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

            RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("phone number is required");

            RuleFor(x => x.AllowedSwappedDays)
            .Custom((instance, context) =>
            {
                if (instance <= 0)
                    instance = 180;
            });
        }
    }
}