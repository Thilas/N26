using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class Transactions
    {
        public TransactionsPaging paging { get; set; }
        public List<Transaction> data { get; set; }
    }
}
