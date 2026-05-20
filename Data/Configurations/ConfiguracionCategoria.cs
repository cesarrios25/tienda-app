using Microsoft.EntityFrameworkCore;
using TiendaApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TiendaApp.Data.Configurations;

public class ConfiguracionCategoria : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");
        builder.HasKey(c => c.IdCategoria);

        builder.Property(c => c.NombreCategoria)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.DescripcionCategoria)
            .HasMaxLength(500);

        // Una categoría tiene muchos productos.
        // Con el restrict no dja borrar tablas si tienen una relacion a otras.
        builder.HasMany(c => c.Productos)
            .WithOne(p => p.Categoria)
            .HasForeignKey(p => p.IdCategoria)
            .OnDelete(DeleteBehavior.Restrict);
    }
}