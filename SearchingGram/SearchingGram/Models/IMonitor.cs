using SearchingGram.Models.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models
{
    public interface IMonitor
    {
        [Key]
        public int Id { get; set; }
        public List<IAccount> Accounts { get; set; }
    }
}
