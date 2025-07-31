using MediatR;
using Compliance.Application.Commands;

namespace Compliance.Application.Contracts.Handlers;

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}
