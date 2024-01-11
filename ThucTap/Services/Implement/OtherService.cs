using CloudinaryDotNet;
using System;
using ThucTap.Entities;
using ThucTap.Handle.Send;
using ThucTap.Payloads.Requests.Other;
using ThucTap.Payloads.Responses;
using ThucTap.Services.IServices;

namespace ThucTap.Services.Implement
{
    public class OtherService : BaseService, IOtherService
    {
        private readonly ResponseObject<Message> responseObject;
        public OtherService()
        {
            responseObject = new ResponseObject<Message>();
        }

        public List<Message> GetAll()
        {
            return dbContext.Message.ToList();
        }

        public ResponseObject<Message> SendMessage(MessageRequest request)
        {
            var account = dbContext.Account.FirstOrDefault(x => x.AccountID == request.AccountID);
            if (account == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Tài khoản không tồn tại", null);
            Message message = new Message();
            message.AccountID = request.AccountID;
            message.Name = request.Name;
            message.Email = request.Email;
            message.Topic = request.Topic;
            message.Content = request.Content;
            dbContext.Add(message);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Gửi lời nhắn thành công", message);

        }
    }
}
