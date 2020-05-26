using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using SearchingGram.Responses;
using SearchingGram.Services.Interfaces;

namespace SearchingGram.Services
{
    public class You_TubeService : IYou_TubeService
    {
        //Service for YouTube API 
        public string URL { get; set ; }

        public  bool IsUserExist(string Name)
        {

            var service = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyA1OI7D6StmNKDg4Fr4-j0ve27RFsbhxOs",
                ApplicationName = "My Project 55986",

            });

            var search = service.Search.List("snippet");
            search.Q = Name;
            search.Type = "channel";
            var result = search.Execute();

            if (result.Items == null || result.Items.Count == 0)
            {
                return false;
            }
            return true;
        }

        public  YouTubeResponseUserInfo GetInfo(string Name)
        {
            var Acc = new YouTubeResponseUserInfo();
            var service = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyA1OI7D6StmNKDg4Fr4-j0ve27RFsbhxOs",
                ApplicationName = "My Project 55986",

            });

            var search = service.Search.List("snippet");

            search.Q = Name;
            search.Type = "channel";
            var result = search.Execute();

            var id = result.Items.First().Id.ChannelId;
            //GetChannel
            var chanelSearch = service.Channels.List("snippet, contentDetails,statistics");

            chanelSearch.Id = id;
            //chanelSearch.ForUsername = Name;

            var channelResult = chanelSearch.Execute();

            var channel = channelResult.Items.First();
            Acc.Name = Name;

            Acc.Subscribers = channel.Statistics.SubscriberCount.ToString();
            Acc.Views = (ulong)channel.Statistics.ViewCount;
            Acc.VideosCount = (ulong)channel.Statistics.VideoCount;


            //GetVideo
            search = service.Search.List("snippet");

            search.ChannelId = id;
            search.Type = "video";

            result = search.Execute();

            var videoIds = new List<string>();
            var names = new List<string>();
            var likes = new List<ulong>();
            var disLikes = new List<ulong>();
            var views = new List<ulong>();
            var comments = new List<ulong>();

            foreach (var v in result.Items)
            {
                videoIds.Add(v.Id.VideoId);
            }
            ulong? mostLiked = 0;
            ulong? mostDisLiked = 0;
            foreach (var vid in videoIds)
            {
                var videoSearch = service.Videos.List("contentDetails, statistics, snippet");
                videoSearch.Id = vid;
                var video = videoSearch.Execute().Items.First();
                names.Add(video.Snippet.Title);
                likes.Add((ulong)video.Statistics.LikeCount);
                disLikes.Add((ulong)video.Statistics.DislikeCount);
                views.Add((ulong)video.Statistics.ViewCount);
                comments.Add((ulong)video.Statistics.CommentCount);
                if(mostLiked< video.Statistics.LikeCount)
                {
                    mostLiked = video.Statistics.LikeCount;
                }
                if(mostDisLiked< video.Statistics.DislikeCount)
                {
                    mostDisLiked = video.Statistics.DislikeCount;
                }

            }

            Acc.VideoNames = names;
            Acc.Likes = likes;
            Acc.Dislikes = disLikes;
            Acc.ViewsList = views;
            Acc.CommentsCounts = comments;
            Acc.MostLiked = (ulong)mostLiked;
            Acc.MostDisliked = (ulong)mostDisLiked;

            return Acc;

        }

       
    }
}
