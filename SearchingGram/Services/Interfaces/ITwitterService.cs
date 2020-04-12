using SearchingGram.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Services
{
    public interface ITwitterService : INetService
    {
        public TikTokResponseUserInfo GetInfo(string name);
    }
}
