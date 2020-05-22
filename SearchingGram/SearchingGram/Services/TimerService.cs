using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SearchingGram.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Services
{
    public  class TimerService : ITimerService
    {
       

        public bool LastTimeRefresh()
        {
            using (StreamReader r = new StreamReader("TimeInfo.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Dictionary<string,string>>(json);
                DateTime last = DateTime.Parse(items["LastRefresh"]);

                //!!!!!!!!!!!!!!!! TimeSpan is here!!!!!!!!!!!!
                return (DateTime.Now-last) > new TimeSpan(3, 0, 0);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
        }
        public void NewTimeStamp(string mm_dd_yyyy__tttt)
        {
            //using(StreamWriter w = new StreamWriter("TimeInfo.json"))
            //{
            var time = DateTime.Parse(mm_dd_yyyy__tttt);
            var mark = new Dictionary<string, string>() { { "LastRefresh", time.ToString() } };
            File.WriteAllText("TimeInfo.json", JsonConvert.SerializeObject(mark));

            //}
        }
    }
}
