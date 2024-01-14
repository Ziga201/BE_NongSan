using CloudinaryDotNet;
using System.Threading.Tasks;
using ThucTap.Entities;
using ThucTap.Handle.Image;
using ThucTap.Handle.Page;
using ThucTap.IServices;
using ThucTap.Migrations;
using ThucTap.Payloads.Converters;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Product;
using ThucTap.Payloads.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThucTap.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly ProductReviewConverter converter;
        private readonly ProductConverter converterProduct;
        private readonly ResponseObject<ProductDTO> responseObject;
        private readonly ResponseObject<ProductReviewDTO> responseProductReviewObject;

        public ProductService()
        {
            converter = new ProductReviewConverter();
            converterProduct = new ProductConverter();
            responseObject = new ResponseObject<ProductDTO>();
            responseProductReviewObject = new ResponseObject<ProductReviewDTO>();
        }

        public async Task<ResponseObject<ProductDTO>> AddProduct(AddProductRequest request)
        {
            if (!dbContext.ProductType.Any(x => x.ProductTypeID == request.ProductTypeID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại sản phẩm không tồn tại", null);
            var avatarFile = await UploadImage.Upfile(request.AvatarImageProduct);

            Product product = new Product();
            product.ProductTypeID = request.ProductTypeID;
            product.NameProduct = request.NameProduct;
            product.Price = request.Price;
            product.AvatarImageProduct = avatarFile == "" ? "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg" : avatarFile;
            product.Describe = request.Describe;
            product.Discount = request.Discount;
            product.Status = request.Status;
            product.Quantity = request.Quantity?? 0;
            product.CreatedAt = DateTime.Now;
            product.UpdateAt = DateTime.Now;
            dbContext.Add(product);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Thêm sản phẩm thành công", converterProduct.EntityToDTO(product));
        }

        public async Task<ResponseObject<ProductReviewDTO>> AddProductReview(AddProductReviewRequest request)
        {
            if (!dbContext.Product.Any(x => x.ProductID == request.ProductID))
                return responseProductReviewObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy sản phẩm", null);
            if (!dbContext.Account.Any(x => x.AccountID == request.AccountID))
                return responseProductReviewObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy người dùng", null);
            var avatarFile = await UploadImage.Upfile(request.Image);

            ProductReview productReview = new ProductReview();
            productReview.ProductID = request.ProductID;
            productReview.AccountID = request.AccountID;
            productReview.PointEvaluation = request.PointEvaluation;
            productReview.Content = request.Content;
            productReview.Image = avatarFile;
            productReview.CreatedAt = DateTime.Now;
            dbContext.Add(productReview);
            dbContext.SaveChanges();
            return responseProductReviewObject.ResponseSucess("Thêm đánh giá thành công", converter.EntityToDTO(productReview));
        }

        public ResponseObject<ProductDTO> DeleteProduct(int id)
        {
            var product = dbContext.Product.FirstOrDefault(x => x.ProductID == id);
            if (product == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Sản phẩm không tồn tại", null);

            dbContext.Remove(product);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xoá sản phẩm thành công", converterProduct.EntityToDTO(product));
        }

        public List<ProductDTO> GetRelatedProduct(int productTypeID)
        {
            return dbContext.Product.Where(x => x.ProductTypeID == productTypeID).OrderBy(x => Guid.NewGuid()).Select(converterProduct.EntityToDTO).ToList();
        }

        public PageResult<ProductDTO> GetProduct(Pagination? pagination)
        {
            var listProduct = dbContext.Product.Select(converterProduct.EntityToDTO);
            var result = PageResult<ProductDTO>.ToPageResult(pagination, listProduct);
            pagination.TotalCount = listProduct.Count();
            return new PageResult<ProductDTO>(pagination, result);
        }

        public ResponseObject<ProductDTO> GetProductByID(int id)
        {
            var product = dbContext.Product.FirstOrDefault(x => x.ProductID == id);
            if (product == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Sản phẩm không tồn tại", null);
            return responseObject.ResponseSucess("Hiện thị sản phẩm thành công", converterProduct.EntityToDTO(product));
        }

        public List<ProductReviewDTO> GetProductReview(int productID)
        {
            var list = dbContext.ProductReview.Where(x => x.ProductID == productID);
            if (list == null)
                return null;
            var listDTO = list.Select(converter.EntityToDTO).ToList();
            return listDTO;
        }

        public int NumberOfPurchases(int id)
        {
            var product = dbContext.OrderDetail.FirstOrDefault(x => x.ProductID == id);
            if (product == null) return 0;
            var total = dbContext.OrderDetail.Where(x => x.ProductID == id).Sum(x => x.Quantity);
            return total;
        }

        public async Task<ResponseObject<ProductDTO>> UpdateProduct(UpdateProductRequest request)
        {
            var product = dbContext.Product.FirstOrDefault(x => x.ProductID == request.ProductID);
            if (product == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Sản phẩm không tồn tại", null);
            if (!dbContext.ProductType.Any(x => x.ProductTypeID == request.ProductTypeID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại sản phẩm không tồn tại", null);
            var avatarFile = await UploadImage.Upfile(request.AvatarImageProduct);

            product.ProductTypeID = request.ProductTypeID;
            product.NameProduct = request.NameProduct;
            product.Price = request.Price;
            product.AvatarImageProduct = avatarFile == "" ? "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg" : avatarFile;
            product.Describe = request.Describe;
            product.Discount = request.Discount;
            product.Status = request.Status;
            product.Quantity = request.Quantity ?? 0;
            product.CreatedAt = DateTime.Now;
            product.UpdateAt = DateTime.Now;
            dbContext.Update(product);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Sửa sản phẩm thành công", converterProduct.EntityToDTO(product));
        }

        public List<ProductReviewDTO> GetProductReviewByAccountID(int accountID)
        {
            var list = dbContext.ProductReview.Where(x => x.AccountID == accountID);
            if (list == null)
                return null;
            var listDTO = list.Select(converter.EntityToDTO).ToList();
            return listDTO;
        }
    }
}
