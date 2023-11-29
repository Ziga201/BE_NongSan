namespace ThucTap.Payloads.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        //public string Avatar { get; set; }
        public string AuthorityName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
