using Newtonsoft.Json;
using SearchingGram.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchingGram.Services
{
    public class TikTokService : ITikTokService
    {
        public string URL { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string url = "http://127.0.0.1:4555/tiktokparse/tasks";

        public TikTokResponseUserInfo GetInfo(string name)
        {
            HttpClient client = new HttpClient();
            string resAsString;
            try
            {
                HttpResponseMessage response = client.GetAsync(url + $"?name={name}").Result;
                resAsString = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                throw new Exception("Trouble with TikTokParse!");
            }
            //TODO Try Catch 
            var deserialized = JsonConvert.DeserializeObject<TikTokResponseUserInfo>(resAsString);

            return deserialized;
        }

        public bool IsUserExist(string name)
        {
            HttpClient client = new HttpClient();
            string resAsString;
            try
            {
                HttpResponseMessage response = client.GetAsync(url + $"?name={name}").Result;
                resAsString = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                throw new Exception("Trouble with TikTokParse!");
            }

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(resAsString);
            if (deserialized.Keys.Contains("error_name")) return false;
            return true;
        }
    }
}
