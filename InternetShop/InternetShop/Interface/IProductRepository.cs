using InternetShop.Models;
using InternetShop.ViewModels;

namespace InternetShop.Interface
{
    public interface IProductRepository
    {
        Task<Users> GetUserAsync(string userName);
        Task<Carts> GetUserCartAsync(int userId);
        Task<ProductViewModel> GetProductInfoAsync(int productId);
        bool AddCartToUser(Carts cart);
        bool AddProductToCart(CartsProducts products);
        bool Save();
    }
}
