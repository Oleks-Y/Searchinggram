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
       
        public string ScreenName { get; set; }

        public int FollowerCount { get; set; }

        public int RetweetsCount { get; set; }

        public int MaxRetweets { get; set; }

        public int MinRetweets { get; set; }

        public string MaxRetweets_Text { get; set; }

        public string MinRetweets_Text { get; set; }
        public string _retweetsList { get; set; }

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
        [NotMapped]
        public List<int> RetweetsList {
            get { return _retweetsList.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList(); }
            set { _retweetsList = string.Join(";", value); }
        }
    }
}
