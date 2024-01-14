namespace ThucTap.Payloads.Requests.Product
{
    public class AddProductReviewRequest
    {
        public int ProductID { get; set; }
        public int AccountID { get; set; }
        public int PointEvaluation { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
