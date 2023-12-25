using ThucTap.Entities;
using ThucTap.Payloads.Requests.Cart;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services.IServices
{
    public interface ICartService
    {
        ResponseObject<CartItem> AddToCart(AddToCartRequest request);
    }
}
