using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.IServices;
using ThucTap.Payloads.Converters;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly ResponseObject<UserDTO> responseObject;
        private readonly UserConverter converter;

        public UserService()
        {
            responseObject = new ResponseObject<UserDTO>();
            converter = new UserConverter();
        }

        public ResponseObject<UserDTO> AddUser(AddUserRequest request)
        {
            if (!dbContext.Account.Any(x => x.AccountID == request.AccountID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Tài khoản không tồn tại", null);
            User user = new User();
            user.UserName = request.UserName;
            user.Phone = request.Phone;
            user.Address = request.Address;
            user.AccountID = request.AccountID;
            user.CreatedAt = DateTime.Now;
            user.UpdateAt = DateTime.Now;
            user.AccountID = request.AccountID;
            dbContext.Add(user);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Thêm thông tin người dùng thành công", converter.EntityToDTO(user));
        }

        public ResponseObject<UserDTO> DeleteUser(int id)
        {
            var user = dbContext.User.FirstOrDefault(x => x.UserID == id);
            if (user == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
            dbContext.Remove(user);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xoá thông tin người dùng thành công", converter.EntityToDTO(user));
        }

        public List<User> GetAll()
        {
            var listUser = dbContext.User.ToList();
            return listUser;
        }

        public ResponseObject<UserDTO> GetByID(int id)
        {
            var user = dbContext.User.FirstOrDefault(x => x.UserID == id);
            if (user == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
            else
                return responseObject.ResponseSucess("Thông tin người dùng", converter.EntityToDTO(user));
        }

        public ResponseObject<UserDTO> UpdateUser(UpdateUserRequest request)
        {
            var user = dbContext.User.FirstOrDefault(x => x.UserID == request.UserID);
            if (user == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
            if (!dbContext.Account.Any(x => x.AccountID == request.AccountID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Tài khoản không tồn tại", null);
            user.UserName = request.UserName;
            user.Phone = request.Phone;
            user.Address = request.Address;
            user.AccountID = request.AccountID;
            user.UpdateAt = DateTime.Now;
            user.AccountID = request.AccountID;
            dbContext.Update(user);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Sửa thông tin người dùng thành công", converter.EntityToDTO(user));
        }
    }
}
