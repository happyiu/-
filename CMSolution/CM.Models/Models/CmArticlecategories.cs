using System;
using System.Collections.Generic;

namespace CM.Models
{
    public partial class CmArticlecategories
    {
        public long Id { get; set; }
        public long ParentCategoryId { get; set; }
        public string Name { get; set; }
        public long DisplaySequence { get; set; }
        public bool IsDefault { get; set; }
    }
}
