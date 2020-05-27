using CoreServices.Interfaces;
using ServiceModel;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreServices.Impl {
    public class EmailSendService : IEmailSendService {
        public async void SendEmail(EmailServiceModel emailServiceModel) {
            try {
                var mail = new MailMessage();
                var SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(emailServiceModel.FromAddress);//need to change
                mail.To.Add(emailServiceModel.ToAddress);
                mail.Subject = emailServiceModel.Subject;
                mail.Body = emailServiceModel.Message;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 25;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("sender@gmail.com", "priyanka12?1", "smtp.gmail.com");//need to change
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                mail.IsBodyHtml = false;

               await Task.Factory.StartNew(()=> SmtpServer.Send(mail));
               
            } catch (Exception) {
               
            }
        }
    }
}
