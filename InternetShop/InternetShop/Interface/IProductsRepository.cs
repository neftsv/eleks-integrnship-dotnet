using InternetShop.Models;
using InternetShop.ViewModels;

namespace InternetShop.Interface
{
    public interface IProductsRepository
    {
        Task<ICollection<Products>> GetAllProductsAsync();
        Task<ProductsViewModel> PaginationProductsAsync(int page, ICollection<Products> products);
    }
}
