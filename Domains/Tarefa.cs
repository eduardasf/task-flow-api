using System.ComponentModel.DataAnnotations;
namespace TaskFlow_API.Domains
{
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
    }
}
