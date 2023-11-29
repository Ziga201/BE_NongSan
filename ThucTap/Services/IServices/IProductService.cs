using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Product;
using ThucTap.Payloads.Responses;

namespace ThucTap.IServices
{
    public interface IProductService
    {
        Task<ResponseObject<Product>> AddProduct(AddProductRequest request);
        Task<ResponseObject<Product>> UpdateProduct(UpdateProductRequest request);
        ResponseObject<Product> DeleteProduct(int id);
        PageResult<Product> GetProduct(Pagination? pagination);
        List<Product> GetOutstandingProduct(int productTypeID);
        ResponseObject<ProductReviewDTO> AddProductReview(AddProductReviewRequest request);
        List<ProductReviewDTO> GetProductReview(int productID);
    }
}
