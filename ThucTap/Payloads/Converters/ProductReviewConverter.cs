using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;

namespace ThucTap.Payloads.Converters
{
    public class ProductReviewConverter : BaseService
    {
        public List<ProductReviewDTO> EntityToListDTO(List<ProductReview> list)
        {
            List<ProductReviewDTO> listDTO = new List<ProductReviewDTO>();
            foreach (var productReview in list)
            {
                ProductReviewDTO dto = new ProductReviewDTO();
                dto.NameProduct = dbContext.Product.FirstOrDefault(x => x.ProductID == productReview.ProductID).NameProduct;
                dto.UserName = dbContext.User.FirstOrDefault(x => x.UserID == productReview.UserID).UserName;
                dto.ContentRated = productReview.ContentRated;
                dto.PointEvaluation = productReview.PointEvaluation;
                dto.ContentSeen = productReview.ContentSeen;
                dto.CreatedAt = productReview.CreatedAt;
                listDTO.Add(dto);
            }
            return listDTO;
        }
        public ProductReviewDTO EntityToDTO(ProductReview productReview)
        {
            return new ProductReviewDTO()
            {
                NameProduct = dbContext.Product.FirstOrDefault(x => x.ProductID == productReview.ProductID).NameProduct,
                UserName = dbContext.User.FirstOrDefault(x => x.UserID == productReview.UserID).UserName,
                ContentRated = productReview.ContentRated,
                PointEvaluation = productReview.PointEvaluation,
                ContentSeen = productReview.ContentSeen,
                CreatedAt = productReview.CreatedAt,
            };
        }
    }
}
