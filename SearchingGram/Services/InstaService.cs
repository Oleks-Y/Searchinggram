using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchingGram.Services
{
    public class InstaService : IInstaService
    {
        //URL IS NOT USING EVERYWHERE !!!!!!!!!!!!!!!

        string INetService.URL { get=>  url; set { } }

        private string url = "https://instagramgetapi.herokuapp.com/instaparse/tasks";

        public InstaResponseUserInfo  GetInfo(string name)
        {
            HttpClient client = new HttpClient();
            string resAsString;
            try
            {
                HttpResponseMessage response = client.GetAsync(url +$"?name={name}").Result;
                resAsString = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                throw new Exception("Trouble with Instaparse!");
            }
            //TODO Try Catch 
            var deserialized = JsonConvert.DeserializeObject<InstaResponseUserInfo>(resAsString);

            return deserialized;


        }



            public bool IsUserExist(string name)
            {
            HttpClient client = new HttpClient();
            string resAsString;
            try
            {
                HttpResponseMessage response = client.GetAsync($"https://instagramgetapi.herokuapp.com/instaparse/tasks?name={name}").Result;
                resAsString = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                throw new Exception("Trouble with Instaparse!");
            }

            var deserialized = JsonConvert.DeserializeObject<InstaResponseUserInfo>(resAsString);
            
            if (deserialized.Is_error == true) return false;
            return true;


        }
    }
}
