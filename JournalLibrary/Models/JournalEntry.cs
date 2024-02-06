using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalLibrary.Models
{
    internal class JournalEntry
    {
        public DateTime date { get; set; }
        public decimal temperature { get; set; }
        public decimal humidity { get; set; }
        public string? description { get; set; }
    }
}
