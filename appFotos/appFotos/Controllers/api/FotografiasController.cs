using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appFotos.Data;
using appFotos.Models;

namespace appFotos.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotografiasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FotografiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fotografias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fotografias>>> GetFotografias()
        {
            return await _context.Fotografias.ToListAsync();
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
