using Microsoft.Identity.Client;
using System.Net;
using System.Numerics;

namespace ThucTap.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int AccountID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Account Account { get; set; }
    }
}
