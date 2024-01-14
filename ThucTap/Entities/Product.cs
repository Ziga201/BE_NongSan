using System;

namespace ThucTap.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public int ProductTypeID { get; set; }
        public string NameProduct { get; set; }
        public float Price { get; set; }
        public string? AvatarImageProduct { get; set; }
        public string? Describe { get; set; }
        public int? Discount { get; set; }
        public string? Status { get; set; }
        public int? Quantity { get; set; }
        public int? Purchases { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public ProductType ProductType { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
