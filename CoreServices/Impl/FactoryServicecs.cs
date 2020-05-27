using CoreServices.Interfaces;
using DataRepositories;
using ServiceModel;

namespace CoreServices.Impl {
    public class FactoryServicecs : IFactoryServicecs {
        public IMailDataRepositories MailDataRepositories { get; set; }
        public IEmailSendService EmailSendService { get; set; }

        public FactoryServicecs(IMailDataRepositories mailDataRepositories, IEmailSendService emailSendService) {
            MailDataRepositories = mailDataRepositories;
            EmailSendService = emailSendService;
        }
        public IJobService GetService(ServiceType serviceType) {
            switch (serviceType) {
                case ServiceType.PrimaryMailSend:
                    return new PrimaryMailSenderService(MailDataRepositories, EmailSendService);
                case ServiceType.ReminderMailSend:
                    return new ReminderMailSenderService(MailDataRepositories, EmailSendService);
                default:return  null;                    
            }
           
        }
    }
}
