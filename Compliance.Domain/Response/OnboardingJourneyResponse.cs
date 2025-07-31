
using System.Text.Json.Serialization;

namespace Compliance.Domain.Response;

public class OnboardingJourneyResponse
{
    public bool AllRulesPassed { get; set; }
    public List<Rule> Rules { get; set; } = new();
    public List<string> VerificationOptions { get; set; }
}

public class Rule
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Result { get; set; }
    public object ResultValue { get; set; }
    public string Message { get; set; }
    [JsonIgnore]
    public bool TerminateOnFailure { get; set; }
}

public static class OnboardingJourneyResponseExtensions
{
    public static OnboardingJourneyResponse PopulateRuleResult(this OnboardingJourneyResponse response, Rule rule)
    {
        response.Rules.Add(new Rule
        {
            Name = rule.Name,
            Code = rule.Code,
            Result = rule.Result,
            ResultValue = rule.ResultValue,
            TerminateOnFailure = rule.TerminateOnFailure,
            Message = rule.Message
        });

        return response;
    }

    public static bool ShouldTerminate(this OnboardingJourneyResponse response)
    {
        return response.Rules.Any(x => x.TerminateOnFailure);
    }

    public static bool CanRunRuleBasedDependencyRuleResult(this OnboardingJourneyResponse response, params string[] rules)
    {
        bool canRunRule = true;
        if(rules.Length <= 0) return canRunRule;
        
        foreach (var rule in rules)
        {
            var ruleResult = response.Rules.Where(x => x.Name == rule.ToString()).FirstOrDefault();

            if (ruleResult is null) { continue; }

            if (ruleResult.Result == RuleResult.Failed.ToString())
            {
                canRunRule = false;
                break;
            }
        }

        return canRunRule;
    }
}
