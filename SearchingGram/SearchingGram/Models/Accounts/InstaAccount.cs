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
    public class InstaAccount : Account, IInstaResponse
    {
        public string Pic { get; set; }

        public string Biography { get; set; }
        public string Business_category_name { get; set; }
        public int Comments { get; set; }
        public int Follow { get; set; }
        public int Followers { get; set; }
        public string Full_name { get; set; }
        public bool Is_business_account { get; set; }
        public int Likes { get; set; }
        

        public int Max_comments { get; set; }
        public string Max_comments_pic { get; set; }
        public int Min_comments { get; set; }
        public string Min_comments_pic { get; set; }
        public int Max_likes { get; set; }
        public string Max_likes_pic { get; set; }
        public int Min_likes { get; set; }
        public string Min_likes_pic { get; set; }
        public string _growsLikes { get; set; }

        public string _growsComments { get; set; }

        public string _growsFollowers{ get; set; }

        public string _likesList { get; set; }

        public string _commentsList { get; set; }

        [NotMapped]
        public Dictionary<string ,int > GrowsLikes
        {
            get { return JsonConvert.DeserializeObject<Dictionary<string, int>>
                    (string.IsNullOrEmpty(_growsLikes) ? "{}" : _growsLikes); }
            set
            {
                _growsLikes = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }        

        [NotMapped]
        public Dictionary<string, int> GrowsComments
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, int>>
                  (string.IsNullOrEmpty(_growsComments) ? "{}" : _growsComments);
            }
            set
            {
                _growsComments = JsonConvert.SerializeObject(value, Formatting.Indented);
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
        public List<int> LikesList {
            get { return _likesList.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList(); }
            set { _likesList = string.Join(";", value); } 
        }

        [NotMapped]
        public List<int> CommentsList {
            get { return _commentsList.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList(); }
            set { _commentsList = string.Join(";", value); } 
        }







    }
}
