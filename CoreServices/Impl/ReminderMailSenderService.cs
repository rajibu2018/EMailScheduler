using System;
using System.Text;
using CoreServices.Interfaces;
using DataRepositories;
using ServiceModel;

namespace CoreServices.Impl {
    public class ReminderMailSenderService : IJobService {

        public IEmailSendService EmailSendService { get; set; }
        public IMailDataRepositories MailDataRepositories { get; set; }
        public ReminderMailSenderService(IMailDataRepositories mailDataRepositories, IEmailSendService emailSendService) {
            MailDataRepositories = mailDataRepositories;
            EmailSendService = emailSendService;
        }
        public void Execute() {
            try {
                var mailNeedToSendUsers = MailDataRepositories.GetUsersToSendFollowUpMail();
                foreach (var user in mailNeedToSendUsers) {
                    var emailModel = new EmailServiceModel {
                        FromAddress = "priyagh945@gmail.com",
                        Message = GetMailBody(user.Name,user.LinkUID),
                        Subject = "Important Message for you from CusJo",
                        ToAddress = user.MailId
                    };
                    EmailSendService.SendEmail(emailModel);

                    
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        private string GetMailBody(string userName, Guid guid) {
            var sb = new StringBuilder();
            sb.AppendLine("Hello " + userName + ",");
            sb.AppendLine("We notice that, you missed our previous mail. ");
            sb.AppendLine("Please click on the below important link ");
            sb.AppendLine("https://cusjo.cusjo.com/" + guid);
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Thanks ");
            return sb.ToString();
        }
    }
}
