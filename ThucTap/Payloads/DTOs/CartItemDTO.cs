﻿namespace ThucTap.Payloads.DTOs
{
    public class CartItemDTO
    {
        public int CartItemID { get; set; }
        public int ProductID { get; set; }

        public string NameProduct { get; set; }
        public float Price { get; set; }
        public string AvartarImageProduct { get; set; }
        public int Quantity { get; set; }
    }
}