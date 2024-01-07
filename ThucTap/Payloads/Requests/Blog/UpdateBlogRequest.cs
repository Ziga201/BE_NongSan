namespace ThucTap.Payloads.Requests.Blog
{
    public class UpdateBlogRequest
    {
        public int BlogID { get; set; }
        public int BlogTypeID { get; set; }
        public int AccountID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
