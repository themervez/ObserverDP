using ObserverDP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverDP.ObserverDesignPattern
{
    public interface IUserObserver
    {
        void CreateUser(AppUser appUser);
    }
}
