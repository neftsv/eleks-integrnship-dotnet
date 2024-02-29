using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Products> Products { get; set; }
        public string ImageUrl { get; set; }
    }
}
