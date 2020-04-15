using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Accounts
{
    public class TikTokAccount : Account
    {
        public string _growsLikes { get; set; }       

        public string _growsFollowers { get; set; }

        [NotMapped]
        public Dictionary<int, string> GrowsLikes
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<int, string>>
                  (string.IsNullOrEmpty(_growsLikes) ? "{}" : _growsLikes);
            }
            set
            {
                _growsLikes = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }       

        [NotMapped]
        public Dictionary<int, string> GrowsFollowers
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<int, string>>
                  (string.IsNullOrEmpty(_growsFollowers) ? "{}" : _growsFollowers);
            }
            set
            {
                _growsFollowers = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }

    }
}
