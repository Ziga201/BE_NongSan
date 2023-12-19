namespace ThucTap.Payloads.Requests.Auth
{
    public class UpdateAccountRequest
    {
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public IFormFile? Avatar { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
