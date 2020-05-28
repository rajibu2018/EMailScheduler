using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreServices.Impl;
using CoreServices.Interfaces;
using DataRepositories;
using Microsoft.AspNetCore.Mvc;
using ServiceModel;

namespace MailViewWebApp.Controllers {
    public class MailViewController : Controller {
        public IMailDataRepositories MailDataRepositories { get; set; }
        public IEmailSendService EmailSendService { get; set; }
        public IFactoryServicecs FactoryServicecs { get; set; }

        public MailViewController() {
            MailDataRepositories = new MailDataRepositories();
            EmailSendService = new EmailSendService();
            FactoryServicecs = new FactoryServicecs(MailDataRepositories, EmailSendService);
        }

        [Route("~/checkmail/{guid}")]
        public IActionResult CheckEmail(string guid) {
            // link sent to email http://localhost:49236/checkmail/guid
            FactoryServicecs.GetService(ServiceType.ThanksMailSend).Execute(guid);
            return View();
        }
    }
}