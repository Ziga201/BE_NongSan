using Microsoft.Identity.Client;
using ThucTap.Entities;
using ThucTap.Handle.Image;
using ThucTap.Payloads.Converters;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Blog;
using ThucTap.Payloads.Responses;
using ThucTap.Services.IServices;

namespace ThucTap.Services.Implement
{
    public class BlogService : BaseService, IBlogService
    {
        private readonly ResponseObject<BlogDTO> responseObject;
        private readonly BlogConverter converter;

        public BlogService()
        {
            responseObject = new ResponseObject<BlogDTO>();
            converter = new BlogConverter();
        }

        public async Task<ResponseObject<BlogDTO>> AddBlog(AddBlogRequest request)
        {
            if (!dbContext.BlogType.Any(x => x.BlogTypeID == request.BlogTypeID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại bài viết không tồn tại", null);
            if (!dbContext.Account.Any(x => x.AccountID == request.AccountID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Tài khoản không tồn tại", null);
            var avatarFile = await UploadImage.Upfile(request.Image);

            Blog blog = new Blog();
            blog.BlogTypeID = request.BlogTypeID;
            blog.AccountID = request.AccountID;
            blog.Title = request.Title;
            blog.Content = request.Content;
            blog.Image = avatarFile == "" ? "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg" : avatarFile;
            blog.CreateAt = DateTime.Now;
            dbContext.Add(blog);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Thêm bài viết thành công", converter.EntityToDTO(blog));
        }

        public ResponseObject<BlogDTO> DeleteBlog(int id)
        {
            var blog = dbContext.Blog.FirstOrDefault(x => x.BlogID == id);
            if (blog == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Bài viết không tồn tại", null);

            dbContext.Remove(blog);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xoá bài viết thành công", converter.EntityToDTO(blog));
        }

        public List<BlogDTO> GetAll()
        {
            return dbContext.Blog.Select(converter.EntityToDTO).ToList();
        }

        public List<BlogDTO> GetAllByBlogTypeID(int id)
        {
            var blog =  dbContext.Blog.Where(x => x.BlogTypeID == id).Select(converter.EntityToDTO).ToList();
            if(blog == null)
                return null;
            return blog;
        }

        public ResponseObject<BlogDTO> GetBlogByID(int id)
        {
            var blog =  dbContext.Blog.FirstOrDefault(x => x.BlogID == id);
            if(blog == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Bài viết không tồn tại", null);
            return responseObject.ResponseSucess("Hiển thị bài viết", converter.EntityToDTO(blog));
        }

        public async Task<ResponseObject<BlogDTO>> UpdateBlog(UpdateBlogRequest request)
        {
            var blog = dbContext.Blog.FirstOrDefault(x => x.BlogID == request.BlogID);
            if (!dbContext.BlogType.Any(x => x.BlogTypeID == request.BlogTypeID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại bài viết không tồn tại", null);
            if (!dbContext.Account.Any(x => x.AccountID == request.AccountID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Tài khoản không tồn tại", null);

            var avatarFile = await UploadImage.Upfile(request.Image);

            blog.BlogTypeID = request.BlogTypeID;
            blog.AccountID = request.AccountID;
            blog.Title = request.Title;
            blog.Content = request.Content;
            blog.Image = avatarFile == "" ? "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg" : avatarFile;
            blog.CreateAt = DateTime.Now;
            dbContext.Update(blog);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Sửa bài viết thành công", converter.EntityToDTO(blog));
        }
    }
}
