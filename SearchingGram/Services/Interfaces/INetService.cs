using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Services
{
    public interface INetService
    {
         public string URL { get; set; }

        


        bool IsUserExist(string name);
        
    }
}
