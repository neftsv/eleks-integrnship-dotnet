using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class Carts
    {
        [Key]
        public int Id { get; set; }
        public Users Users { get; set; }
    }
}
