using Microsoft.EntityFrameworkCore;
using TiendaApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TiendaApp.Data.Configurations;

public class ConfiguracionStock : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");

        builder.HasKey(s => s.IdStock);

        builder.Property(s => s.Cantidad)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(s => s.CantidadMinima)
            .IsRequired()
            .HasDefaultValue(5);
    }
}