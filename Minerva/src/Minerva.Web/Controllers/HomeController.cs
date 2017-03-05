using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Mvc;
using Minerva.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minerva.Web.Controllers
{
    public class HomeController : CommonController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestEmail()
        {
            const string from = "MINERVAKITAPKULUBU@GMAIL.COM";
            const string to = "MINERVAKITAPKULUBU@GMAIL.COM";


            var destination = new Destination()
            {
                ToAddresses = new List<string>() { to }
            };

            var subject = new Content("AWS Email Test");
            var textBody = new Content("Hey if you got this mail u did it Yilmaz... U rly did it. Lets have a beer :D");
            var body = new Body(textBody);

            var message = new Message(subject, body);

            var request = new SendEmailRequest(from, destination, message);
            var client = new AmazonSimpleEmailServiceClient(Amazon.RegionEndpoint.USWest2);

            try
            {
                client.SendEmailAsync(request).Wait();
                return Content("Done!");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
