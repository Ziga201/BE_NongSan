using ThucTap.Entities;
using ThucTap.Payloads.Converters;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Cart;
using ThucTap.Payloads.Responses;
using ThucTap.Services.IServices;

namespace ThucTap.Services.Implement
{
    public class CartService : BaseService, ICartService
    {
        private readonly ResponseObject<CartItemDTO> responseObject;
        private readonly ResponseObject<Cart> responseObjectCart;
        private readonly CartItemConverter converter;
        public CartService()
        {
            responseObject = new ResponseObject<CartItemDTO>();
            converter = new CartItemConverter();
            responseObjectCart = new ResponseObject<Cart>();
        }
        public ResponseObject<CartItemDTO> AddToCart(AddToCartRequest request)
        {
            var user = dbContext.User.FirstOrDefault(x => x.UserID == request.UserID);
            if (user == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound,"Người dùng không tồn tại", null);
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
            
            
            return responseObject.ResponseSucess("Thêm giỏ vào hàng thành công !", converter.EntityToDTO(cartItem));
        }

        public ResponseObject<Cart> DeleteCart(int userID)
        {
            var cart = dbContext.Cart.FirstOrDefault(x => x.UserID == userID);
            if (cart == null)
                return responseObjectCart.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
            dbContext.Remove(cart);
            dbContext.SaveChanges();
            return responseObjectCart.ResponseSucess("Xóa cart sau khi thanh toán", cart);
        }

        public ResponseObject<CartItemDTO> DeleteCartItem(int cartItemID)
        {
            var cartItem = dbContext.CartItem.FirstOrDefault(x => x.CartItemID == cartItemID);
            if (cartItem == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "CartItem không tồn tại", null);
            dbContext.Remove(cartItem);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xóa CartItem thành công !", converter.EntityToDTO(cartItem));
        }

        public List<CartItemDTO> GetAll(int userID)
        {
            var cart = dbContext.Cart.FirstOrDefault(x => x.UserID == userID);
            if (cart == null)
                return null;
            var cartItem = dbContext.CartItem.Where(x => x.CartID == cart.CartID);
            var cartItemDTO = cartItem.Select(converter.EntityToDTO).ToList();
            return cartItemDTO;
        }
    }
}
