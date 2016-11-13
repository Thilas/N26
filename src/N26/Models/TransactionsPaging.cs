using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class TransactionsPaging
    {
        public string next { get; set; }
        public int totalResults { get; set; }
    }
}
