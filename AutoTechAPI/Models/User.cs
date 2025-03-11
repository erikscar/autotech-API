using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTechAPI.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("email")]
        [Required]
        public string Email { get; set; }

        [Column("hash_password")]
        [Required]
        public string HashPassword { get; set; }

        [Column("created_at", TypeName = "DATE")]
        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
