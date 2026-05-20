using Microsoft.EntityFrameworkCore;
using TiendaApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TiendaApp.Data.Configurations;

public class ConfiguracionImagenProducto : IEntityTypeConfiguration<ImagenProducto>
{
    public void Configure(EntityTypeBuilder<ImagenProducto> builder)
    {
        builder.ToTable("ImagenProducto");

        builder.HasKey(i => i.IdImagenProducto);

        builder.Property(i => i.Url)
            .IsRequired()
            .HasMaxLength(500);
    }
}