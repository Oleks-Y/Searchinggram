using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Services
{
    public interface INetService
    {
         string URL { get; set; }

        string GetInfo(string name);


        bool IsUserExist(string name);
        
    }
}
