using MediatR;
using Compliance.Application.Queries;

namespace Compliance.Application.Contracts.Handlers
{
    public interface IQueryHandler<in TQuery, TResult> :
       IRequestHandler<TQuery, TResult>
       where TQuery : IQuery<TResult>
    {
    }
}
