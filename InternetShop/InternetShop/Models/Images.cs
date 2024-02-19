using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Products {  get; set; }
        public string Url { get; set; }
    }
}
