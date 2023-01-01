using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MimeKit;
using ObserverDP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverDP.ObserverDesignPattern
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin Observer", "net.gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAddressTo= new MailboxAddress("User", appUser.Email);
            mimeMessage.To.Add(mailboxAddressTo);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Observer Design Pattern Example Registration Discount|Discount Code:GIFT001";
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Registration Discount";
            SmtpClient client = new SmtpClient();//using MailKit.Net.Smtp;
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("net.gmail.com", "asdfghjklplghjk");//gmail address and api key
            client.Send(mimeMessage);
            client.Disconnect(true);
            logger.LogInformation($"{ appUser.Name + " " + appUser.Surname} isimli kullanıcının {appUser.Email} adlı e-mail adresine indirim kodu başarıyla gönderilmiştir.");

        }
    }
}
