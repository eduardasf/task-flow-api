using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Domains;

namespace TaskFlow_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
