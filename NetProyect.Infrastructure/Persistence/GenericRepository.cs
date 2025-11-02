using Microsoft.EntityFrameworkCore;
using NetProyect.Application.Interfaces;

namespace NetProyect.Infrastructure.Persistence;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly NetProyectDbContext _ctx;
    public GenericRepository(NetProyectDbContext ctx) => _ctx = ctx;

    public Task<T?> GetByIdAsync(object id, CancellationToken ct)
        => _ctx.Set<T>().FindAsync([id], ct).AsTask();

    public Task AddAsync(T entity, CancellationToken ct)
        => _ctx.Set<T>().AddAsync(entity, ct).AsTask();

    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct)
        => _ctx.Set<T>().AddRangeAsync(entities, ct);

    public void Update(T entity) => _ctx.Set<T>().Update(entity);
    public void Remove(T entity) => _ctx.Set<T>().Remove(entity);

    public IQueryable<T> Query() => _ctx.Set<T>().AsQueryable();
}