using ThucTap.Context;

namespace ThucTap.Services
{
    public class BaseService
    {
        public readonly AppDbContext dbContext;

        public BaseService()
        {
            dbContext = new AppDbContext();
        }
    }
}
