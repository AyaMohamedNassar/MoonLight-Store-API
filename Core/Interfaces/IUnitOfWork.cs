using Core.Entities;
using Core.Entities.OrderAgregates;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity; 
        Task ExecuteTransactionAsync(Func<Task> action);
        Task<int> SaveAsync();
    }
}
