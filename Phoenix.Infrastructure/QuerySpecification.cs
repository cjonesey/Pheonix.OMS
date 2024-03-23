using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure
{
    public class QuerySpecification<TEntity> : IQuerySpecification<TEntity> where TEntity : class
    {

        public Expression<Func<TEntity, bool>>? FilterClause { get; private set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderByClause { get; private set; }
        public int? PageSizeClause { get; private set; }
        public int? PageClause { get; private set; }
        public Expression<Func<TEntity, object?>>? IncludeClause { get; private set; }

        public virtual void Filter(Expression<Func<TEntity, bool>> filter)
        {
            FilterClause = filter;
        }

        public virtual void OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            OrderByClause = orderBy;
        }
        public void Include(Expression<Func<TEntity, object?>>? include)
        {
            IncludeClause = include;
        }
        public virtual void PageSize(int pageSize)
        {
            PageSizeClause = pageSize;
        }
        public virtual void Page(int page)
        {
            PageClause = page;
        }
    }
}
