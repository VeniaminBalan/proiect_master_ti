using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Entities;
using MVC.ValueObjects;

namespace MVC.Data.EntityConfigurations;

public class TaxeConfigurations : IEntityTypeConfiguration<Taxe>
{
    
    
    public void Configure(EntityTypeBuilder<Taxe> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Impozit)
            .HasConversion(new PercentageConverter())
            .IsRequired();
        
        builder.Property(b => b.CAS)
            .HasConversion(new PercentageConverter())
            .IsRequired();
        
        builder.Property(b => b.CASS)
            .HasConversion(new PercentageConverter())
            .IsRequired();

        builder.HasData(
            [
                new Taxe(new Percentage(25), new Percentage(10), new Percentage(10), 1)
            ]);
    }
}