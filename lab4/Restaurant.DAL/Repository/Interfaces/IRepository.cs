using System.Linq.Expressions;

namespace Restaurant.DAL.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> FindByIdAsync(int id);

        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task SaveAsync();
    }
}
