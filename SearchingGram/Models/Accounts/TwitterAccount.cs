using Newtonsoft.Json;
using SearchingGram.Models.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Accounts
{
    public class TwitterAccount : Account, ITwitterResponse
    {
        public string Pic { get; set; }
        public string _growsRetweets { get; set; }

        public string _growsFollowers { get; set; }

        [NotMapped]
        public Dictionary<string, int> GrowsRetweets
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, int>>
                  (string.IsNullOrEmpty(_growsRetweets) ? "{}" : _growsRetweets);
            }
            set
            {
                _growsRetweets = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }

        [NotMapped]
        public Dictionary<string, int> GrowsFollowers
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, int>>
                  (string.IsNullOrEmpty(_growsFollowers) ? "{}" : _growsFollowers);
            }
            set
            {
                _growsFollowers = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }
    }
}
