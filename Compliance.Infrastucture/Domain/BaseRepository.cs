using Dapper;
using Autofac;
using System.Text;
using Compliance.Domain;
using System.Linq.Expressions;
using Compliance.Shared.DataAccess;
using Compliance.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using Compliance.Shared.Domains.Helper;

namespace Compliance.Infrastructure.Domains
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly UtilityServiceContext _context;
        private DbSet<TEntity> _dbSet;

        private Dictionary<string, object> sqlParameters;

        public BaseRepository(UtilityServiceContext context)
        {
            _context = context;
        }
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = _context.Set<TEntity>();
                }

                return _dbSet;
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
        {
            using (var scope = ServiceCompositionRoot.BeginLifetimeScope())
            {
                var sqlConnectionFactory = scope.Resolve<ISqlConnectionFactory>();
                using (var connection = sqlConnectionFactory.GetOpenConnection())
                {
                    var result = await connection.QueryAsync<T>(sql, sqlParameters);
                    sqlParameters = null;
                    return result;
                }

            }
        }

        protected void AddSqlParameters(string key, object value)
        {
            if (sqlParameters == null)
                sqlParameters = new Dictionary<string, object>();

            sqlParameters.Add(key, value);
        }


        private async Task<PaginatedList<TEntity>> GetAllAsync<T>(int pageIndex, int pageSize, Expression<Func<TEntity, T>> keySelector,
            Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = FilterQuery(keySelector, predicate, orderBy, includeProperties);
            var total = await entities.CountAsync();// entities.CountAsync() is different than pageSize
            entities = entities.Paginate(pageIndex, pageSize);
            var list = await entities.ToListAsync();
            return list.ToPaginatedList(pageIndex, pageSize, total);
        }

        public async Task<PaginatedList<TEntity>> GetAllAsync<T>(int pageIndex, int pageSize, Expression<Func<TEntity, T>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return await GetAllAsync(pageIndex, pageSize, keySelector, null, orderBy);
        }


        #region Helpers

        private IQueryable<TEntity> FilterQuery<T>(Expression<Func<TEntity, T>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy,
               Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            entities = (predicate != null) ? entities.Where(predicate) : entities;
            entities = (orderBy == OrderBy.Ascending)
                ? entities.OrderBy(keySelector)
                : entities.OrderByDescending(keySelector);
            return entities;
        }

        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = Entities;
            //Todo: a better way to write this
            //http://appetere.com/post/Passing-Include-statements-into-a-Repository
            //not tested before will do that later looks shorter
            //entities  = includeProperties.Aggregate(entities, (current, include) => current.Include(include));

            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }

        private IQueryable<TEntity> IncludeStringProperties(string[] includeProperties)
        {
            IQueryable<TEntity> entities = null;
            foreach (var includeProperty in includeProperties)
            {
                if (entities == null)
                    entities = Entities.Include(includeProperty);
                else
                    entities = entities.Include(includeProperty);
            }
            return entities;
        }
        #endregion

        protected string AddSort(string sql, string direction, string orderBy)
        {
            sql += " ORDER BY @orderBy @direction";
            var sqlQuery = new StringBuilder(sql);
            sqlQuery.Replace("@direction", direction);
            sqlQuery.Replace("@orderBy", orderBy);
            return sqlQuery.ToString();
        }

        protected string ToPaged(int pageIndex = 1, int pageSize = 10)
        {
            int skip = pageSize * (pageIndex - 1);
            AddSqlParameters("@Skip", skip);
            AddSqlParameters("@PageSize", pageSize);
            return " OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY";
        }
    }
}