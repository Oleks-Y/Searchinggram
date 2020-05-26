using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Responses
{
    //Response from Twitter API
    public class TweeterResponseUserInfo
    {
        public string Pic { get; set; }
        public string ScreenName { get; set; }

        public int FollowerCount { get; set; }

        public int RetweetsCount { get; set; }

        public int MaxRetweets { get; set; }

        public int MinRetweets { get; set; }

        public string MaxRetweets_Text { get; set; }

        public string MinRetweets_Text { get; set; }

        public List<int> RetweetsList { get; set; }



    }
}
