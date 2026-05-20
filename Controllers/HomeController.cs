using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaApp.Data;

namespace TiendaApp.Controllers;

public class HomeController : Controller
{
    private readonly TiendaAppDbContext _context;

    public HomeController(TiendaAppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.StockTotal = await _context.Stocks.SumAsync(s => (int?)s.Cantidad) ?? 0;
        ViewBag.CategoriasActivas = await _context.Categorias.CountAsync(c => c.Activo);
        ViewBag.ProductosRegistrados = await _context.Productos.CountAsync();

        return View();
    }
}