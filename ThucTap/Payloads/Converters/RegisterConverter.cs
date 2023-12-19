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
                AccountID = account.AccountID,
                UserName = account.UserName,
                Password = account.Password,
                Avatar = account.Avatar,
                Email = account.Email,
                Status = account.Status,
                AuthorityName = dbContext.Decentralization.FirstOrDefault(x => x.DecentralizationID == account.DecentralizationID).AuthorityName,
                CreatedAt = DateTime.UtcNow,
            };
        }

    }
}
