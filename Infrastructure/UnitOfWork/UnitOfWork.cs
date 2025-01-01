using Core.Entities;
using Core.Entities.OrderAgregates;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Collections;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StoreDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IConnectionMultiplexer redis;

        public UnitOfWork(StoreDbContext storeDbContext, ILogger<UnitOfWork> logger, IConnectionMultiplexer redis)
        {
            _context = storeDbContext;
            _logger = logger;
            this.redis = redis;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            _repositories ??= [];

            string type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositorytype = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositorytype
                    .MakeGenericType(typeof(TEntity)),_context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }

        public async Task ExecuteTransactionAsync(Func<Task> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "An Error Ocuur During Transaction");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
