using System;
using System.Text;
using CoreServices.Interfaces;
using DataRepositories;
using ServiceModel;

namespace CoreServices.Impl {
    public class ThanksMailSenderService : IJobService {

        public IEmailSendService EmailSendService { get; set; }
        public IMailDataRepositories MailDataRepositories { get; set; }
        public ThanksMailSenderService(IMailDataRepositories mailDataRepositories, IEmailSendService emailSendService) {
            MailDataRepositories = mailDataRepositories;
            EmailSendService = emailSendService;
        }
        public void Execute(object contract) {
            try {
                var mailNeedToSendUsers = MailDataRepositories.GetUsersToSendThankYouMail(contract.ToString());

                if (mailNeedToSendUsers != null) {
                    var emailModel = new EmailServiceModel {
                        FromAddress = "sender@gmail.com",
                        Message = GetMailBody(mailNeedToSendUsers.Name),
                        Subject = "Thank you for your participation",
                        ToAddress = mailNeedToSendUsers.MailId
                    };
                    EmailSendService.SendEmail(emailModel);
                    MailDataRepositories.UpdateEmailHistory(contract.ToString());
                }

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        private string GetMailBody(string userName) {
            var sb = new StringBuilder();
            sb.AppendLine("Hello " + userName + ",");
            sb.AppendLine("Thank you to complete the steps. ");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Thanks ");
            return sb.ToString();
        }
    }
}
