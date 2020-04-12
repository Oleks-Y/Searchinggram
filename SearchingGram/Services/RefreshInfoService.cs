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
        public RefreshInfoService(WatcherDbContext  wdb)
        {
            _WDb = wdb;
        }

        public void PefreshAccountsInfo()
        {
            foreach(var a in _WDb.Watchers)
            {
                foreach(var b in a.Monitors)
                {
                    foreach(var c in b.InstaAccounts)
                    {
                        
                    }
                    foreach(var c in b.TikTokAccounts)
                    {

                    }
                    foreach(var c in b.TwitterAccounts)
                    {

                    }
                }
            }
        }
    }
}
