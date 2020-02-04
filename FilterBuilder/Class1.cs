using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilterBuilder
{
    public class _FilterBuilder<T>
    {
        private List<string> NotFilteredFields { get; set; } = null;

        public _FilterBuilder() { }

        public _FilterBuilder(List<string> notFilteredFields = null)
        {
            this.NotFilteredFields = notFilteredFields;
        }
        public Expression Build(List<FilterObject> filter)
        {
            return null;
        }
    }
}
