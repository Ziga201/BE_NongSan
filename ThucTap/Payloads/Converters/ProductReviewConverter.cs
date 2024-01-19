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
                ProductID = productReview.ProductID,
                NameProduct = dbContext.Product.FirstOrDefault(x => x.ProductID == productReview.ProductID).NameProduct,
                UserName = dbContext.Account.FirstOrDefault(x => x.AccountID == productReview.AccountID).UserName,
                Avatar = dbContext.Account.FirstOrDefault(x => x.AccountID == productReview.AccountID).Avatar,
                PointEvaluation = productReview.PointEvaluation,
                Content = productReview.Content,
                Image = productReview.Image,
                CreatedAt = productReview.CreatedAt,
            };
        }
    }
}
