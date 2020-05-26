using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram
{
    //Response from Instagram API 
    public class InstaResponseUserInfo
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
        public bool Is_error { get; set; }

        public int Max_comments { get; set; }
        public string Max_comments_pic { get; set; }
        public int Min_comments { get; set; }
        public string Min_comments_pic { get; set; }
        public int Max_likes { get; set; }
        public string Max_likes_pic { get; set; }
        public int Min_likes { get; set; }
        public string Min_likes_pic { get; set; }

        public List<int> LikesList { get; set; }

        public List<int> CommentsList { get; set; }
    }
}
