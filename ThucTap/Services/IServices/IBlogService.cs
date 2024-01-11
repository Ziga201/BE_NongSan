using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Blog;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services.IServices
{
    public interface IBlogService
    {
        Task<ResponseObject<BlogDTO>> AddBlog(AddBlogRequest request);
        Task<ResponseObject<BlogDTO>> UpdateBlog(UpdateBlogRequest request);
        ResponseObject<BlogDTO> DeleteBlog(int id);
        List<BlogDTO> GetAll();
        List<BlogDTO> GetAllByBlogTypeID(int id);
        ResponseObject<BlogDTO> GetBlogByID(int id);
    }
}
