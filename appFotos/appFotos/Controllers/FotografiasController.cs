using System.Globalization;
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
            /*
             * select *
             * from fotografias f, utilizadores u, categorias c
             * where f.categoriaFk = c.Id and f.donoFk = u.Id
             */

            var listaFotografias = _context.Fotografias
                .Include(f => f.Categoria)
                .Include(f => f.Dono);
            return View(await listaFotografias.ToListAsync());
        }

        // GET: Fotografias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografias
                .Include(f => f.Categoria)
                .Include(f => f.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotografia == null)
            {
                return NotFound();
            }

            return View(fotografia);
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
        public async Task<IActionResult> Create([Bind("Titulo,Descricao,DataFotografia,PrecoAux,DonoFk,CategoriaFk")] Fotografias fotografia, IFormFile ficheiroFotografia)
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
            
            bool haImagem = false;
            var nomeImagem = "";
            
            // validação de FKs
            var utilizador = _context.Utilizadores
                .Where(u => u.Id==fotografia.DonoFk);

            if (!utilizador.Any())
            {
                ModelState.AddModelError("DonoFk", "Tem de selecionar um Dono correto");
            }

            var categoria = _context.Categorias.FirstOrDefaultAsync(c => c.Id == fotografia.CategoriaFk);
            if (categoria.Result == null)
            {
                ModelState.AddModelError("CategoriaFk", "Tem de selecionar uma Categoria correta");
            }
            
            // se não recebermos nenhum ficheiro, enviamos ao user um erro a dizer que tem de submeter um
            if (ficheiroFotografia == null)
            {
                ModelState.AddModelError("", "Tem de submeter um ficheiro");
            }
            
            if (ModelState.IsValid)
            {
                fotografia.Preco = Convert.ToDecimal(fotografia.PrecoAux.Replace('.', ','),  
                    new CultureInfo("pt-PT"));
                
                // se entrar no else foi submetido um ficheiro
                // há ficheiro, mas é uma imagem?
                if (!(ficheiroFotografia.ContentType == "image/png" ||
                      ficheiroFotografia.ContentType == "image/jpeg"
                    )) {
                    // não
                    // vamos usar uma imagem pre-definida
                    fotografia.Ficheiro = "example.png";
                }
                // se chegar aqui é um ficheiro & uma imagem
                else
                {
                    haImagem = true;
                    // gerar nome imagem
                    Guid g = Guid.NewGuid();
                    // atrás do nome adicionamos a pasta onde a escrevemos
                    nomeImagem = g.ToString();
                    string extensaoImagem =Path.GetExtension(ficheiroFotografia.FileName).ToLowerInvariant();
                    nomeImagem += extensaoImagem;
                    // guardar o nome do ficheiro na BD
                    fotografia.Ficheiro = "imagens/"+nomeImagem;
                }
                
                
                // se existe uma imagem para escrever no disco
                if (haImagem)
                {
                    // vai construir o path para o diretório onde são guardadas as imagens
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/imagens");
                    
                    // antes de escrevermos o ficheiro, vemos se o diretório existe
                    if(!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);
                    
                    // atualizamos o Path para incluir o nome da imagem
                    filePath = Path.Combine(filePath, nomeImagem);
                    
                    // escreve a imagem
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ficheiroFotografia.CopyToAsync(fileStream);
                    }
                }
                
                _context.Add(fotografia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaFk"] = new SelectList(_context.Categorias, "Id", "Categoria", fotografia.CategoriaFk);
            ViewData["DonoFk"] = new SelectList(_context.Utilizadores, "Id", "Nome", fotografia.DonoFk);
            return View(fotografia);
        }

        // GET: Fotografias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografias.FindAsync(id);
            if (fotografia == null)
            {
                return NotFound();
            }
            
            fotografia.PrecoAux = fotografia.Preco.ToString();
            ViewData["CategoriaFk"] = new SelectList(_context.Categorias, "Id", "Categoria", fotografia.CategoriaFk);
            ViewData["DonoFk"] = new SelectList(_context.Utilizadores, "Id", "Nome", fotografia.DonoFk);
            return View(fotografia);
        }

        // POST: Fotografias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,PrecoAux,DataFotografia,DonoFk,CategoriaFk")] Fotografias fotografia)
        {
            if (id != fotografia.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                fotografia.Preco = Convert.ToDecimal(fotografia.PrecoAux.Replace('.', ','),  
                    new CultureInfo("pt-PT"));
                
                try
                {
                    _context.Update(fotografia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotografiasExists(fotografia.Id))
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
            ViewData["CategoriaFk"] = new SelectList(_context.Categorias, "Id", "Categoria", fotografia.CategoriaFk);
            ViewData["DonoFk"] = new SelectList(_context.Utilizadores, "Id", "CodPostal", fotografia.DonoFk);
            return View(fotografia);
        }

        // GET: Fotografias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografias
                .Include(f => f.Categoria)
                .Include(f => f.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotografia == null)
            {
                return NotFound();
            }

            return View(fotografia);
        }

        // POST: Fotografias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotografia = await _context.Fotografias.FindAsync(id);
            if (fotografia != null)
            {
                // se a imagem for diferente da default, vamos apagá-la do disco ao apagar a entrada da BD
                if (fotografia.Ficheiro != "example.png")
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", fotografia.Ficheiro);
                    // se o ficheiro/imagem existir apagamos
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }
               
                _context.Fotografias.Remove(fotografia);
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
