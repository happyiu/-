using System;
using System.Collections.Generic;

namespace CM.Models
{
    public partial class CmRolecategories
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long CategorieId { get; set; }
    }
}
