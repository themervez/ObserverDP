using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ObserverDP.DAL;
using ObserverDP.Models;
using ObserverDP.ObserverDesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverDP.Controllers
{
    public class DefaultController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserObserverSubject _userObserver;

        public DefaultController(UserManager<AppUser> userManager, UserObserverSubject userObserver)
        {
            _userManager = userManager;
            _userObserver = userObserver;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterViewModel model)
        {
            var appUser = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            if(result.Succeeded)
            {
                _userObserver.NotifyObserver(appUser);//Triggered

                ViewBag.message = "Üyelik Başarılı Bir Şekilde Oluşturuldu.";
            }
            else
            {
                ViewBag.message = "Üyelik Kaydında Bir Hata Oluştu!";
            }
            return View();
        }
    }
}
