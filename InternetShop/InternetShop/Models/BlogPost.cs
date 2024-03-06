using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")] 
        public Users Users { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        [Required]
        public string? image { get; set; }

    }
}