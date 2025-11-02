using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProyect.Domain.Entities;

namespace NetProyect.Domain.Mappings;

public class ForbesListConfiguration : IEntityTypeConfiguration<ForbesList>
{
    public void Configure(EntityTypeBuilder<ForbesList> b) 
    {
        b.ToTable("ForbesList");
        b.HasKey(x => x.Id);
        b.Property(x => x.Uri).IsRequired().HasMaxLength(256);
        b.HasIndex(x => x.Uri).IsUnique(false);
        b.HasOne(x => x.Industry)
         .WithMany(i => i.Forbes)
         .HasForeignKey(x => x.IndustryId)
         .OnDelete(DeleteBehavior.Restrict);
        b.HasOne(x => x.Profile)
         .WithMany()
         .HasForeignKey(x => x.ProfileId)
         .OnDelete(DeleteBehavior.Cascade);
        b.HasOne(x => x.NetWorth)
         .WithMany()
         .HasForeignKey(x => x.NetWorthId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}