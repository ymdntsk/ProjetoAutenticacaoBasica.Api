using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacaoBasica.Api.Models;

namespace ProjetoAutenticacaoBasica.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Usuario> Usuarios{ get; set; }
        public DbSet<Voo> Voos { get; set; }
        public DbSet<Passagem> Passagems { get; set; }
        public DbSet<Passageiro> Passageiros { get; set; }

    }
}

