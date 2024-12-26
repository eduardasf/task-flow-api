using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Domains;

namespace TaskFlow_API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
