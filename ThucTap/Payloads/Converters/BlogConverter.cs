using ThucTap.Entities;
using ThucTap.Migrations;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ThucTap.Payloads.Converters
{
    public class BlogConverter : BaseService
    {
        public BlogDTO EntityToDTO(Blog blog)
        {
            return new BlogDTO()
            {
                BlogID = blog.BlogID,
                AccountID = blog.AccountID,
                BlogTypeID = blog.BlogTypeID,
                BlogTypeName = dbContext.BlogType.FirstOrDefault(x => x.BlogTypeID == blog.BlogTypeID).BlogTypeName,
                FullName = dbContext.Account.FirstOrDefault(x => x.AccountID == blog.AccountID).FullName,
                Title = blog.Title,
                Content = blog.Content,
                Image = blog.Image,
                CreateAt = blog.CreateAt,
            };
        }
    }
}
