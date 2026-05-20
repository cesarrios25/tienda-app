using System.ComponentModel.DataAnnotations;

namespace TiendaApp.Models;

public class Producto
{
    [Key]
    public int IdProducto { get; set; }
    [Required]
    public string? NombreProducto { get; set; } = string.Empty;
    public string? DescripcionProducto { get; set; } = string.Empty;
    [Required]
    public string? SKU { get; set; } = string.Empty;
    [Required]
    public decimal Precio { get; set; }
    public decimal? DescuentoPrecio { get; set; }
    public string? Marca { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
    
    // Llave foránea → pertenece a una categoría
    public int IdCategoria { get; set; }
    
    // navegacion
    public Categoria Categoria { get; set; } = null!;
    public Stock Stock { get; set; } = null!;
    public ICollection<ImagenProducto> Imagenes { get; set; } = new List<ImagenProducto>();
}