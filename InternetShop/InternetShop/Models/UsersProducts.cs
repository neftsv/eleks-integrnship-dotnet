using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class UsersProducts
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}
