using SearchingGram.Data;
using System;
using System.Collections.Generic;
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
        public RefreshInfoService(WatcherDbContext  wdb, IInstaService instaService, ITwitterService twitterService, ITikTokService tikTokService, ITimerService timer)
        {
            _WDb = wdb;
            _instaService = instaService;
            _twitterService = twitterService;
            _tikTokService = tikTokService;
        }

        public async Task RefreshAccountsInfo()
        {
            foreach(var a in _WDb.Watchers)
            {
                //if (a.Monitors == null) continue;
                foreach(var b in _WDb.Monitors)
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

                                
                            }

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
                            _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).GrowsRetweets= CurrentRetweetsDict;

                            var currentUserPic = res.Pic;
                            _WDb.TwitterAccounts.FirstOrDefault(x => x.Id == e.Id).Pic = currentUserPic;
                        }
                    }
                }
            }
            await _WDb.SaveChangesAsync();
        }
    }
}
