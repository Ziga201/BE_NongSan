using CloudinaryDotNet;
using System.Threading.Tasks;
using ThucTap.Entities;
using ThucTap.Handle.Image;
using ThucTap.Handle.Page;
using ThucTap.IServices;
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
        private readonly ResponseObject<Product> responseObject;
        private readonly ResponseObject<ProductReviewDTO> responseProductReviewObject;

        public ProductService()
        {
            converter = new ProductReviewConverter();
            responseObject = new ResponseObject<Product>();
            responseProductReviewObject = new ResponseObject<ProductReviewDTO>();
        }

        public async Task<ResponseObject<Product>> AddProduct(AddProductRequest request)
        {
            if (!dbContext.ProductType.Any(x => x.ProductTypeID == request.ProductTypeID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại sản phẩm không tồn tại", null);
            var avatarFile = await UploadImage.Upfile(request.AvartarImageProduct);

            Product product = new Product();
            product.ProductTypeID = request.ProductTypeID;
            product.NameProduct = request.NameProduct;
            product.Price = request.Price;
            product.AvartarImageProduct = avatarFile == "" ? "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg" : avatarFile;
            product.Title = request.Title;
            product.Discount = request.Discount;
            product.Status = nameof(Enum.Status.ACTIVE);
            product.CreatedAt = DateTime.Now;
            product.UpdateAt = DateTime.Now;
            dbContext.Add(product);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Thêm sản phẩm thành công", product);
        }

        public ResponseObject<ProductReviewDTO> AddProductReview(AddProductReviewRequest request)
        {
            if (dbContext.Product.Any(x => x.ProductID == request.ProductID))
                return responseProductReviewObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy sản phẩm", null);
            if (dbContext.User.Any(x => x.UserID == request.UserID))
                return responseProductReviewObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy người dùng", null);
            ProductReview productReview = new ProductReview();
            productReview.ProductID = request.ProductID;
            productReview.UserID = request.UserID;
            productReview.ContentRated = request.ContentRated;
            productReview.PointEvaluation = request.PointEvaluation;
            productReview.ContentSeen = request.ContentSeen;
            productReview.Status = nameof(Enum.Status.ACTIVE);
            productReview.CreatedAt = DateTime.Now;
            productReview.UpdateAt = DateTime.Now;
            dbContext.Add(productReview);
            dbContext.SaveChanges();
            return responseProductReviewObject.ResponseSucess("Thêm đánh giá thành công", converter.EntityToDTO(productReview));
        }

        public ResponseObject<Product> DeleteProduct(int id)
        {
            var product = dbContext.Product.FirstOrDefault(x => x.ProductID == id);
            if (product == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Sản phẩm không tồn tại", null);
            
            dbContext.Remove(product);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xoá sản phẩm thành công", product);
        }

        public List<Product> GetOutstandingProduct(int productTypeID)
        {
            return dbContext.Product.Where(x => x.ProductTypeID == productTypeID).OrderByDescending(x => x.NumberOfViews).ToList();
        }

        public PageResult<Product> GetProduct(Pagination? pagination)
        {
            var listProduct = dbContext.Product.AsQueryable();
            var result = PageResult<Product>.ToPageResult(pagination, listProduct);
            pagination.TotalCount = listProduct.Count();
            return new PageResult<Product>(pagination, result);
        }

        public List<ProductReviewDTO> GetProductReview(int productID)
        {
            var list = dbContext.ProductReview.Where(x => x.ProductID == productID).ToList();
            if (list == null)
                return null;
            return converter.EntityToListDTO(list);
        }

        public int NumberOfPurchases(int id)
        {
            var product = dbContext.OrderDetail.FirstOrDefault(x => x.ProductID == id);
            if (product == null) return 0;
            var total = dbContext.OrderDetail.Where(x => x.ProductID == id).Sum(x => x.Quantity);
            return total;
        }

        public async Task<ResponseObject<Product>> UpdateProduct(UpdateProductRequest request)
        {
            var product = dbContext.Product.FirstOrDefault(x => x.ProductID == request.ProductID);
            if (product == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Sản phẩm không tồn tại", null);
            if (!dbContext.ProductType.Any(x => x.ProductTypeID == request.ProductTypeID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại sản phẩm không tồn tại", null);
            var avatarFile = await UploadImage.Upfile(request.AvartarImageProduct);

            product.ProductTypeID = request.ProductTypeID;
            product.NameProduct = request.NameProduct;
            product.Price = request.Price;
            product.AvartarImageProduct = avatarFile == "" ? "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg" : avatarFile;
            product.Title = request.Title;
            product.Discount = request.Discount;
            product.Status = request.Status;
            product.CreatedAt = DateTime.Now;
            product.UpdateAt = DateTime.Now;
            dbContext.Update(product);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Sửa sản phẩm thành công", product);
        }

        public ResponseObject<Product> UpdateView(int id)
        {
            var product = dbContext.Product.FirstOrDefault(x => x.ProductID == id);
            product.NumberOfViews += 1;
            dbContext.Update(product);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Cập nhật lượt xem thành công", product);
        }
    }
}
