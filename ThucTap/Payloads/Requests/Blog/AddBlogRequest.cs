namespace ThucTap.Payloads.Requests.Blog
{
    public class AddBlogRequest
    {
        public int BlogTypeID { get; set; }
        public int AccountID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
