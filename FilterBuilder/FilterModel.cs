using System;
using System.Collections.Generic;

namespace FilterBuilder
{
    public class FilterModel
    {
        public List<FilterObject> FilterObjects { get; set; } = null;
        public List<string> NotFilteredFields { get; set; } = null;
    }
}
