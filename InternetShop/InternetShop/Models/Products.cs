using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }
        public string Name { get; set; }
        public virtual List<Images> Images { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<UsersProducts> UsersProducts { get; set; }
        public virtual List<OrdersProducts> OrdersProducts { get; set; }
    }
}
