using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N26.Models
{
    public class Cards
    {
        public CardsPaging paging { get; set; }
        public List<Card> data { get; set; }
    }
}
