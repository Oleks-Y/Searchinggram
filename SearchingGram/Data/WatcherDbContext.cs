using Microsoft.EntityFrameworkCore;
using SearchingGram.Models;
using SearchingGram.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Data
{
    public class WatcherDbContext : DbContext
    {
        public WatcherDbContext(DbContextOptions<WatcherDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Watcher> Watchers { get; set; }

        public DbSet<Monitor> Monitors { get; set; }

        public DbSet<InstaAccount> InstaAccounts { get; set; }
        public DbSet<TikTokAccount> TikTokAccounts { get; set; }
        public DbSet<TwitterAccount> TwitterAccounts { get; set; }





    }
}
