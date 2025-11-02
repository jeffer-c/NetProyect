using NetProyect.Application.Interfaces;

namespace NetProyect.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly NetProyectDbContext _ctx;
    public UnitOfWork(NetProyectDbContext ctx) => _ctx = ctx;
    public Task<int> SaveChangesAsync(CancellationToken ct) => _ctx.SaveChangesAsync(ct);
}