﻿namespace InternetShop.Models
{
    public class Carts
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int ProductId {  get; set; }
        public int Quantity {  get; set; }
    }
}