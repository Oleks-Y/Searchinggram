using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models
{
    public class Watcher
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Monitor> Monitors { get; set; }
    }
}
