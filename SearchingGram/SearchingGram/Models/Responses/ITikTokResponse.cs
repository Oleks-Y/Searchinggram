using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Responses
{
    public interface ITikTokResponse
    {
        public string Name { get; set; }
        public Dictionary<string, string> GrowsLikes { get; set; }
        public Dictionary<string, string> GrowsFollowers { get; set; }
    }
}
