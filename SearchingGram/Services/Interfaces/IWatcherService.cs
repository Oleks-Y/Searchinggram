using SearchingGram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Services
{
    interface IWatcherService
    {
        void SetNewUser(User user);
    }
}
