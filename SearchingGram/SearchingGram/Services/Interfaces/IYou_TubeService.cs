using SearchingGram.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Services.Interfaces
{
    public interface IYou_TubeService : INetService
    {
        public YouTubeResponseUserInfo GetInfo(string Name);
    }
}
