using MediatR;

namespace Compliance.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }

    public abstract record QueryBase<TResult> : IQuery<TResult> { }
}
