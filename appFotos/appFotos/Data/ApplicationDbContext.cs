using appFotos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace appFotos.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Utilizadores> Utilizadores { get; set; }
    
    public DbSet<Gostos> Gostos { get; set; }
    
    public DbSet<Fotografias> Fotografias { get; set; }
    
    public DbSet<Categorias> Categorias { get; set; }
    
    public DbSet<Compras> Compras { get; set; }
}