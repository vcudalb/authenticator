#nullable enable
using System.Linq.Expressions;
using Authenticator.Domain.Repositories.Abstractions;
using Authenticator.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Authenticator.Infrastructure.Persistence.Repositories;

    /// <inheritdoc />
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IDbContextFactory<AuthenticatorDbContext> _contextFactory;

        /// <summary>
        /// Constructs a new instance of the <see cref="GenericRepository{TEntity}"/>
        /// </summary>
        /// <param name="contextFactory"> an instance of <see cref="IDbContextFactory{TContext}"/> that defines a factory for creating <see cref="AuthenticatorDbContext" /> instances</param>
        public GenericRepository(IDbContextFactory<AuthenticatorDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "",
            CancellationToken cancellationToken = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter is not null) query = query.Where(filter);
            query = includeProperties
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy is not null
                ? await orderBy(query).ToListAsync(cancellationToken)
                : await query.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "",
            CancellationToken cancellationToken = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            IQueryable<TEntity> query = context.Set<TEntity>().AsNoTracking();
            if (filter is not null) query = query.Where(filter);
            query = includeProperties
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy is not null
                ? await orderBy(query).ToListAsync(cancellationToken)
                : await query.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TEntity?> FindAsync(object id, CancellationToken cancellationToken = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            return await context.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            return await context.Set<TEntity>().FirstOrDefaultAsync(filter, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TEntity?> GetFirstAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default) 
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            return await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
        }

        /// <inheritdoc />
        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            await context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            var entity = await context.Set<TEntity>().FirstAsync(filter, cancellationToken);
            context.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    
        /// <inheritdoc />
        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            context.RemoveRange(context.Set<TEntity>().Where(filter));
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public void Delete(TEntity entityToDelete)
        {
            using var context =  _contextFactory.CreateDbContext();
            context.Set<TEntity>().Remove(entityToDelete);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(TEntity entity)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }