using ServiceModel;

namespace CoreServices.Interfaces {
    
    public interface IEmailSendService
    {
       void SendEmail(EmailServiceModel emailServiceModel);
    }
}
