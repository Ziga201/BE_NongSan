using Microsoft.EntityFrameworkCore;
using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;

namespace ThucTap.Payloads.Converters
{
    public class OrderDetailConverter : BaseService
    {
        public OrderDetailDTO EntityToDTO(OrderDetail orderDetail)
        {
            return new OrderDetailDTO()
            {
                NameProduct = dbContext.Product.FirstOrDefault(x => x.ProductID == orderDetail.ProductID).NameProduct,
                AvatarImageProduct = dbContext.Product.FirstOrDefault(x => x.ProductID == orderDetail.ProductID).AvatarImageProduct,
                PriceTotal = orderDetail.PriceTotal,
                Quantity = orderDetail.Quantity,

            };
        }
    }
}
