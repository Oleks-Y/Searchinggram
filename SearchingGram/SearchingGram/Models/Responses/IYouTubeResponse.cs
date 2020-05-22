using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Responses
{
    public interface IYouTubeResponse
    {
        public string Name{ get; set; }
        public string ChanelId { get; set; }

        public string Subscribers { get; set; }

        public string Views { get; set; }

        public Dictionary<string, ulong?> GrowViews { get; set; }

        public List<ulong> ViewsList { get; set; }
        public List<ulong> Likes { get; set; }
        public List<ulong> Dislikes { get; set; }
        public List<ulong> CommentsCounts { get; set; }

        public List<string> VideoNames { get; set; }

        public ulong? MostLiked { get; set; }
        public ulong? MostDisliked { get; set; }
    }
}
