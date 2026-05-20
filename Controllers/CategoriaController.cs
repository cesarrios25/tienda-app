using Microsoft.AspNetCore.Mvc;
using TiendaApp.Data;
using TiendaApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TiendaApp.Controllers;
[Authorize(Roles = "Admin")]  // --> solo accede el administrador, los usuarios no tienen ingreso a est parte
public class CategoriaController : Controller
{
    private readonly TiendaAppDbContext _context;
    
    // El DbContext se inyecta automáticamente — esto es inyección de dependencias
    public CategoriaController(TiendaAppDbContext context)
    {
        _context = context;
    }
    
    // GET -> Categorias.
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var categorias = await _context.Categorias.ToListAsync();
        return View(categorias);
    }
    
    // GET -> Categorias por ID.
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.IdCategoria == id);

        if (categoria == null) return NotFound();

        return View(categoria);
    }
    
    // GET form-> Categorias -> Create.
    public IActionResult Create()
    {
        return View();
    }
    
    // POST -> Categorias -> Create.
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Categoria categoria)
    {
        if (!ModelState.IsValid) return View(categoria);

        categoria.CreadoEn = DateTime.UtcNow;
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    // GET -> Categorias -> Edit por ID.
    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();

        return View(categoria);
    }
    
    // POST -> Categorias -> Edit por ID.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Categoria categoria)
    {
        if (id != categoria.IdCategoria) return BadRequest();
        if (!ModelState.IsValid) return View(categoria);

        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    // GET -> Categorias -> Delete por ID.
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();

        return View(categoria);
    }

    // POST -> Categorias -> Delete por ID.
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}



