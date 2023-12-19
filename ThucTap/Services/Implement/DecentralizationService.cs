using ThucTap.Services.IServices;
using ThucTap.Entities;

namespace ThucTap.Services.Implement
{
    public class DecentralizationService : BaseService, IDecentralizationService
    {
        public List<Decentralization> GetAll()
        {
            var list = dbContext.Decentralization.ToList();
            return list;
        }
    }
}
