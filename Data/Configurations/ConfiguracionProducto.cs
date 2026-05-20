using Microsoft.EntityFrameworkCore;
using TiendaApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TiendaApp.Data.Configurations;

public class ConfiguracionProducto : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("Productos");
        builder.HasKey(p => p.IdProducto);

        builder.Property(p => p.NombreProducto)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(c => c.DescripcionProducto)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.SKU)
            .IsRequired()
            .HasMaxLength(50);

        // SKU es único, no pueden existir dos productos con el mismo
        builder.HasIndex(p => p.SKU)
            .IsUnique();

        builder.Property(p => p.Precio)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.DescuentoPrecio)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Marca)
            .HasMaxLength(100);

        // Un producto tiene un solo stock
        builder.HasOne(p => p.Stock)
            .WithOne(s => s.Producto)
            .HasForeignKey<Stock>(s => s.IdProducto)
            .OnDelete(DeleteBehavior.Cascade);

        // Un producto tiene muchas imágenes
        builder.HasMany(p => p.Imagenes)
            .WithOne(i => i.Producto)
            .HasForeignKey(i => i.IdProducto)
            .OnDelete(DeleteBehavior.Cascade);
    }
}