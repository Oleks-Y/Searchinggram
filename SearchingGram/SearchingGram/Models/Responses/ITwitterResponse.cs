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


    }
}
