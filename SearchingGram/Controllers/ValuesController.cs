﻿using System;
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
using SearchingGram.Services.Interfaces;
using Tweetinvi.Core.Extensions;

namespace SearchingGram.Controllers
{
    //++++++++++++++++++++++
    //Controller for interaction with data from WatchersDB, where stored user data and data from soc. networks
    //++++++++++++++++++++++
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        public DataDbContext _Db;
        //Watchers DB Context
        public WatcherDbContext _WDb;
        public readonly IInstaService _instaService;
        public readonly ITwitterService _twitterService;
        public readonly ITikTokService _tikTokService;
        public readonly IYou_TubeService _youtubeService;
        //Service, that using to add information about already added account
        public readonly IInitializeInfoService _initializeInfoService;
        
        public ValuesController(DataDbContext db, WatcherDbContext wdb,IInstaService instaService, ITwitterService twitterService, ITikTokService tikTokService, ITimerService timer, IYou_TubeService youtubeService, IInitializeInfoService initializeInfoService)
        {
            _Db = db;
            _WDb = wdb;
            _instaService = instaService;
            _twitterService = twitterService;
            _tikTokService = tikTokService;
            _youtubeService = youtubeService;
            
            _initializeInfoService = initializeInfoService;
        }
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //     Every method have parameter token - it`s unique user identificator, that generate in TokenController
        //     Every account called Watcher and have monitors. 
        //     Monitor - list of accounts networks. 
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //token-it`s unique user key, that generate in TokenController
        // method to add new monitor to Monitors table
        [HttpPost]
        [Route("/[controller]/addmonitor")]
        public async Task<IActionResult> AddMonitor(string token,string monitorName)
        {
            var User = checkToken(token);
            if (User == null) return Json("None");
            
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            //if user exist, but Watcher don`t exist, like in first request, creating Watcher, adding to WatchersDB. 
            if (watcher == null)
            {
                
                _WDb.Watchers.Add(new Watcher { Name = User.Login });
                await _WDb.SaveChangesAsync();
                watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            }
            if(_WDb.Monitors.FirstOrDefault(x => x.Name == monitorName) != null)
            {
                return Json(false);
            }
            _WDb.Monitors.Add(new Monitor { Name = monitorName, Watcher = watcher });
            await _WDb.SaveChangesAsync();
            return Ok(true);


        }
        //token-it`s unique user key, that generate in TokenController
        //method to add new Account 
        // monitorName - name of monitor, where user want add account 
        //accountType - Name of social network, can be Instagram, Twitter, YouTube 
        [HttpPost]
        [Route("/[controller]/addaccount")]
        public async Task<IActionResult> AddAccount(string token, string monitorName, string accountName, string accountType)        
        {
            var User = checkToken(token);
            if (User == null) return Json("No user ");
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            var monitor = _WDb.Monitors.FirstOrDefault(x => x.WatcherId == watcher.Id && x.Name==monitorName);
            if (monitor == null) return Json("No monitor");
            
            switch (accountType.ToLower())
            {
                case "twitter":
                    
                     if (!_twitterService.IsUserExist(accountName)) return NotFound("No account found");
                    await _WDb.TwitterAccounts.AddAsync(new TwitterAccount { Name = accountName, MonitorOwner = monitor });
                    await _initializeInfoService.InitInfoTwitter(accountName);
                    break;
                case "instagram":
                    if (!_instaService.IsUserExist(accountName)) return NotFound("No account found");
                    await _WDb.InstaAccounts.AddAsync(new InstaAccount { Name = accountName, MonitorOwner = monitor });
                    await _initializeInfoService.InitInfoInsta(accountName);
                    break;
                case "youtube":
                    if (!_youtubeService.IsUserExist(accountName)) return NotFound("No account found");
                    await _WDb.YouTubeAccounts.AddAsync(new YouTubeAccount { Name = accountName, MonitorOwner = monitor });
                    await _initializeInfoService.InitInfoYouTube(accountName);
                    break;
                //case "TikTok":
                //    if (!_tikTokService.IsUserExist(accountName)) return NotFound("No account found");
                //    await _WDb.TikTokAccounts.AddAsync(new TikTokAccount{ Name = accountName, MonitorOwner = monitor });
                //    break;
                

                default:
                    return NotFound("We don`t support this social network");
            }
           await  _WDb.SaveChangesAsync();
            return Ok("Added");

        }
        //token-it`s unique user key, that generate in TokenController
        //method, that return all accounts names in selected monitor
        //monitorName - name of monitor 
        [HttpGet]
        [Route("/[controller]/getaccslist")]
        public IActionResult GetStatistics(string token, string monitorName)
        {
            //404 if user don`t exist
            var User = checkToken(token);
            if (User == null) return NotFound("No user with this token!Please create token! ");

            
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            var monitor = _WDb.Monitors.FirstOrDefault(x => x.WatcherId == watcher.Id && x.Name == monitorName);
            // 404 if user don`t have Monitors
            if (monitor == null) return NotFound("No such file!");

            var resposeDict = new Dictionary<string, List<string>>() { { "name", new List<string>(){ watcher.Name } },
                                                                       { "Instagram", new List<string>()},
                                                                       { "YouTube", new List<string>()},
                                                                       { "Twitter", new List<string>()}};

            //Find account names
            _WDb.InstaAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x=>resposeDict["Instagram"].Add(x.Name)); 
            _WDb.YouTubeAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => resposeDict["YouTube"].Add(x.Name));
            _WDb.TwitterAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => resposeDict["Twitter"].Add(x.Name));
            return Json(resposeDict);
        }
        //token-it`s unique user key, that generate in TokenController
        //method, that return information about all accounts in selected monitor
        //monitorName - name of monitor 
        [HttpGet]
        [Route("/Values/get_accs_Info")]
        public IActionResult GetAccsInfo(string token, string monitorName)
        {   //404 if user don`t exist
            var User = checkToken(token);
            if (User == null) return NotFound("No user with this token!Please create token! ");
            //Find Watcher
            //404 if Monitor don`t exist in this Watcher  
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            var monitor = _WDb.Monitors.FirstOrDefault(x => x.WatcherId == watcher.Id && x.Name == monitorName);
            if (monitor == null) return NotFound("No such file!");


            var response = new AccountsInfoResponse
            {
                Instagram = new List<IInstaResponse>(),
                YouTube = new List<IYouTubeResponse>(),
                Twitter = new List<ITwitterResponse>()
            };
            // Convert objects from db to IResponse interfaces to show only data, that needed to user
            _WDb.InstaAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => response.Instagram.Add((IInstaResponse)x));
            _WDb.YouTubeAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => response.YouTube.Add((IYouTubeResponse)x));
            _WDb.TwitterAccounts.Where(y => y.MonitorOwnerId == monitor.Id).ForEach(x => response.Twitter.Add((ITwitterResponse)x));
           


            return Json(response);
        }
        //token-it`s unique user key, that generate in TokenController
        //method return information about one single account
        // monitorName - name of monitor, where user want add account 
        //accountName - name of account that must be Added
        //accountType - Name of social network, can be Instagram, Twitter, YouTube 
        [HttpGet]
        [Route("/Values/get_account_Info")]
        public IActionResult GetAccountInfo(string token, string monitorName, string accountName, string accountType)
        {
            //404 if user don`t exist
            var User = checkToken(token);
            if (User == null) return NotFound("No user with this token!Please create token! ");
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            var monitor = _WDb.Monitors.FirstOrDefault(x => x.WatcherId == watcher.Id && x.Name == monitorName);
            if (monitor == null) return NotFound("No such file!");

            // Convert objects from db to IResponse interfaces to show only data, that needed to user
            switch (accountType)
            {
                case "Twitter":
                    var response = new List<ITwitterResponse>();
                    response.Add((ITwitterResponse)_WDb.TwitterAccounts.FirstOrDefault(x => x.Name == accountName));
                   return Json(response);
                    
                case "Instagram":
                    var response1 = new List<IInstaResponse>();
                    response1.Add((IInstaResponse)_WDb.InstaAccounts.FirstOrDefault(x => x.Name == accountName));

                   return Json(response1);
                case "YouTube":
                    var response2 = new List<IYouTubeResponse>();
                    response2.Add((IYouTubeResponse)_WDb.YouTubeAccounts.FirstOrDefault(x => x.Name == accountName));

                    return Json(response2);
                   

                default:
                    return NotFound("No account");
            }
            


            return NotFound("No account");
        }
        //method return list of monitors from Watcher
        //token-it`s unique user key, that generate in TokenController
        [HttpGet]
        [Route("/[controller]/get_monitors")]
        public async Task<IActionResult> GetStatistics(string token)
        {
            var User = checkToken(token);
            if (User == null) return NotFound("No user with this token!Please create token! ");
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            if (watcher == null)
            {
                _WDb.Watchers.Add(new Watcher { Name = User.Login });
                await _WDb.SaveChangesAsync();
                watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            }
            IEnumerable<string> monitor = _WDb.Monitors.Where(x => x.WatcherId == watcher.Id).Select(x=>x.Name);

            Dictionary<string,List<string>> resposeDict;
            try
            {
                 resposeDict = new Dictionary<string, List<string>>() { { "User", new List<string>(){ watcher.Name } },
                                                                       { "Monitors", monitor.ToList()} };
            }
            catch
            {
                return NotFound();
            }

                                                                      


           
            return Json(resposeDict);
        }
        //token-it`s unique user key, that generate in TokenController
        //method return information about one single account
        // monitorName - name of monitor, where user want add account 
        //accountName - name of account that must be Added
        //accountType - Name of social network, can be Instagram, Twitter, YouTube
        [HttpDelete]
        [Route("/[controller]/delete_account")]
        public async Task<IActionResult> DeleteAccount(string token, string monitorName, string accountName, string accountType)
        {
            var User = checkToken(token);
            if (User == null) return NotFound("No user with this token!Please create token! ");
            var watcher = _WDb.Watchers.FirstOrDefault(x => x.Name == User.Login);
            var monitor = _WDb.Monitors.FirstOrDefault(x => x.WatcherId == watcher.Id && x.Name == monitorName);

            switch (accountType)
            {
                case "Twitter":

                    _WDb.TwitterAccounts.Remove(_WDb.TwitterAccounts.FirstOrDefault(x => x.Name == accountName));
                    break;
                case "Instagram":

                    _WDb.InstaAccounts.Remove(_WDb.InstaAccounts.FirstOrDefault(x => x.Name == accountName));
                    break;
                case "YouTube":

                    _WDb.YouTubeAccounts.Remove(_WDb.YouTubeAccounts.FirstOrDefault(x => x.Name == accountName));
                    break;
                
                default:
                    return NotFound("No account");
            }
            await _WDb.SaveChangesAsync();
            return Ok("Deleted");



        }

        // method to find user by Token
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