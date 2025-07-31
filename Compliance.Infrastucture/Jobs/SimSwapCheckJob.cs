using Quartz;
using Autofac;
using Compliance.Application.Contracts;

namespace Compliance.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class SimSwapCheckJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = ServiceCompositionRoot.BeginLifetimeScope();
    
        var service = scope.Resolve<IBackgroundSimSwapCheckService>();

        var cancellationToken = context.CancellationToken;

        await service.ExecuteOperationAsync(cancellationToken);
    }
}
