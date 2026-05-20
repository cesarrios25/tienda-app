using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaApp.Data;
using TiendaApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace TiendaApp.Controllers;
[Authorize(Roles = "Admin")]
public class ProductoController : Controller
{
    private readonly TiendaAppDbContext _context;

    public ProductoController(TiendaAppDbContext context)
    {
        _context = context;
    }

    // GET -> Productos
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var productos = await _context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Stock)
            .ToListAsync();

        return View(productos);
    }

    // GET -> Productos/Details/1
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Stock)
            .Include(p => p.Imagenes)
            .FirstOrDefaultAsync(p => p.IdProducto == id);

        if (producto == null) return NotFound();

        return View(producto);
    }

    // GET -> Productos/Create
    public async Task<IActionResult> Create()
    {
        await LoadCategoriesAsync();
        return View();
    }

    // POST -> Productos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Producto producto, int stockQuantity)
    {
        // Las propiedades de navegación no vienen del formulario, EF las llena solo
        ModelState.Remove("Stock");
        ModelState.Remove("Categoria");
        ModelState.Remove("Imagenes");

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return View(producto);
        }

        producto.CreadoEn = DateTime.UtcNow;
        producto.Stock = new Stock
        {
            Cantidad       = stockQuantity,
            CantidadMinima = 5,
            Actualizado    = DateTime.UtcNow
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET -> Productos/Edit/1
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.Stock)
            .FirstOrDefaultAsync(p => p.IdProducto == id);

        if (producto == null) return NotFound();

        await LoadCategoriesAsync(producto.IdCategoria);
        return View(producto);
    }

    // POST -> Productos/Edit/1
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Producto producto, int stockQuantity)
    {
        if (id != producto.IdProducto) return BadRequest();

        ModelState.Remove("Stock");
        ModelState.Remove("Categoria");
        ModelState.Remove("Imagenes");

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync(producto.IdCategoria);
            return View(producto);
        }

        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.IdProducto == id);
        if (stock != null)
        {
            stock.Cantidad    = stockQuantity;
            stock.Actualizado = DateTime.UtcNow;
        }

        _context.Productos.Update(producto);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET -> Productos/Delete/1
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Stock)
            .FirstOrDefaultAsync(p => p.IdProducto == id);

        if (producto == null) return NotFound();

        return View(producto);
    }

    // POST -> Productos/Delete/1
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Carga el dropdown de categorías
    private async Task LoadCategoriesAsync(int? selectedId = null)
    {
        var categorias = await _context.Categorias
            .Where(c => c.Activo)
            .ToListAsync();

        ViewBag.CategoryId = new SelectList(categorias, "IdCategoria", "NombreCategoria", selectedId);
    }
}