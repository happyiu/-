using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.BLL.Ext
{
    public class Manager
    {
        public Models.Ext.ManagerResponse GetList()
        {
            return new Models.Ext.ManagerResponse
            {
                Roles = (new CM.BLL.CmRole()).GetList(),
                Managers = (new CM.BLL.CmManagers()).GetList()
            };
        }
    }
}
