using System.Numerics;
using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;

namespace ThucTap.Payloads.Converters
{
    public class UserConverter : BaseService
    {
        public UserDTO EntityToDTO(User user)
        {
            return new UserDTO()
            {
                UserName = user.UserName,
                Phone = user.Phone,
                Address = user.Address,
                AccountName = dbContext.Account.FirstOrDefault(x => x.AccountID == user.AccountID).UserName
            };
        }
    }
}
