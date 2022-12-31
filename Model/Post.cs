using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace trading_app_3_api.Model
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(1)]
        [Column(TypeName = "varchar(500)")]
        public string? Content { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public User.User? User{ get; set; }
    }
}
