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

        public void RefreshAccountsInfo()
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
                                currentInfoLikesDict[currentInfoLikesDict.Keys.Count+1] = res.Likes;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).GrowsLikes = currentInfoLikesDict;
                                //_WDb.SaveChanges();

                                var currentInfoCommentsDict = c.GrowsComments;
                                currentInfoCommentsDict[currentInfoCommentsDict.Keys.Count+1] = res.Comments;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).GrowsComments = currentInfoCommentsDict;
                                //_WDb.SaveChanges();

                                var currentInfoFollowersDict = c.GrowsFollowers;
                                currentInfoFollowersDict[currentInfoFollowersDict.Keys.Count+1] = res.Followers;
                                _WDb.InstaAccounts.FirstOrDefault(x => x.Id == c.Id).GrowsFollowers = currentInfoFollowersDict;
                                
                            }

                        }
                    }

                    foreach (var d in _WDb.TikTokAccounts)
                    {
                        if (d.MonitorOwnerId == b.Id)
                        {
                            var res = _tikTokService.GetInfo(d.Name);

                            var currentInfoLikesDict = d.GrowsLikes;
                            currentInfoLikesDict[currentInfoLikesDict.Keys.Count + 1] = res.Likes;
                            _WDb.TikTokAccounts.FirstOrDefault(x => x.Id == d.Id).GrowsLikes = currentInfoLikesDict;

                            var currentInfoFollowersDict = d.GrowsFollowers;
                            currentInfoFollowersDict[currentInfoFollowersDict.Keys.Count + 1] = res.Followers;
                            _WDb.TikTokAccounts.FirstOrDefault(x => x.Id == d.Id).GrowsFollowers = currentInfoFollowersDict;
                        }
                    }

                    //foreach (var c in b.TwitterAccounts)
                    //{
                        
                    //}
                }
            }
            _WDb.SaveChanges();
        }
    }
}
