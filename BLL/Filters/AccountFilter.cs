using System;
using System.Collections.Generic;

namespace BLL.Filters
{
    public class AccountFilter
    {
        public Guid? IdEqual { get; set; }
        public string NameContains { get; set; }

        public Paging Paging { get; set; }
        public IList<Sort> Sorts { get; set; } = new List<Sort>();
    }
}
