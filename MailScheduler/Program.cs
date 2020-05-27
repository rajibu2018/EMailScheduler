using CoreServices.Impl;
using CoreServices.Interfaces;
using DataRepositories;
using ServiceModel;
using System;

namespace MailScheduler {
    class Program {


        static void Main(string[] args) {
            var MailDataRepositories = new MailDataRepositories();
            var EmailSendService = new EmailSendService();
            IFactoryServicecs factoryServicecs = new FactoryServicecs(MailDataRepositories, EmailSendService);

            var executionTime = new TimeSpan(0, 0, 0);
            var firstAttemptOfDay = true;
            while (true) {
                if (DateTime.Now.TimeOfDay.Hours == executionTime.Hours && DateTime.Now.TimeOfDay.Minutes == executionTime.Minutes) {
                    if (firstAttemptOfDay) {
                        firstAttemptOfDay = false;
                        factoryServicecs.GetService(ServiceType.ReminderMailSend).Execute();
                        factoryServicecs.GetService(ServiceType.PrimaryMailSend).Execute();
                    }
                } else {
                    firstAttemptOfDay = true;
                }
            }
        }


    }


}
