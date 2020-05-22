using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Accounts
{
    public interface IAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Monitor MonitorOwner { get; set; }

        public int MonitorOwnerId { get; set; }


    }
}
