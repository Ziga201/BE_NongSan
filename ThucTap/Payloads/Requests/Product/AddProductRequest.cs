namespace ThucTap.Payloads.Requests.Product
{
    public class AddProductRequest
    {
        public int ProductTypeID { get; set; }
        public string NameProduct { get; set; }
        public float Price { get; set; }
        public IFormFile? AvatarImageProduct { get; set; }
        public string Title { get; set; }
        public int? Discount { get; set; }
    }
}
