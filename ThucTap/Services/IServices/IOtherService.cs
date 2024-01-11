using ThucTap.Entities;
using ThucTap.Payloads.Requests.Other;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services.IServices
{
    public interface IOtherService
    {
        ResponseObject<Message> SendMessage(MessageRequest request);
        List<Message> GetAll();
    }
}
