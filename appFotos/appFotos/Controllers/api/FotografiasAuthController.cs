using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appFotos.Data;
using appFotos.Models;
using appFotos.Models.ApiModels;
using Microsoft.AspNetCore.Authorization;

namespace appFotos.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FotografiasAuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FotografiasAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fotografias
        [HttpGet]
        public ActionResult GetFotografias()
        {
            var result = _context.Fotografias
                .Where(f => f.Dono.IdentityUserName == User.Identity.Name)
                .Select(f => new FotosAutDto{Titulo = f.Titulo, Descricao = f.Descricao, 
                    Ficheiro = f.Ficheiro})
                .ToList();
            
            return Ok(result);
        }

        // GET: api/Fotografias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fotografias>> GetFotografias(int id)
        {
            var fotografias = await _context.Fotografias.FindAsync(id);

            if (fotografias == null)
            {
                return NotFound();
            }

            return fotografias;
        }

        // PUT: api/Fotografias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFotografias(int id, Fotografias fotografias)
        {
            if (id != fotografias.Id)
            {
                return BadRequest();
            }
            
            var fotoAux = _context.Fotografias
                .Include(f => f.Dono)
                .AsNoTracking()
                .First(f => f.Id==id);
            
            if (fotoAux.Dono.IdentityUserName != User.Identity.Name)
            {
                return Unauthorized();
            }

            _context.Entry(fotografias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotografiasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Fotografias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fotografias>> PostFotografias(Fotografias fotografias)
        {
            _context.Fotografias.Add(fotografias);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFotografias", new { id = fotografias.Id }, fotografias);
        }

        // DELETE: api/Fotografias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFotografias(int id)
        {
            var fotografias = await _context.Fotografias.FindAsync(id);
            if (fotografias == null)
            {
                return NotFound();
            }

            _context.Fotografias.Remove(fotografias);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FotografiasExists(int id)
        {
            return _context.Fotografias.Any(e => e.Id == id);
        }
    }
}
