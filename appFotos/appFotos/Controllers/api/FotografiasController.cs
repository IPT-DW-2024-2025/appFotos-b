using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appFotos.Data;
using appFotos.Models;
using appFotos.Models.ApiModels;

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
        public ActionResult GetFotografias()
        {
            if (User.Identity.IsAuthenticated)
            {
                var resultAut = _context.Fotografias.ToList();
                return Ok(resultAut);
            }
            
            var result = _context.Fotografias
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
    }
}
