namespace ThucTap.Payloads.Requests.Product
{
    public class AddProductReviewRequest
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string ContentRated { get; set; }
        public int PointEvaluation { get; set; }
        public string ContentSeen { get; set; }
    }
}
