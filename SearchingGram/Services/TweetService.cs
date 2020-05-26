using Newtonsoft.Json;
using SearchingGram.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Tweetinvi;
using TweetSharp;
namespace SearchingGram.Services
{
    public class TweetService : ITwitterService
    {
        public string URL { get ; set ; }

        public TwitterService _service;
        public TweetService()
        {
            _service=new TwitterService("", "", "", "");
        }
        //Service for Twitter API 
        public TweeterResponseUserInfo GetInfo(string name)
        {
           var finded= _service.SearchForUser(new SearchForUserOptions { Q = name });
            
            TwitterUser deserialized=null;
            foreach (var i in finded)
            {
                deserialized = i;
                break;
            }
            var response = _service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
            {
                ScreenName = deserialized.ScreenName,
                Count = 500,
                IncludeRts = false,
                ExcludeReplies = true
            });
            int retweetsCount = 0;
            int maxretweets = 0;
            int minretweets = 100000000;
            string MaxText = "";
            string MinText = "";
            List<int> retweetsCountsList = new List<int>();
            foreach (var i in response)
            {
                retweetsCount += i.RetweetCount;
                if (maxretweets < i.RetweetCount) { maxretweets = i.RetweetCount; MaxText = i.TextDecoded; }
                if(minretweets> i.RetweetCount) { minretweets = i.RetweetCount; MinText = i.TextDecoded; }
                retweetsCountsList.Add(i.RetweetCount);
            }

            return new TweeterResponseUserInfo { ScreenName = deserialized.ScreenName,
                FollowerCount = deserialized.FollowersCount, 
                RetweetsCount = retweetsCount, 
                Pic=deserialized.ProfileImageUrl, 
                MaxRetweets=maxretweets,
                MinRetweets = minretweets,
                MaxRetweets_Text = MaxText,
                MinRetweets_Text = MinText,
                RetweetsList = retweetsCountsList
            };
           
            
        }

        public bool IsUserExist(string name)
        {
            var result = _service.SearchForUser(new SearchForUserOptions { Q = name });           
            if (result.ToJson()== "[]")
            {
                return false;
            }
            return true;
        }
    }
}
