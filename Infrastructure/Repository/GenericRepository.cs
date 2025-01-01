using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Infrastructure.SpecEvaluator;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly StoreDbContext _context;
        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        async Task<TEntity> IGenericRepository<TEntity>.GetEntity(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        async Task<IReadOnlyList<TEntity>> IGenericRepository<TEntity>.ListAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
        public async Task<int> CountAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>
                .GetQuery(_context.Set<TEntity>().AsQueryable(),specification);
        }
        public async Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

    }
}
