﻿using Microsoft.AspNetCore.Mvc;
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
        public TestController(IInstaService instaService, ITwitterService twitterService, ITikTokService tikTokService, ITimerService timer)
        {
            _instaService = instaService;
            _twitterService = twitterService;
            _tikTokService = tikTokService;
        }
        [HttpGet]
        [Route("/[controller]/testInsta")]
        public IActionResult Test(string name)
        {
            return Json(_instaService.GetInfo(name));

            
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
    }
}
