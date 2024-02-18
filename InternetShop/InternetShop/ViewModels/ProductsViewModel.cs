using InternetShop.Models;

namespace InternetShop.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Products> Items { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
