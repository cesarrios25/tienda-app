using System.ComponentModel.DataAnnotations;

namespace TiendaApp.Models;

public class Stock
{
    [Key]
    public int IdStock { get; set; }
    [Required] public int Cantidad { get; set; } = 0;
    [Required] public int CantidadMinima { get; set; } = 5;
    public DateTime Actualizado { get; set; } = DateTime.UtcNow;
    
    // Llave foránea → pertenece a un producto
    public int IdProducto { get; set; }
    
    // Navegacion 
    public Producto Producto { get; set; } = null!;
}