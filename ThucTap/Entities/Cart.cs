using System.ComponentModel.DataAnnotations;

namespace ThucTap.Entities
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
