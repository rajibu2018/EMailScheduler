using CoreServices.Interfaces;
using ServiceModel;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreServices.Impl {
    public class EmailSendService : IEmailSendService {
        public void SendEmail(EmailServiceModel emailServiceModel) {
            try {
                var mail = new MailMessage();
                var SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("rajibu2003@gmail.com");
                mail.To.Add(emailServiceModel.ToAddress);
                mail.Subject = emailServiceModel.Subject;
                mail.Body = emailServiceModel.Message;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 25;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("rajibu2003@gmail.com", "priya@89", "smtp.gmail.com");//need to change
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = true;
                mail.IsBodyHtml = false;
                SmtpServer.SendAsync(mail, emailServiceModel.Subject);

            } catch (Exception) {               
            }
        }
    }
}
