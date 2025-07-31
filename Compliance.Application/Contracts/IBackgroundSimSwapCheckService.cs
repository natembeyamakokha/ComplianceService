namespace Compliance.Application.Contracts;

public interface IBackgroundSimSwapCheckService
{
    Task ExecuteOperationAsync(CancellationToken cancellationToken);
}
