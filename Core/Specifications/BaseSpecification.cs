using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            Includes.Add(include);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpresion)
        {
            OrderBy = orderByExpresion;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpresion)
        {
            OrderByDescending = orderByDescendingExpresion;
        }

        protected void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
