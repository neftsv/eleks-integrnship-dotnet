namespace InternetShop.Models
{
    public class Users
    {
        public int Id { get; set; }
        public int RoleId { get; set; } = 0;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Surname {  get; set; }
        public string? Name { get; set; }
        public string? Patronimic {  get; set; }
        public string? PhoneNumber {  get; set; }
    }
}
