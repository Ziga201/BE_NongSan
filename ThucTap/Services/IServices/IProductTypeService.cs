using Microsoft.AspNetCore.Mvc.RazorPages;
using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.Payloads.Requests.ProductType;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services.IServices
{
    public interface IProductTypeService
    {
        ResponseObject<ProductType> AddProductType(AddProductTypeRequest request);
        ResponseObject<ProductType> UpdateProductType(UpdateProductTypeRequest request);
        ResponseObject<ProductType> DeleteProductType(int id);
        List<ProductType> GetAll();
    }
}
