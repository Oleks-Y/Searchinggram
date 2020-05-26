using SearchingGram.Data;
using SearchingGram.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Core.Extensions;

namespace SearchingGram.Services
{
    public class RefreshInfoService : IRefreshInfoService
    {
        public WatcherDbContext _WDb;
        public readonly IInstaService _instaService;
        public readonly ITwitterService _twitterService;
        public readonly ITikTokService _tikTokService;

        public readonly IYou_TubeService _you_TubeService;

        public RefreshInfoService(WatcherDbContext wdb, IInstaService instaService, ITwitterService twitterService, ITikTokService tikTokService, ITimerService timer, IYou_TubeService you_TubeService)
        {
            _WDb = wdb;
            _instaService = instaService;
            _twitterService = twitterService;
            _tikTokService = tikTokService;
            _you_TubeService = you_TubeService;
        }

        public async Task RefreshAccountsInfo()
        {
            
                
             _WDb.InstaAccounts.ForEach(c =>
            {
                var res = _instaService.GetInfo(c.Name);

                var currentInfoLikesDict = c.GrowsLikes;
                currentInfoLikesDict[DateTime.Now.ToString()] = res.Likes;
                c.GrowsLikes = currentInfoLikesDict;


                var currentInfoCommentsDict = c.GrowsComments;
                currentInfoCommentsDict[DateTime.Now.ToString()] = res.Comments;
                c.GrowsComments = currentInfoCommentsDict;
                //_WDb.SaveChanges();

                var currentInfoFollowersDict = c.GrowsFollowers;
                currentInfoFollowersDict[DateTime.Now.ToString()] = res.Followers;
                c.GrowsFollowers = currentInfoFollowersDict;

                var currentUserPic = res.Pic;
                c.Pic = currentUserPic;


                c.Comments = res.Comments;
               c.CommentsList = res.CommentsList;
                c.Followers = res.Followers;
                c.Likes = res.Likes;
                c.LikesList = res.LikesList;
                c.Max_comments = res.Max_comments;
                c.Max_comments_pic = res.Max_comments_pic;
                c.Max_likes = res.Max_likes;
                c.Max_likes_pic = res.Max_likes_pic;
                c.Min_comments = res.Min_comments;
                c.Min_comments_pic = res.Min_comments_pic;
                c.Min_likes = res.Min_likes;
                c.Min_likes_pic = res.Min_likes_pic;
            });
                        
                        
            _WDb.TwitterAccounts.ForEach(e =>
            {
                var res = _twitterService.GetInfo(e.Name);

                var currentInfoFollowersDict = e.GrowsFollowers;
                currentInfoFollowersDict[DateTime.Now.ToString()] = res.FollowerCount;
                e.GrowsFollowers = currentInfoFollowersDict;

                var CurrentRetweetsDict = e.GrowsRetweets;
                CurrentRetweetsDict[DateTime.Now.ToString()] = res.RetweetsCount;
                e.GrowsRetweets = CurrentRetweetsDict;

                var currentUserPic = res.Pic;
                e.Pic = currentUserPic;

                e.FollowerCount = res.FollowerCount;
                e.MaxRetweets = res.MaxRetweets;
                e.MaxRetweets_Text = res.MaxRetweets_Text;
                e.MinRetweets = res.MinRetweets;
                e.MinRetweets_Text = res.MinRetweets_Text;
                e.RetweetsCount = res.RetweetsCount;
                e.RetweetsList = res.RetweetsList;
            });
                       
            _WDb.YouTubeAccounts.ForEach(k =>
            {
                var res = _you_TubeService.GetInfo(k.Name);

                var currentViewsDict = k.GrowViews;
                currentViewsDict[DateTime.Now.ToString()] = res.Views;
                k.GrowViews = currentViewsDict;


                k.Likes = res.Likes;
                k.Dislikes = res.Dislikes;
                k.MostDisliked = res.MostDisliked;
                k.MostLiked = res.MostLiked;
                k.Subscribers = res.Subscribers;
                k.VideoNames = res.VideoNames;
                k.VideosCount = res.VideosCount.ToString();
                k.Views = res.Views.ToString();
                k.ViewsList = res.ViewsList;
            });
                
            
            await _WDb.SaveChangesAsync();
        }

    }
}
