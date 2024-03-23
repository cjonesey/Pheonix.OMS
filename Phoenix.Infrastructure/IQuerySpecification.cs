using System.Linq.Expressions;

namespace Phoenix.Infrastructure
{
    public interface IQuerySpecification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>>? FilterClause { get; }
        Expression<Func<TEntity, object?>>? IncludeClause { get; }
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderByClause { get; }
        int? PageClause { get; }
        int? PageSizeClause { get; }

        void Filter(Expression<Func<TEntity, bool>> filter);
        void Include(Expression<Func<TEntity, object?>>? include);
        void OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        void Page(int page);
        void PageSize(int pageSize);
    }
}