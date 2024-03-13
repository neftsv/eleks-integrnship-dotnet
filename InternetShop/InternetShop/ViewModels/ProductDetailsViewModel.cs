using InternetShop.Models;

namespace InternetShop.ViewModels
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Images> Images { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
