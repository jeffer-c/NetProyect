using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProyect.Domain.Entities;

namespace NetProyect.Domain.Mappings;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> b)
    {
        b.ToTable("Profile");

        b.HasKey(x => x.Id);

        b.Property(x => x.PersonName)
            .IsRequired()
            .HasMaxLength(128);

        b.Property(x => x.LastName)
            .HasMaxLength(128);

        b.Property(x => x.Gender)
            .HasMaxLength(24);

        b.Property(x => x.CountryOfCitizenship)
            .HasMaxLength(128);

        b.Property(x => x.Source)
            .HasMaxLength(128);

        b.Property(x => x.SquareImage)
            .HasMaxLength(512);

        // EF Core 8 soporta DateOnly; mapeamos a 'date' en SQL
        b.Property(x => x.BirthDate)
            .HasColumnType("date");

        // Valor por defecto
        b.Property(x => x.ImageExists)
            .HasDefaultValue(false);

        // Índice útil para búsquedas por nombre
        b.HasIndex(x => new { x.PersonName, x.LastName });
    }
}