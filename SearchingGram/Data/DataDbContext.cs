using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SearchingGram.Models;

namespace SearchingGram.Data
{
    public class DataDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

       
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
