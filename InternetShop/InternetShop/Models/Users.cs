using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class Users : IdentityUser
    {
        public Carts Carts { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronimic { get; set; }
        public virtual List<UsersProducts> UsersProducts { get; set; }
        public virtual List<Orders> Orders { get; set; }
    }
}
