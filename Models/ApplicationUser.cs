using Microsoft.AspNetCore.Identity;

namespace TiendaApp.Models;

public class ApplicationUser : IdentityUser
{
    public string NombreCompleto { get; set; } = string.Empty;
}