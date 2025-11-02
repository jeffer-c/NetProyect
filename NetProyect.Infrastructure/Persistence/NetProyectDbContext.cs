using Microsoft.EntityFrameworkCore;
using NetProyect.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetProyect.Infrastructure.Persistence;

public class NetProyectDbContext : DbContext
{
    public NetProyectDbContext(DbContextOptions<NetProyectDbContext> options) : base(options) { }

    public DbSet<ForbesList> ForbesLists => Set<ForbesList>();
    public DbSet<Industry> Industries => Set<Industry>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<NetWorth> NetWorths => Set<NetWorth>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Importa TODAS las configuraciones desde el assembly Domain (relaciones en Domain)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetProyect.Domain.Mappings.ForbesListConfiguration).Assembly);
    }
}