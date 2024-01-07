using ThucTap.Entities;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services.IServices
{
    public interface IBlogTypeService
    {
        ResponseObject<BlogType> AddBlogType(string blogTypeName);
        ResponseObject<BlogType> UpdateBlogType(BlogType blogType);
        ResponseObject<BlogType> DeleteBlogType(int id);
        List<BlogType> GetAll();
        
    }
}
