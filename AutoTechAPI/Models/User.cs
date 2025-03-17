using Microsoft.AspNetCore.Http.HttpResults;
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
        [MaxLength(100)]
        public string? Name { get; set; }

        [Column("email")]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("hash_password")]
        [Required]
        [MaxLength(100)]
        public string HashPassword { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

    }
}

//CREATE TABLE Users (
//id INT IDENTITY(1, 1) PRIMARY KEY,
//name NVARCHAR(100),
//email NVARCHAR(100) NOT NULL,
//hash_password NVARCHAR(100) NOT NULL,
//created_at DATETIME2 DEFAULT SYSDATETIME()
//)