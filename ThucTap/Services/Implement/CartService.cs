using ThucTap.Entities;
using ThucTap.Payloads.Requests.Cart;
using ThucTap.Payloads.Responses;
using ThucTap.Services.IServices;

namespace ThucTap.Services.Implement
{
    public class CartService : BaseService, ICartService
    {
        private readonly ResponseObject<CartItem> responseObject;
        public CartService()
        {
            responseObject = new ResponseObject<CartItem>();
        }
        public ResponseObject<CartItem> AddToCart(AddToCartRequest request)
        {
            var cart = dbContext.Cart.FirstOrDefault(x => x.UserID == request.UserID);
            if (cart == null)
            {
                cart = new Cart() { UserID = request.UserID };
                dbContext.Add(cart);
                dbContext.SaveChanges();
            }
            var cartItem = dbContext.CartItem.FirstOrDefault(x => x.ProductID ==  request.ProductID && x.CartID == cart.CartID);
            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    CartID = cart.CartID,
                    ProductID = request.ProductID,
                    Quantity = 1,
                };
                dbContext.Add(cartItem);
                dbContext.SaveChanges();
            }
            else
            {
                cartItem.Quantity += 1;
                dbContext.Update(cartItem);
                dbContext.SaveChanges();
            }
            
            
            return responseObject.ResponseSucess("Thêm giỏ hàng thành công", cartItem);
        }
    }
}
