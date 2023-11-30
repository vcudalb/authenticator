using System.Linq.Expressions;

namespace Authenticator.Domain.Repositories.Abstractions;

/// <summary>
/// Generic repository interface for performing CRUD operations on entities of type T.
/// </summary>
/// <typeparam name="TEntity">The type of entities in the repository.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Retrieves asynchronously entities based on a filter, with optional ordering and related properties.
    /// </summary>
    /// <param name="filter">A filter predicate to apply to the entities.</param>
    /// <param name="orderBy">A function to order the entities based on a queryable.</param>
    /// <param name="includeProperties">Comma-separated names of related properties to include in the result.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A collection of entities matching the filter criteria.</returns>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves asynchronously entities without tracking them, based on a filter, with optional ordering and related properties.
    /// </summary>
    /// <param name="filter">A filter predicate to apply to the entities.</param>
    /// <param name="orderBy">A function to order the entities based on a queryable.</param>
    /// <param name="includeProperties">Comma-separated names of related properties to include in the result.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A collection of entities matching the filter criteria without tracking.</returns>
    Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves asynchronously the first entity matching the filter criteria.
    /// </summary>
    /// <param name="filter">A filter predicate to apply to the entities.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The first entity matching the filter criteria, or null if no match is found.</returns>
    Task<TEntity?> FindAsync(object id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves asynchronously the first entity matching the filter criteria without tracking it.
    /// </summary>
    /// <param name="filter">A filter predicate to apply to the entities.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The first entity matching the filter criteria, or null if no match is found.</returns>
    Task<TEntity?> GetFirstAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Inserts asynchronously a new entity into the repository.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes asynchronously entities based on a filter criteria.
    /// </summary>
    /// <param name="filter">A filter predicate to apply to the entities for deletion.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes asynchronously entities based on a filter criteria.
    /// </summary>
    /// <param name="filter">A filter predicate to apply to the entities for deletion.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <param name="entityToDelete">The entity to delete.</param>
    void Delete(TEntity entityToDelete);
    
    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    Task UpdateAsync(TEntity entity);
}