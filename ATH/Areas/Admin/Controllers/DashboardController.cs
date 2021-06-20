using ATH.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ATH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        [TrackExecutionTime]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TestEmail ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TestEmail(string reciever, string subject, string message)
        {

            if (String.IsNullOrWhiteSpace(reciever) || String.IsNullOrWhiteSpace(subject) || String.IsNullOrWhiteSpace(message))
            {

                ViewBag.Error = "Please provide all inputs";
                return View("TestEmail");
            }
            

            var senderEmail = new MailAddress("722713744f-580406@inbox.mailtrap.io", "Mail Trip");
            var receiverEmail = new MailAddress(reciever, "Default Reciever");

            var smtp = new SmtpClient
            {
                Host = "smtp.mailtrap.io",
                Port = 2525,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("afdc804c1aa7b6", "ea66fcfa2d401b")
            };

            using ( var mailMessage = new MailMessage(senderEmail, receiverEmail) { Subject = subject, Body = message } )
            {
                smtp.Send(mailMessage);
            }


            ViewBag.Success = "Mail send successfully";
            return View("TestEmail");
        }


    }







}