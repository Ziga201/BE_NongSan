namespace ThucTap.Payloads.Requests.Product
{
    public class UpdateProductRequest
    {
        public int ProductID { get; set; }
        public int ProductTypeID { get; set; }
        public string NameProduct { get; set; }
        public float Price { get; set; }
        public IFormFile? AvartarImageProduct { get; set; }
        public string? Status { get; set; }
        public string Title { get; set; }
        public int? Discount { get; set; }
    }
}
