using System.ComponentModel.DataAnnotations;

namespace TiendaApp.Models;

public class Categoria
{
    [Key]
    public int IdCategoria { get; set; }
    [Required]
    public string? NombreCategoria { get; set; } = string.Empty;
    [Required]
    public string? DescripcionCategoria { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}