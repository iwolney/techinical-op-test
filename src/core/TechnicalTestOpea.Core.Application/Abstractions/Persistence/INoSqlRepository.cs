using System.Linq.Expressions;
using TechnicalTestOpea.Core.Domain.Models;

namespace TechnicalTestOpea.Core.Application.Abstractions.Persistence
{
    public interface INoSqlRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<Guid> CreateAsync(T entitty);
        Task UpdateAsync(T entitty);
        Task RemoveAsync(Guid id);
        Task<PagedResponse<TProjected>> GetPagedAsync<TProjected>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjected>> projection, PagedRequest request) where TProjected : class;
    }
}
