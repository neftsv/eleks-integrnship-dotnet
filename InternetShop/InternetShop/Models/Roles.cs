namespace InternetShop.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Users> Users { get; set; }
    }
}
