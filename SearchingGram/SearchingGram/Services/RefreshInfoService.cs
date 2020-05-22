using SearchingGram.Data;
using SearchingGram.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            foreach (var a in _WDb.Watchers)
            {
                //if (a.Monitors == null) continue;
                foreach (var b in _WDb.Monitors)
                {
                    if (b.WatcherId == a.Id)
                    {
                        foreach (var c in _WDb.InstaAccounts)
                        {
                            //if (c == null) continue;
                            if (c.MonitorOwnerId == b.Id)
                            {
                                //Перевірити на необхідність таких операцій!!!
                                var res = _instaService.GetInfo(c.Name);

                                var currentInfoLikesDict = c.GrowsLikes;
                                currentInfoLikesDict[DateTime.Now.ToString()] = res.Likes;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).GrowsLikes = currentInfoLikesDict;
                                //_WDb.SaveChanges();

                                var currentInfoCommentsDict = c.GrowsComments;
                                currentInfoCommentsDict[DateTime.Now.ToString()] = res.Comments;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).GrowsComments = currentInfoCommentsDict;
                                //_WDb.SaveChanges();

                                var currentInfoFollowersDict = c.GrowsFollowers;
                                currentInfoFollowersDict[DateTime.Now.ToString()] = res.Followers;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).GrowsFollowers = currentInfoFollowersDict;

                                var currentUserPic = res.Pic;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Pic = currentUserPic;


                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Comments = res.Comments;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).CommentsList = res.CommentsList;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Followers = res.Followers;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Likes = res.Likes;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).LikesList = res.LikesList;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Max_comments = res.Max_comments;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Max_comments_pic = res.Max_comments_pic;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Max_likes = res.Max_likes;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Max_likes_pic = res.Max_likes_pic;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Min_comments = res.Min_comments;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Min_comments_pic = res.Min_comments_pic;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Min_likes = res.Min_likes;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).Min_likes_pic = res.Min_likes_pic;




                            }

                        }
                        foreach (var e in _WDb.TwitterAccounts)
                        {
                            if (e.MonitorOwnerId == b.Id)
                            {
                                var res = _twitterService.GetInfo(e.Name);

                                var currentInfoFollowersDict = e.GrowsFollowers;
                                currentInfoFollowersDict[DateTime.Now.ToString()] = res.FollowerCount;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).GrowsFollowers = currentInfoFollowersDict;

                                var CurrentRetweetsDict = e.GrowsRetweets;
                                CurrentRetweetsDict[DateTime.Now.ToString()] = res.RetweetsCount;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).GrowsRetweets = CurrentRetweetsDict;

                                var currentUserPic = res.Pic;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).Pic = currentUserPic;

                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).FollowerCount = res.FollowerCount;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).MaxRetweets = res.MaxRetweets;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).MaxRetweets_Text = res.MaxRetweets_Text;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).MinRetweets = res.MinRetweets;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).MinRetweets_Text = res.MinRetweets_Text;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).RetweetsCount = res.RetweetsCount;
                                _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).RetweetsList = res.RetweetsList;

                            }
                        }
                        foreach (var k in _WDb.YouTubeAccounts)
                        {
                            if (k.MonitorOwnerId == b.Id)
                            {
                                var res = _you_TubeService.GetInfo(k.Name);

                                var currentViewsDict = k.GrowViews;
                                currentViewsDict[DateTime.Now.ToString()] = res.Views;
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).GrowViews = currentViewsDict;


                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).Likes = res.Likes;
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).Dislikes = res.Dislikes;
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).MostDisliked = res.MostDisliked;
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).MostLiked = res.MostLiked;
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).Subscribers = res.Subscribers;
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).VideoNames = res.VideoNames;
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).VideosCount = res.VideosCount.ToString();
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).Views = res.Views.ToString();
                                _WDb.YouTubeAccounts.FirstOrDefault(x => x.Id == k.Id).ViewsList = res.ViewsList;




                            }
                        }
                    }



                    
                    
                }
            }
            await _WDb.SaveChangesAsync();
        }
    }
}
