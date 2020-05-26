using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Accounts
{
    //Basic class, that contain basic information about watched account
    public  class Account
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Monitor MonitorOwner { get; set; }

        public int MonitorOwnerId { get; set; }
    }
}
