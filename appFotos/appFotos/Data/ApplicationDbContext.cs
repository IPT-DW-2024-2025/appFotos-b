using appFotos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace appFotos.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 'importa' todo o comportamento do método, 
        // aquando da sua definição na SuperClasse
        base.OnModelCreating(modelBuilder);

        // criar os perfis de utilizador da nossa app
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "a", Name = "Administrador", NormalizedName = "ADMINISTRADOR" });

        // criar um utilizador para funcionar como ADMIN
        // função para codificar a password
        var hasher = new PasswordHasher<IdentityUser>();
        // criação do utilizador
        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "admin",
                UserName = "admin@mail.pt",
                NormalizedUserName = "ADMIN@MAIL.PT",
                Email = "admin@mail.pt",
                NormalizedEmail = "ADMIN@MAIL.PT",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "Aa0_aa")
            }
        );
        // Associar este utilizador à role ADMIN
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "admin", RoleId = "a" });
        
    }

    public DbSet<Utilizadores> Utilizadores { get; set; }

    public DbSet<Gostos> Gostos { get; set; }

    public DbSet<Fotografias> Fotografias { get; set; }

    public DbSet<Categorias> Categorias { get; set; }

    public DbSet<Compras> Compras { get; set; }
}