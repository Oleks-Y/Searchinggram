using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Responses
{
    public interface ITwitterResponse 
    {
        public string Pic { get; set; }
        public string Name { get; set; }

        public Dictionary<string, int> GrowsRetweets { get; set; }
        public Dictionary<string, int> GrowsFollowers { get; set; }

        public string ScreenName { get; set; }

        public int FollowerCount { get; set; }

        public int RetweetsCount { get; set; }

        public int MaxRetweets { get; set; }

        public int MinRetweets { get; set; }

        public string MaxRetweets_Text { get; set; }

        public string MinRetweets_Text { get; set; }
    }
}
