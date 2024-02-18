using InternetShop.Models;
using InternetShop.ViewModels;

namespace InternetShop.Interface
{
    public interface IProductsRepository
    {
        Task<ICollection<Products>> GetAllProductsAsync();
        Task<ICollection<Products>> GetProductsByCategoryAsync(ICollection<Products> products, ICollection<int> categoryId);
        Task<ICollection<Products>> SearchAsync(ICollection<Products> products, string productName);
        Task<ProductsViewModel> PaginationProductsAsync(int page, ICollection<Products> products);
    }
}
