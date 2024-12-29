using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskFlow_API.Domains
{
    public enum StatusTarefa
    {
        Pending,
        Completed,
        Overdue,
        All
    }
    public class Tarefa
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(150)]
        public required string Nome { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Required]
        public DateTime DataValidade { get; set; }
        public bool Concluido { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public StatusTarefa Status { get; set; } = StatusTarefa.Pending;

    }
}
