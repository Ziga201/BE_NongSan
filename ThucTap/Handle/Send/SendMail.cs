using System.Net;
using System.Net.Mail;



namespace ThucTap.Handle.Send
{
    public class SendMail
    {
        public static void Send(MailContent content)
        {
            //var smtpClient = new SmtpClient("smtp.ethereal.email")
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("nametung1@gmail.com", "ctffyirfjdivdyeu"),
                //Credentials = new NetworkCredential("lyric55@ethereal.email", "gZqJRrnSDqAHHXaUH7"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("nametung1@gmail.com"),
                Subject = content.Subject,
                Body = content.Content,
                IsBodyHtml = true
            };
            mailMessage.To.Add(content.MailTo);
            smtpClient.Send(mailMessage);
        }
    }
}
