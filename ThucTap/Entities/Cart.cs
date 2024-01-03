using System.ComponentModel.DataAnnotations;

namespace ThucTap.Entities
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int AccountID { get; set; }
        public Account Account { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
