using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TiendaApp.Data;
using TiendaApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Selecciona la connection string según el entorno
var connectionString = builder.Configuration.GetConnectionString(
    builder.Environment.IsEnvironment("Docker") ? "DockerConnection" : "DefaultConnection"
);

// Registra el DbContext con MySQL
builder.Services.AddDbContext<TiendaAppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

// Después de builder.Services.AddDbContext<...>

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Reglas de contraseña (ajustables)
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;

        // Lockout
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;

        // Usuario
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<TiendaAppDbContext>()
    .AddDefaultTokenProviders();

// Cookie de autenticación
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

var app = builder.Build();

// Aplica migraciones automáticamente al iniciar (necesario en Docker)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TiendaAppDbContext>();
    db.Database.Migrate();
    await SeedData.InicializarAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Categoria}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();