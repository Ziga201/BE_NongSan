using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;

namespace ThucTap.Payloads.Converters
{
    public class ProductReviewConverter : BaseService
    {
        public ProductReviewDTO EntityToDTO(ProductReview productReview)
        {
            return new ProductReviewDTO()
            {
                NameProduct = dbContext.Product.FirstOrDefault(x => x.ProductID == productReview.ProductID).NameProduct,
                UserName = dbContext.Account.FirstOrDefault(x => x.AccountID == productReview.AccountID).UserName,
                ContentRated = productReview.ContentRated,
                PointEvaluation = productReview.PointEvaluation,
                ContentSeen = productReview.ContentSeen,
                CreatedAt = productReview.CreatedAt,
            };
        }
    }
}
