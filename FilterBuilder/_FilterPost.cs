using System;
using System.Collections.Generic;
using System.Text;
using FilterBuilder.Test;

namespace FilterBuilder
{
    public class _FilterPost : _FilterBuilder<Post>
    {
        public _FilterPost() : base(new List<string>() { nameof(Post.Body) }) { }
    }

    public class tttt
    {
        public tttt()
        {
            List<FilterObject> filter = new List<FilterObject>();
            var s = new _FilterPost();
            var expression = s.Build(filter);
        }
    }
}
