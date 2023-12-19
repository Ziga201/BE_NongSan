namespace ThucTap.Payloads.DTOs
{
    public class RegisterDTO
    {
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string AuthorityName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
