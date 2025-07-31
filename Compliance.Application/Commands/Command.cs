using System;
using MediatR;

namespace Compliance.Application.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }

    public interface ICommand : IRequest<Unit>
    {
        Guid Id { get; }
    }
}
