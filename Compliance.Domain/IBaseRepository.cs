using System.Linq.Expressions;
using Compliance.Shared.Domains.Helper;

namespace Compliance.Domain
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql);
        Task<PaginatedList<TEntity>> GetAllAsync<T>(int pageIndex, int pageSize, Expression<Func<TEntity, T>> keySelector, OrderBy orderBy = OrderBy.Ascending);
    }
}