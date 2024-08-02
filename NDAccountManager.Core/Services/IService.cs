using System.Linq.Expressions;

namespace NDAccountManager.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
