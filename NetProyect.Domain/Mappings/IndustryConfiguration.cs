using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProyect.Domain.Entities;

namespace NetProyect.Domain.Mappings;

public class IndustryConfiguration : IEntityTypeConfiguration<Industry>
{
    public void Configure(EntityTypeBuilder<Industry> b)
    {
        b.ToTable("Industry");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

        b.HasIndex(x => x.Name)
            .IsUnique();

        // Relación invertida (coherente con ForbesListConfiguration)
        b.HasMany(i => i.Forbes)
         .WithOne(fl => fl.Industry)
         .HasForeignKey(fl => fl.IndustryId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}