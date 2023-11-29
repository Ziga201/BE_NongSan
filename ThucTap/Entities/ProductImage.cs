namespace ThucTap.Entities
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        public string Title { get; set; }
        public string ImageProduct { get; set; }
        public int ProductID { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Product Product { get; set; }
    }
}
