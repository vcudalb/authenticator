using System.Linq.Expressions;
using Authenticator.Domain.Common;
using Authenticator.Domain.Repositories.Abstractions;
using Authenticator.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Authenticator.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AuthenticatorDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public GenericRepository(AuthenticatorDbContext context)
    {
        this.Context = context;
        this.DbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet;
        if (filter is not null) query = query.Where(filter);
        query = includeProperties
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return orderBy is not null
            ? await orderBy(query).ToListAsync(cancellationToken)
            : await query.ToListAsync(cancellationToken);
    }

    public async ValueTask<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default) =>
        await DbSet.FindAsync(id, cancellationToken);

    public async ValueTask InsertAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await DbSet.AddAsync(entity, cancellationToken);

    public async ValueTask DeleteAsync(object id, CancellationToken cancellationToken = default) =>
        Delete(await DbSet.FindAsync(id, cancellationToken));

    public void Delete(TEntity entityToDelete)
    {
        if (Context.Entry(entityToDelete).State == EntityState.Detached) DbSet.Attach(entityToDelete);
        DbSet.Remove(entityToDelete);
    }

    public void Update(TEntity entityToUpdate)
    {
        DbSet.Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await Context.SaveChangesAsync(cancellationToken);
}