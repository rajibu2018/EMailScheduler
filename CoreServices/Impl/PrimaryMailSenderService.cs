using CoreServices.Interfaces;
using DataRepositories;
using ServiceModel;
using System;
using System.Text;

namespace CoreServices.Impl {

    public class PrimaryMailSenderService : IJobService {

        public IMailDataRepositories MailDataRepositories { get; set; }
        public IEmailSendService EmailSendService { get; set; }

        public PrimaryMailSenderService(IMailDataRepositories mailDataRepositories, IEmailSendService emailSendService) {
            MailDataRepositories = mailDataRepositories;
            EmailSendService = emailSendService;
        }
        public void Execute() {
            try {
                var mailNeedToSendUsers = MailDataRepositories.GetUsersToSendPrimaryMail();
                foreach (var user in mailNeedToSendUsers) {
                    var guid = Guid.NewGuid();
                    var emailModel = new EmailServiceModel {
                        FromAddress = "sender@gmail.com",
                        Message = GetMailBody(user.Name, guid),
                        Subject = "Important Message for you from CusJo",
                        ToAddress = user.MailId
                    };
                    EmailSendService.SendEmail(emailModel);

                    // save email sent history
                    MailDataRepositories.SaveEmailSentHistory(user, guid);
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        private string GetMailBody(string userName, Guid guid) {

            var sb = new StringBuilder();
            sb.AppendLine("Hello " + userName + ",");
            sb.AppendLine("Please click on the below important link ");
            sb.AppendLine("https://cusjo.cusjo.com/" + guid);
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Thanks ");
            return sb.ToString();
        }
    }




}
