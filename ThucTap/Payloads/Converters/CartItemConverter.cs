using Microsoft.EntityFrameworkCore;
using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;

namespace ThucTap.Payloads.Converters
{
    public class CartItemConverter : BaseService
    {
        public CartItemDTO EntityToDTO(CartItem cartItem)
        {
            return new CartItemDTO()
            {
                CartItemID = cartItem.CartItemID,
                ProductID = cartItem.ProductID,
                NameProduct = dbContext.Product.FirstOrDefault(x => x.ProductID == cartItem.ProductID).NameProduct,
                Price = dbContext.Product.FirstOrDefault(x => x.ProductID == cartItem.ProductID).Price,
                AvartarImageProduct = dbContext.Product.FirstOrDefault(x => x.ProductID == cartItem.ProductID).AvartarImageProduct,
                Quantity = cartItem.Quantity,
            };
        }
    }
}
