using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Accounts
{
    public class InstaAccount : Account
    {
       public string _growsLikes { get; set; }

        public string _growsComments { get; set; }

        public string _growsFollowers{ get; set; }

        [NotMapped]
        public Dictionary<int ,int > GrowsLikes
        {
            get { return JsonConvert.DeserializeObject<Dictionary<int, int>>
                    (string.IsNullOrEmpty(_growsLikes) ? "{}" : _growsLikes); }
            set
            {
                _growsLikes = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }        

        [NotMapped]
        public Dictionary<int, int> GrowsComments
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<int, int>>
                  (string.IsNullOrEmpty(_growsComments) ? "{}" : _growsComments);
            }
            set
            {
                _growsComments = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }

        [NotMapped]
        public Dictionary<int, int> GrowsFollowers
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<int, int>>
                  (string.IsNullOrEmpty(_growsFollowers) ? "{}" : _growsFollowers);
            }
            set
            {
                _growsFollowers = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }







    }
}
