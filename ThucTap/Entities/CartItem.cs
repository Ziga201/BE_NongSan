namespace ThucTap.Entities
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Cart Cart { get; set; }
    }
}
