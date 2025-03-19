using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appFotos.Data;
using appFotos.Models;


namespace appFotos.Controllers
{
    public class FotografiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FotografiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fotografias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fotografias.Include(f => f.Categoria).Include(f => f.Dono);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Fotografias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografias = await _context.Fotografias
                .Include(f => f.Categoria)
                .Include(f => f.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotografias == null)
            {
                return NotFound();
            }

            return View(fotografias);
        }

        // GET: Fotografias/Create
        public IActionResult Create()
        {
            ViewData["CategoriaFk"] = new SelectList(_context.Categorias, "Id", "Categoria");
            ViewData["DonoFk"] = new SelectList(_context.Utilizadores, "Id", "Nome");
            return View();
        }

        // POST: Fotografias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,DataFotografia,Preco,DonoFk,CategoriaFk")] Fotografias fotografias, IFormFile ficheiroFotografia)
        {
            /*
             * 1- validar se o ficheiro vem a null
             *  se vier preparamos um erro
             *
             * 2- validar se o ficheiro é uma foto válida
             *  se for inválido enviamos um erro
             * 
             * 3- se chegar aqui podemos guardar o ficheiro no disco, e a referência na BD
             */
            
            ModelState.Remove("Categoria");
            ModelState.Remove("Dono");
            ModelState.Remove("Ficheiro");

            bool haImagem = false;
            var nomeImagem = "";

            // se não recebermos nenhum ficheiro, enviamos ao user um erro a dizer que tem de submeter um
            if (ficheiroFotografia == null)
            {
                ModelState.AddModelError("", "Tem de submeter um ficheiro");
            }
            // se entrar no else foi submetido um ficheiro
            else
            {
                // há ficheiro, mas é uma imagem?
                if (!(ficheiroFotografia.ContentType == "image/png" ||
                      ficheiroFotografia.ContentType == "image/jpeg"
                    )) {
                    // não
                    // vamos usar uma imagem pre-definida
                    fotografias.Ficheiro = "example.png";
                }
                // se chegar aqui é um ficheiro & uma imagem
                else
                {
                    haImagem = true;
                    // gerar nome imagem
                    Guid g = Guid.NewGuid();
                    // atrás do nome adicionamos a pasta onde a escrevemos
                    nomeImagem = "imagens/"+g.ToString();
                    string extensaoImagem =Path.GetExtension(ficheiroFotografia.FileName).ToLowerInvariant();
                    nomeImagem += extensaoImagem;
                    // guardar o nome do ficheiro na BD
                    fotografias.Ficheiro = nomeImagem;
                }
            }
            
            if (ModelState.IsValid)
            {
                // se existe uma imagem para escrever no disco
                if (haImagem)
                {
                    // vai construir o path para escrever a imagem
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", nomeImagem);
                    // escreve a imagem
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ficheiroFotografia.CopyToAsync(fileStream);
                    }
                }
                
                _context.Add(fotografias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaFk"] = new SelectList(_context.Categorias, "Id", "Categoria", fotografias.CategoriaFk);
            ViewData["DonoFk"] = new SelectList(_context.Utilizadores, "Id", "Nome", fotografias.DonoFk);
            return View(fotografias);
        }

        // GET: Fotografias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografias = await _context.Fotografias.FindAsync(id);
            if (fotografias == null)
            {
                return NotFound();
            }
            ViewData["CategoriaFk"] = new SelectList(_context.Categorias, "Id", "Categoria", fotografias.CategoriaFk);
            ViewData["DonoFk"] = new SelectList(_context.Utilizadores, "Id", "Nome", fotografias.DonoFk);
            return View(fotografias);
        }

        // POST: Fotografias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Ficheiro,DataFotografia,Preco,DonoFk,CategoriaFk")] Fotografias fotografias)
        {
            if (id != fotografias.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotografias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotografiasExists(fotografias.Id))
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
            ViewData["CategoriaFk"] = new SelectList(_context.Categorias, "Id", "Categoria", fotografias.CategoriaFk);
            ViewData["DonoFk"] = new SelectList(_context.Utilizadores, "Id", "CodPostal", fotografias.DonoFk);
            return View(fotografias);
        }

        // GET: Fotografias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografias = await _context.Fotografias
                .Include(f => f.Categoria)
                .Include(f => f.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotografias == null)
            {
                return NotFound();
            }

            return View(fotografias);
        }

        // POST: Fotografias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotografias = await _context.Fotografias.FindAsync(id);
            if (fotografias != null)
            {
                // se a imagem for diferente da default, vamos apagá-la do disco ao apagar a entrada da BD
                if (fotografias.Ficheiro != "example.png")
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", fotografias.Ficheiro);
                    // se o ficheiro/imagem existir apagamos
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }
               
                _context.Fotografias.Remove(fotografias);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotografiasExists(int id)
        {
            return _context.Fotografias.Any(e => e.Id == id);
        }
    }
}
