using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Minerva.Web.Controllers
{
    public class JoinUsController : Controller
    {
        readonly IAmazonSimpleEmailService amazonSimpleEmailService;

        public JoinUsController(IAmazonSimpleEmailService amazonSimpleEmailService)
        {
            this.amazonSimpleEmailService = amazonSimpleEmailService;
        }

        // GET: /<controller>/
        [Route("/Contact/JoinUs")]
        public IActionResult JoinUs(Models.JoinUsModel model)
        {
            const string from = "MINERVAKITAPKULUBU@GMAIL.COM";
            const string to = "MINERVAKITAPKULUBU@GMAIL.COM";

            var destination = new Destination()
            {
                ToAddresses = new List<string>() { to }
            };

            var subject = new Content("Bize Katılın Formu");

            var content = new StringBuilder();
            content.AppendLine($"Ad: {model.FirstName}");
            content.AppendLine($"Soyad: {model.LastName}");
            content.AppendLine($"Doğum Tarihi: {model.BirthDateDay}.{model.BirthDateMonth}.{model.BirthDateYear}");
            content.AppendLine($"Meslek: {model.Occupation}");
            content.AppendLine($"Mail: {model.Email}");
            content.AppendLine($"GSM: {model.Gsm}");
            content.AppendLine($"Okuduğunuz Son Üç Kitap (1): {model.LastBook1}");
            content.AppendLine($"Okuduğunuz Son Üç Kitap (2): {model.LastBook2}");
            content.AppendLine($"Okuduğunuz Son Üç Kitap (3): {model.LastBook3}");
            content.AppendLine($"Favori Kitabınız: {model.FavoriteBook}");
            content.AppendLine($"Favori Kitabınız (Neden): {model.FavoriteBookReason}");
            content.AppendLine($"Okumayı Sevdiğiniz Tür (1): {model.PreferedType1}");
            content.AppendLine($"Okumayı Sevdiğiniz Yazar (1): {model.PreferedAuthor1}");
            content.AppendLine($"Okumayı Sevdiğiniz Tür (2): {model.PreferedType2}");
            content.AppendLine($"Okumayı Sevdiğiniz Yazar (2): {model.PreferedAuthor2}");
            content.AppendLine($"Neden Kitap Kulübü: {model.WhyBookClub}");
            content.AppendLine($"Eklemek İstedikleriniz: {model.Extra}");

            var textBody = new Content(content.ToString());
            var body = new Body(textBody);

            var message = new Message(subject, body);

            var request = new SendEmailRequest(from, destination, message);

            try
            {
                amazonSimpleEmailService.SendEmailAsync(request).Wait();
                return Content("Done!");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        [Route("/Contact")]
        public IActionResult ContactUs(string Name, string Email, string Phone, string Message)
        {
            const string from = "MINERVAKITAPKULUBU@GMAIL.COM";
            const string to = "MINERVAKITAPKULUBU@GMAIL.COM";

            var destination = new Destination()
            {
                ToAddresses = new List<string>() { to }
            };

            var subject = new Content("Bize Ulaşın Formu");

            var content = new StringBuilder();
            content.AppendLine($"Ad: {Name}");
            content.AppendLine($"Mail: {Email}");
            content.AppendLine($"GSM: {Phone}");
            content.AppendLine($"Mesaj: {Message}");

            var textBody = new Content(content.ToString());
            var body = new Body(textBody);

            var message = new Message(subject, body);

            var request = new SendEmailRequest(from, destination, message);

            try
            {
                amazonSimpleEmailService.SendEmailAsync(request).Wait();
                return Content("Done!");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
