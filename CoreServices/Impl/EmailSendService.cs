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
                var SmtpServer = new SmtpClient();
                mail.From = new MailAddress(emailServiceModel.FromAddress);
                mail.To.Add(emailServiceModel.ToAddress);
                mail.Subject = emailServiceModel.Subject;
                mail.Body = emailServiceModel.Message;
                mail.IsBodyHtml = true;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
               
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailServiceModel.FromAddress, "password");//need to change
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.SendAsync(mail, emailServiceModel.Subject);

            } catch (Exception) {               
            }
        }
    }
}
