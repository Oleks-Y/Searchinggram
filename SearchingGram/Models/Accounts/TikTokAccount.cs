using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Accounts
{
    public class TikTokAccount : Account
    {
        public List<int> FollowersGrows { get; set; }

        public List<int> LikesGrows { get; set; }
    }
}
