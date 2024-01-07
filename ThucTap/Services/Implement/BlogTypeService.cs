using ThucTap.Entities;
using ThucTap.Payloads.Responses;
using ThucTap.Services.IServices;

namespace ThucTap.Services.Implement
{
    public class BlogTypeService : BaseService, IBlogTypeService
    {
        private readonly ResponseObject<BlogType> responseObject;
        public BlogTypeService()
        {
            responseObject = new ResponseObject<BlogType>();
        }

        public ResponseObject<BlogType> AddBlogType(string blogTypeName)
        {
            BlogType blogType = new BlogType();
            blogType.BlogTypeName = blogTypeName;
            dbContext.Add(blogType);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Thêm loại bài viết thành công", blogType);
        }

        public ResponseObject<BlogType> DeleteBlogType(int id)
        {
            var blogType = dbContext.BlogType.FirstOrDefault(x => x.BlogTypeID == id);
            if (blogType == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại bài viết không tồn tại", null);
            dbContext.Remove(blogType);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xoá loại bài viết thành công", blogType);
        }

        public List<BlogType> GetAll()
        {
            return dbContext.BlogType.ToList();

        }

        public ResponseObject<BlogType> UpdateBlogType(BlogType blogType)
        {
            var blogCanTim = dbContext.BlogType.FirstOrDefault(x => x.BlogTypeID == blogType.BlogTypeID);
            if (blogCanTim == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Loại bài viết không tồn tại", null);
            blogCanTim.BlogTypeName = blogType.BlogTypeName;
            dbContext.Update(blogCanTim);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Sửa loại bài viết thành công", blogCanTim);
        }
    }
}
