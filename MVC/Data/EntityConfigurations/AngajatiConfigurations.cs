using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Entities;
using MVC.ValueObjects;

namespace MVC.Data.EntityConfigurations;

public class AngajatiConfigurations : IEntityTypeConfiguration<Angajat>
{
    
    
    public void Configure(EntityTypeBuilder<Angajat> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.SporProcentual)
            .HasConversion(new PercentageConverter())
            .IsRequired();
    }
}