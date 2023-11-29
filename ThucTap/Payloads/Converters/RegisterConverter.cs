using ThucTap.Context;
using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;

namespace ThucTap.Payloads.Converters
{
    public class RegisterConverter:BaseService
    {
        public RegisterDTO EntityToDTO(Account account)
        {
            return new RegisterDTO()
            {
                UserName = account.UserName,
                //Avatar = account.Avatar,
                AuthorityName = dbContext.Decentralization.FirstOrDefault(x => x.DecentralizationID == account.DecentralizationID).AuthorityName,
                Email = account.Email,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
