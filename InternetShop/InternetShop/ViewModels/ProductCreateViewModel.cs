using InternetShop.Models;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.ViewModels
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }
        public int ProductUserId { get; set; }
        public int CurrentUserId { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
        public int UserId { get; set; }
        [Range(1, short.MaxValue, ErrorMessage = "You have to select category.")]
        public int CategoryId { get; set; }
        public ICollection<Categories>? Categories { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


    }
}
