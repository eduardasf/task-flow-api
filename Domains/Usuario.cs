using System.ComponentModel.DataAnnotations;

namespace TaskFlow_API.Domains
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Email { get; set; }
    }
}
