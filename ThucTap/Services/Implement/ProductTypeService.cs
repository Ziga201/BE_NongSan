using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.Payloads.Requests.ProductType;
using ThucTap.Payloads.Responses;
using ThucTap.Services.IServices;

namespace ThucTap.Services.Implement
{
    public class ProductTypeService : BaseService, IProductTypeService
    {
        private readonly ResponseObject<ProductType> _responseObject;

        public ProductTypeService()
        {
            _responseObject = new ResponseObject<ProductType>();
        }

        public ResponseObject<ProductType> AddProductType(AddProductTypeRequest request)
        {
            ProductType productType = new ProductType();
            productType.NameProductType = request.NameProductType;
            productType.CreatedAt = DateTime.Now;
            productType.UpdateAt = DateTime.Now;
            dbContext.Add(productType);
            dbContext.SaveChanges();
            return _responseObject.ResponseSucess("Thêm loại sản phẩm thành công", productType);
        }

        public ResponseObject<ProductType> DeleteProductType(int id)
        {
            var productType = dbContext.ProductType.FirstOrDefault(x => x.ProductTypeID == id);
            if (productType == null)
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại sản phẩm không tồn tại", null);
            dbContext.Remove(productType);
            dbContext.SaveChanges();
            return _responseObject.ResponseSucess("Xoá loại sản phẩm thành công", productType);
        }

        public List<ProductType> GetAll()
        {
            var list = dbContext.ProductType.ToList();
            return list;
        }

        public ResponseObject<ProductType> UpdateProductType(UpdateProductTypeRequest request)
        {
            var productType = dbContext.ProductType.FirstOrDefault(x => x.ProductTypeID == request.ProductTypeID);
            if (productType == null)
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại sản phẩm không tồn tại", null);
            productType.NameProductType = request.NameProductType;
            productType.CreatedAt = DateTime.Now;
            productType.UpdateAt = DateTime.Now;
            dbContext.Update(productType);
            dbContext.SaveChanges();
            return _responseObject.ResponseSucess("Sửa loại sản phẩm thành công", productType);
        }
    }
}
