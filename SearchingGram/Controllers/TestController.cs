using Microsoft.AspNetCore.Mvc;
using SearchingGram.Data;
using SearchingGram.Models.Accounts;
using SearchingGram.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TestController : Controller
    {
        public readonly IInstaService _instaService;
        public readonly ITwitterService _twitterService;
        public readonly ITikTokService _tikTokService;
        public readonly IRefreshInfoService _refreshInfo;
        public WatcherDbContext _WDb;
        public TestController(IInstaService instaService, ITwitterService twitterService, ITikTokService tikTokService, ITimerService timer, IRefreshInfoService refreshInfo, WatcherDbContext WDb)
        {
            _instaService = instaService;
            _twitterService = twitterService;
            _tikTokService = tikTokService;
            _refreshInfo = refreshInfo;
            _WDb = WDb;
        }
        [HttpGet]
        [Route("/[controller]/testInsta")]
        public IActionResult Test()
        {
            List<string> res = new List<string>();
            foreach(var i in _WDb.InstaAccounts)
            {
                res.Add(i.Name);
                res.Add(i.MonitorOwnerId.ToString());
            }
            foreach (var i in _WDb.TwitterAccounts)
            {
                res.Add(i.Name);
                res.Add(i.MonitorOwnerId.ToString());
            }
            foreach (var i in _WDb.YouTubeAccounts)
            {
                res.Add(i.Name);
                res.Add(i.MonitorOwnerId.ToString());
            }


            return Json(res);

            
        }
        [HttpGet]
        [Route("/[controller]/testTikTok")]
        public IActionResult Test1(string name)
        {
            return Json(_tikTokService.GetInfo(name));


        }
        [HttpGet]
        [Route("/[controller]/testTwitter")]
        public IActionResult Test2(string name)
        {
            return Json(_twitterService.GetInfo(name));


        }
        [HttpGet]
        [Route("/[controller]/testRefresh")]
        public IActionResult Test3(string name)
        {
            _refreshInfo.RefreshAccountsInfo();
            return Ok("Done");


        }

    }
}
