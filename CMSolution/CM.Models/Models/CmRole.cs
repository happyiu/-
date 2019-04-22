using System;
using System.Collections.Generic;

namespace CM.Models
{
    public partial class CmRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Rank { get; set; }
    }
}
