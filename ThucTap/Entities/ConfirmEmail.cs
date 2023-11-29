namespace ThucTap.Entities
{
    public class ConfirmEmail
    {
        public int ConfirmEmailID { get; set; }
        public int AccountID { get; set; }
        public int CodeActive { get; set; }
        public DateTime ExpriedTime { get; set; }
        public bool IsConfirmed { get; set; }
        public Account Account { get; set; }
    }
}
