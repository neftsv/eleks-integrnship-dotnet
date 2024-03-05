using InternetShop.Models;
using InternetShop.ViewModels;

namespace InternetShop.Interface
{
    public interface IHomeRepository
    {
        Task<ICollection<HomeViewModel>> GetAllCategoriesAsync();
    }
}
