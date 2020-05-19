using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchingGram.Controllers.Responses;
using SearchingGram.Data;
using SearchingGram.Models;
using SearchingGram.Models.Accounts;
using SearchingGram.Models.Responses;
using SearchingGram.Services;
using Tweetinvi.Core.Extensions;

namespace SearchingGram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        public DataDbContext _Db;
        public WatcherDbContext _WDb;
        public readonly IInstaService _instaService;
        public readonly ITwitterService _twitterService;
        public readonly ITikTokService _tikTokService;
        private ITimerService _timer;
        public ValuesController(DataDbContext db, WatcherDbContext wdb,IInstaService instaService, ITwitterService twitterService, ITikTokService tikTokService, ITimerService timer)
        {
            _Db = db;
            _WDb = wdb;
            _instaService = instaService;
            _twitterService = twitterService;
            _tikTokService = tikTokService;
            _timer = timer;
        }
        
        [HttpGet]
        [Route("/[controller]/test")]
        public IActionResult Test() => Json(_timer.LastTimeRefresh());

        [HttpGet]
        [Route("/[controller]/getbyid")]
        public IActionResult GetLogin(int id)
        {
            var user = _Db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound("No user found");
            }
            return Ok(user.Token); 
        }

        [HttpPost]
        [Route("/[controller]/addmonitor")]
        public IActionResult AddMonitor(string token,string monitorName)
        {
            var User = checkToken(token);
            if (User == null) return Json("None");
            
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            if (watcher == null)
            {
                _WDb.Watchers.Add(new Watcher { Name = User.Login });
                _WDb.SaveChanges();
                watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            }
            if(_WDb.Monitors.FirstOrDefault(x => x.Name == monitorName) != null)
            {
                return Json(false);
            }
            _WDb.Monitors.Add(new Monitor { Name = monitorName, Watcher = watcher });
            _WDb.SaveChanges();
            return Ok(true);


        }
        [HttpPost]
        [Route("/[controller]/addaccount")]
        public IActionResult AddAccount(string token, string monitorName, string accountName, string accountType)        
        {
            var User = checkToken(token);
            if (User == null) return Json("No user ");
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            var monitor = _WDb.Monitors.FirstOrDefault(x => x.WatcherId == watcher.Id && x.Name==monitorName);
            if (monitor == null) return Json("No monitor");
            
            switch (accountType)
            {
                case "Twitter":
                     if (!_twitterService.IsUserExist(accountName)) return NotFound("No account found");
                    _WDb.TwitterAccounts.Add(new TwitterAccount { Name = accountName, MonitorOwner = monitor });
                    break;
                case "Instagram":
                    if (!_instaService.IsUserExist(accountName)) return NotFound("No account found");
                    _WDb.InstaAccounts.Add(new InstaAccount { Name = accountName, MonitorOwner = monitor });
                    break;
                case "TikTok":
                    if (!_tikTokService.IsUserExist(accountName)) return NotFound("No account found");
                    _WDb.TikTokAccounts.Add(new TikTokAccount{ Name = accountName, MonitorOwner = monitor });
                    break;
                default:
                    return NotFound("We don`t support this social network");
            }
            _WDb.SaveChanges();
            return Ok("Added");

        }
        [HttpGet]
        [Route("/[controller]/getaccslist")]
        public IActionResult GetStatistics(string token, string monitorName)
        {
            var User = checkToken(token);
            if (User == null) return NotFound("No user with this token!Please create token! ");
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            var monitor = _WDb.Monitors.FirstOrDefault(x => x.WatcherId == watcher.Id && x.Name == monitorName);
            if (monitor == null) return NotFound("No such file!");

            var resposeDict = new Dictionary<string, List<string>>() { { "name", new List<string>(){ watcher.Name } },
                                                                       { "Instagram", new List<string>()},
                                                                       { "TikTok", new List<string>()},
                                                                       { "Twitter", new List<string>()}};


            _WDb.InstaAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x=>resposeDict["Instagram"].Add(x.Name)); 
            _WDb.TikTokAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => resposeDict["TikTok"].Add(x.Name));
            _WDb.TwitterAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => resposeDict["Twitter"].Add(x.Name));
            return Json(resposeDict);
        }
        [HttpGet]
        [Route("/Values/get_accs_Info")]
        public IActionResult GetAccsInfo(string token, string monitorName)
        {
            var User = checkToken(token);
            if (User == null) return NotFound("No user with this token!Please create token! ");
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            var monitor = _WDb.Monitors.FirstOrDefault(x => x.WatcherId == watcher.Id && x.Name == monitorName);
            if (monitor == null) return NotFound("No such file!");


            var response = new AccountsInfoResponse
            {
                Instagram = new List<IInstaResponse>(),
                
                Twitter = new List<ITwitterResponse>()
            };
            _WDb.InstaAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => response.Instagram.Add((IInstaResponse)x));
            
            _WDb.TwitterAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => response.Twitter.Add((ITwitterResponse)x));
           


            return Json(response);
        }
        [HttpGet]
        [Route("/[controller]/get_monitors")]
        public IActionResult GetStatistics(string token)
        {
            var User = checkToken(token);
            if (User == null) return NotFound("No user with this token!Please create token! ");
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            IEnumerable<string> monitor = _WDb.Monitors.Where(x => x.WatcherId == watcher.Id).Select(x=>x.Name);
            
            

            var resposeDict = new Dictionary<string, List<string>>() { { "User", new List<string>(){ watcher.Name } },
                                                                       { "Monitors", monitor.ToList()} };

                                                                      


           
            return Json(resposeDict);
        }


        private User checkToken(string token)
        {
            foreach (var user in _Db.Users)
            {
                if (user.Token == token)
                {
                    return user;
                }
            }
            return null;

        }




    }
}