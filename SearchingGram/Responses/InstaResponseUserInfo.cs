using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram
{
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
    }
}
