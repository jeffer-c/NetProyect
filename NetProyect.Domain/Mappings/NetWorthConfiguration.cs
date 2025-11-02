using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProyect.Domain.Entities;

namespace NetProyect.Domain.Mappings;

public class NetWorthConfiguration : IEntityTypeConfiguration<NetWorth>
{
    public void Configure(EntityTypeBuilder<NetWorth> b)
    {
        b.ToTable("NetWorth");

        b.HasKey(x => x.Id);

        // Precisión decimal típica para importes
        b.Property(x => x.Original)
            .HasPrecision(18, 2);

        b.Property(x => x.Number)
            .HasPrecision(18, 2);

        b.Property(x => x.EstWorthPrev)
            .HasPrecision(18, 2);

        b.Property(x => x.FinalWorth)
            .HasPrecision(18, 2);

        b.Property(x => x.Currency)
            .HasMaxLength(16);

        b.Property(x => x.Formatted)
            .HasMaxLength(64);

        // Índice para ordenar/filtrar por patrimonio
        b.HasIndex(x => x.FinalWorth);
    }
}