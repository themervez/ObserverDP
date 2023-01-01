using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ObserverDP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverDP.ObserverDesignPattern
{
    public class UserObserverWriteToConsole : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;//

        public UserObserverWriteToConsole(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverWriteToConsole>>();
            logger.LogInformation($"{appUser.Name + " " + appUser.Surname} isimli kullanıcı sisteme kaydoldu.");//Kulllanıcı identity üzerinden kayıt işlemini yaptığında observer devreye girererek konsol ekranına bir log kaydı gönderecek
        }
    }
}
