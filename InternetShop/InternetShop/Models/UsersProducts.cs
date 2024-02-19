using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models
{
    public class UsersProducts
    {
        public string UserId {  get; set; }
        public int ProductId { get; set; }
        public Users Users { get; set; }
        public Products Products { get; set; }
    }
}
