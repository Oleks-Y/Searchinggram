using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Responses
{
    //Response from YouTube API 
    public class YouTubeResponseUserInfo
    {
        public string Name { get; set; }

        public string ChanelId { get; set; }

        public string Subscribers { get; set; }

        public ulong Views { get; set; }

        public List<ulong> ViewsList { get; set; }

        public List<ulong> Likes { get; set; }

        public List<ulong> Dislikes { get; set; }

        public ulong MostLiked { get; set; }

        public ulong MostDisliked { get; set; }

        public string MostPopularVideos { get; set; }

        public List<ulong> CommentsCounts { get; set; }

        public ulong VideosCount { get; set; } 

        public List<string> VideoNames { get; set; }
    }
}
