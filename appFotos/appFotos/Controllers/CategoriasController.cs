using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appFotos.Data;
using appFotos.Models;
using Microsoft.AspNetCore.Authorization;

namespace appFotos.Controllers
{
    /// <summary>
    /// Controller responsável pelas categorias
    /// </summary>
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
            if (categorias == null)
            {
                return NotFound();
            }

            return View(categorias);
        }

        // GET: Categorias/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Categoria")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categorias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categorias);
        }

        // GET: Categorias/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias.FindAsync(id);
            
            // guardamos em sessão o id da categoria que o utilizador quer editar
            // se ele fizer um post para um Id diferente, ele está a tentar alterar algo que não devia
            HttpContext.Session.SetInt32("categoriaId", categorias.Id);
            
            if (categorias == null)
            {
                return NotFound();
            }
            return View(categorias);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit([FromRoute]int id, [Bind("Id,Categoria")] Categorias categorias)
        {
            // adicionámos o [FromRoute] para ele ir buscar o id que vem na Rota
            // ex: Edit/5 -> vai buscar o id=5
            // sem o [FromRoute] ele vai buscar o Id que vem no Body
            if (id != categorias.Id)
            {
                return NotFound();
            }

            var categoriaDaSessao = HttpContext.Session.GetInt32("categoriaId");

            if (categoriaDaSessao != id)
            {
                ModelState.AddModelError("Id", "Tentaste aldrabar isto palhaço!");
                return View(categorias);
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorias);
                    await _context.SaveChangesAsync();
                    
                    HttpContext.Session.SetInt32("categoriaId", 0);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasExists(categorias.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            // se chegarmos aqui é porque algo correu mal
            return View(categorias);
        }

        // GET: Categorias/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorias == null)
            {
                return NotFound();
            }

            return View(categorias);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorias = await _context.Categorias.FindAsync(id);
            if (categorias != null)
            {
                _context.Categorias.Remove(categorias);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
