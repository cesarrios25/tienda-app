using System.ComponentModel.DataAnnotations;

namespace TiendaApp.Models;

public class ImagenProducto
{
    [Key]
    public int IdImagenProducto { get; set; }
    [Required]
    public string? Url { get; set; } = string.Empty;

    public bool Principal { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Llave foránea → pertenece a un producto
    public int IdProducto { get; set; }
    
    //Navegacion
    public Producto Producto { get; set; } = null!;
}