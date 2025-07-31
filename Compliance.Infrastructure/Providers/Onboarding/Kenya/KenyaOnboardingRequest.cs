using Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;
using Compliance.Application.UseCases.SimSwap.ProcessVerifySimSwap;
using Compliance.Application.UseCases.VerifyLastSwapDate;
using Compliance.Domain.Entity.SimSwapTasks;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using Compliance.Domain.Mappers;
using Compliance.Domain.Response;
using Newtonsoft.Json;
using Omni;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Compliance.Infrastructure.Providers.Onboarding.Kenya
{
    public class KenyaOnboardingJourneyRequest : IOnboardingJourneyRequest<OnboardingJourneyResponse>
    {
        public int Age { get; set; }
        public string Channel { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CallBackUrl { get; set; }
        public string Source { get; set; }
        public CountryCode CountryCode { get; set; }
        public Collection<Phone> PhoneNumbers { get; set; }
        public string Key => $"{Age}_{Channel}_{CountryCode}";
        public List<SubsidiaryRulesModel> SubsidiaryRules { get; set; }
        public OnboardingJourneyResponse Result { get; set; } = new();

    }


    public static class OnboardingExtension
    {
        public static List<SimSwapForm> CreateSimSwapRequest(this IOnboardingJourneyRequest request, int allowedSwappedDays)
        {
            List<SimSwapForm> simSwapForms = new();
            foreach (var item in request.PhoneNumbers)
                simSwapForms.Add(new SimSwapForm()
                {
                    PhoneNumber = item.PhoneNumber,
                    CountryCode = request.CountryCode.ToString(),
                    AllowedSwappedDays = allowedSwappedDays
                });

            return simSwapForms;
        }

        public static Result<SimSwapCheckTask> CreateSimSwapTaskEntity(this IOnboardingJourneyRequest request, int allowedSwappedDays)
        {
            var command = new ProcessVerifySimSwapCommand(
                allowedSwappedDays, 
                request.CountryCode.ToString(), 
                request.PhoneNumbers.Select(p => p.PhoneNumber).ToArray(), 
                request.CallBackUrl, request.CustomerId);

            var simSwapCheckTaskResult = SimSwapCheckTask.Create(
                 (int)SimSwapCheckTaskStatus.Pending,
                 nameof(SimSwapCheckTaskStatus.Pending),
                 typeof(ProcessVerifySimSwapCommand).FullName,
                 JsonConvert.SerializeObject(command),
                 request.CountryCode.ToString(),
                 request.CustomerId,
                 request.Source,
                 nameof(Operation.VerifySimSwap)
                );

            if (simSwapCheckTaskResult.HasError) 
            { 
                return simSwapCheckTaskResult.Error;
            }

            return simSwapCheckTaskResult.Value;
        }
    }
}
