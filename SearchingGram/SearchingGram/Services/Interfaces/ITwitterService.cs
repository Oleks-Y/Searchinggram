using SearchingGram.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;

namespace SearchingGram.Services
{
    public interface ITwitterService : INetService
    {
        public TweeterResponseUserInfo GetInfo(string name);
    }
}
