﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class OrdersProducts
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Orders Orders { get; set; }
        public int Quantity { get; set; }
        [Range(0, 7)]
        public int Status { get; set; } = 0;
    }
}
