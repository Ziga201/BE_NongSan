using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Product;
using ThucTap.Payloads.Responses;

namespace ThucTap.IServices
{
    public interface IProductService
    {
        Task<ResponseObject<ProductDTO>> AddProduct(AddProductRequest request);
        Task<ResponseObject<ProductDTO>> UpdateProduct(UpdateProductRequest request);
        ResponseObject<ProductDTO> DeleteProduct(int id);
        PageResult<ProductDTO> GetProduct(Pagination? pagination);
        List<ProductDTO> GetOutstandingProduct(int productTypeID);
        ResponseObject<ProductReviewDTO> AddProductReview(AddProductReviewRequest request);
        List<ProductReviewDTO> GetProductReview(int productID);
        ResponseObject<ProductDTO> UpdateView(int id);
        int NumberOfPurchases(int id);
        ResponseObject<ProductDTO> GetProductByID(int id);

    }
}
