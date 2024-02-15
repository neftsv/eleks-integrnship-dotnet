﻿namespace InternetShop.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int ProductId {  get; set; }
        public int Quantity {  get; set; }
        public decimal TotalPrice {  get; set; }
        public DateTime Date {  get; set; }
    }
}
