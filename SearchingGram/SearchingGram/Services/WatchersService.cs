using SearchingGram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Services
{
    public class WatchersService : IWatcherService
    {
        public User CurrentUser { get; set; }

        public void SetNewUser(User user)
        {
            CurrentUser = user;
        }



    }
}
