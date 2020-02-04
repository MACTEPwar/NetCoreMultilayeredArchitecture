using System;
using System.Collections.Generic;
using System.Text;

namespace FilterBuilder
{
    public class OperationObject: FilterObject
    {
        public string Field { get; set; }
        public string Operation { get; set; }
        public string Property { get; set; }
        List<FilterObject> SubFilter { get; set; } = null;
    }
}
