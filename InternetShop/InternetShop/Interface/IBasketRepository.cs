using InternetShop.Models;
using InternetShop.ViewModels;

namespace InternetShop.Interface
{
    public interface IBasketRepository
    {
        Task<CartViewModel> GetUserCratProductsAsync(string userName);
        Task<CartsProducts> GetCartProductAsync(int cartsProductsId);
        Task<int> GetCartItemCount(string userName);
        bool DeleteProduct(CartsProducts cartsProducts);
        bool Save();
    }
}
