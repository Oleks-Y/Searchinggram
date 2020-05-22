using SearchingGram.Models.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models
{
    public class Monitor 
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<InstaAccount> InstaAccounts { get; set; }
        public List<TikTokAccount> TikTokAccounts { get; set; }
        public List<TwitterAccount> TwitterAccounts { get; set; }

        public int WatcherId { get; set; }

        public Watcher Watcher { get; set; }
    }
}
