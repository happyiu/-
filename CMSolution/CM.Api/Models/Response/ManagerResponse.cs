using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.Models;

namespace CM.Models.Ext
{
    public class ManagerResponse
    {
        public List<CmManagers> Managers { get; set; }
        public List<CmRole> Roles { get; set; }
    }
}
