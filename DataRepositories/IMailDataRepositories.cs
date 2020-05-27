using MailSchedulerModels;
using System;
using System.Collections.Generic;
using ServiceModel;

namespace DataRepositories
{
   public interface IMailDataRepositories
    {

        List<User> GetUsersToSendPrimaryMail();
        void SaveEmailSentHistory(User user, Guid guid);
        List<UserEmailServiceModel> GetUsersToSendFollowUpMail();

    }
}
