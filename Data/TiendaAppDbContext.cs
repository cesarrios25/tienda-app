using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TiendaApp.Data.Configurations;
using TiendaApp.Models;

namespace TiendaApp.Data;
// Cambio clave: hereda de IdentityDbContext<ApplicationUser> en lugar de DbContext
public class TiendaAppDbContext : IdentityDbContext<ApplicationUser>
{
    public TiendaAppDbContext(DbContextOptions<TiendaAppDbContext> options) : base(options) {}
    
    // Cada DbSet representa una tabla en la base de datos
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<ImagenProducto> ImagenProductos { get; set; }
    public DbSet<Stock> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // CRÍTICO: llamar base primero para que Identity configure sus tablas
        base.OnModelCreating(modelBuilder);
        
        // Registra todas las configuraciones de la carpeta Configurations
        modelBuilder.ApplyConfiguration(new ConfiguracionCategoria());
        modelBuilder.ApplyConfiguration(new ConfiguracionProducto());
        modelBuilder.ApplyConfiguration(new ConfiguracionImagenProducto());
        modelBuilder.ApplyConfiguration(new ConfiguracionStock());
    }
}

