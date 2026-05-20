using Microsoft.AspNetCore.Identity;
using TiendaApp.Models;

namespace TiendaApp.Data;

public static class SeedData
{
    public static async Task InicializarAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Crear roles si no existen
        string[] roles = ["Admin", "Cliente"];
        foreach (var rol in roles)
        {
            if (!await roleManager.RoleExistsAsync(rol))
                await roleManager.CreateAsync(new IdentityRole(rol));
        }

        // Crear usuario Admin si no existe
        const string adminEmail = "admin@tiendaapp.com";
        const string adminPassword = "Admin123!";

        var adminExistente = await userManager.FindByEmailAsync(adminEmail);
        if (adminExistente == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                NombreCompleto = "Administrador",
                EmailConfirmed = true
            };

            var resultado = await userManager.CreateAsync(admin, adminPassword);
            if (resultado.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}