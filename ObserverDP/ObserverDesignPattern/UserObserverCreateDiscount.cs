using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ObserverDP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverDP.ObserverDesignPattern
{
    public class UserObserverCreateDiscount : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
                var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();
            var scoped = _serviceProvider.CreateScope();
            var context = scoped.ServiceProvider.GetRequiredService<Context>();//ServiceProvider üzerinden Context'e erişim sağlandı
            context.Discounts.Add(new Discount
            {
                Rate=25,
                UserId=appUser.Id
            });
            context.SaveChanges();
            logger.LogInformation($"Yeni kayıt olan kullanıcı:{appUser.Name + " " + appUser.Surname} için %25 oranında bir indirim kodu tanımlandı.");          
        }
    }
}
