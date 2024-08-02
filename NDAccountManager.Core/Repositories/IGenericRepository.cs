using System.Linq.Expressions;

namespace NDAccountManager.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T,bool>> expression);
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<bool> AnyAsync(Expression<Func<T,bool>> expression);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
