using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Domains;

namespace TaskFlow_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para o enum StatusTarefa
            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusTarefa)Enum.Parse(typeof(StatusTarefa), v)
                );
        }
    }
}
