using System;
using ThucTap.Enum;

namespace ThucTap.Entities
{
    public class Account
    {
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Status { get; set; } = nameof(Enum.Status.INACTIVE);
        public int DecentralizationID { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        public string? Avatar { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Decentralization Decentralization { get; set; }
    }
}
