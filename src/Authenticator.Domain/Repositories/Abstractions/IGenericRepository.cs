using System.Linq.Expressions;
using Authenticator.Domain.Common;

namespace Authenticator.Domain.Repositories.Abstractions;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>,
        IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default);

    ValueTask<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    ValueTask InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(object id, CancellationToken cancellationToken = default);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}