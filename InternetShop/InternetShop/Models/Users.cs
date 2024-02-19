using Microsoft.AspNetCore.Identity;

namespace InternetShop.Models
{
    public class Users : IdentityUser
    {
        public string Surname {  get; set; }
        public string Name { get; set; }
        public string Patronimic {  get; set; }
        public virtual List<UsersProducts> UsersProducts { get; set; }
    }
}
