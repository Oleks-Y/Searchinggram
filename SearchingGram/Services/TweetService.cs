using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Tweetinvi;
using TweetSharp;
namespace SearchingGram.Services
{
    public class TweetService : ITwitterService
    {
        public string URL { get ; set ; }

        public TwitterService _service;
        public TweetService()
        {
            _service=new TwitterService("SyFjRNtGIXiHLS3k9hUWdFZbv", "XmeczmCV07Kde8n41BbIytzc3eIbSDdFGfRNf0mXou716dsHJN", "1153209111942762502-iqMo6JAksO2LjD3cYSFxDSMVsy9bnI", "rp8RZcvlTW4etlWDW1ALig3pLY7TBtGAlXTaqn8BFBKJE");
        }

        public string GetInfo(string name)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExist(string name)
        {
            var result = _service.SearchForUser(new SearchForUserOptions { Q = name });           
            if (result.ToJson()== "[]")
            {
                return false;
            }
            return true;
        }
    }
}
