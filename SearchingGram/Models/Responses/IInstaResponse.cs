using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Responses
{
    public interface IInstaResponse 
    {
        public string Name { get; set; }

        public string Pic { get; set; }

        public Dictionary<string, int> GrowsLikes { get; set; }
        public Dictionary<string, int> GrowsComments { get; set; }
        public Dictionary<string, int> GrowsFollowers { get; set; }

    }
}
