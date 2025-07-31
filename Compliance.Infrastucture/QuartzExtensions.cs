using Quartz;
using Compliance.Domain.Configurations;
using Compliance.Infrastructure.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace Compliance.Infrastructure;

public static class QuartzExtensions
{
    public static IServiceCollection RegisterBackgroundSimSwapRunner(this IServiceCollection services, SimSwapCheckTaskJobSetting jobSetting)
    {
        if (!jobSetting.IsActive)
        {
            return services;
        }
        // Add Quartz services
        services.AddQuartz(q =>
        {
            Type SimSwapCheckJobType = typeof(SimSwapCheckJob);
            // Define a job and trigger
            var jobKey = JobKey.Create(jobSetting.JobKey);
            q.AddJob(SimSwapCheckJobType, jobKey)
            .AddTrigger(trigger => trigger.ForJob(jobKey)
            .WithSimpleSchedule(s => s.WithInterval(TimeSpan.FromMilliseconds(jobSetting.IntervalMilliSeconds))
            .RepeatForever()));
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
            options.AwaitApplicationStarted = true;
        });

        return services;
    }
}
