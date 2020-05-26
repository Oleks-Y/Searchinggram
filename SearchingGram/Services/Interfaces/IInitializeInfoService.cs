using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Services.Interfaces
{
    public interface IInitializeInfoService
    {
        Task InitInfoInsta(string name);

        Task InitInfoYouTube(string name);
        Task InitInfoTwitter(string name);
    }
}
