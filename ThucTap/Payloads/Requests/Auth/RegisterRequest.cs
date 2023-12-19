namespace ThucTap.Payloads.Requests
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public IFormFile? Avatar { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
