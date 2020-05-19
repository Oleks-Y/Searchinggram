using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Responses
{
    public class TweeterResponseUserInfo
    {
        public string Pic { get; set; }
        public string ScreenName { get; set; }

        public int FollowerCount { get; set; }

        public int RetweetsCount { get; set; }


    }
}
