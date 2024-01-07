using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;

namespace ThucTap.Payloads.Converters
{
    public class ProductConverter : BaseService
    {
        public ProductDTO EntityToDTO(Product product)
        {
            return new ProductDTO()
            {
                ProductID = product.ProductID,
                ProductTypeID = product.ProductTypeID,
                NameProductType = dbContext.ProductType.FirstOrDefault(x => x.ProductTypeID == product.ProductTypeID).NameProductType,
                NameProduct = product.NameProduct,
                Price = product.Price,
                AvatarImageProduct = product.AvatarImageProduct,
                Title = product.Title,
                Discount = product.Discount,
                Status = product.Status,
                NumberOfViews = product.NumberOfViews,
            };
        }
    }
}
