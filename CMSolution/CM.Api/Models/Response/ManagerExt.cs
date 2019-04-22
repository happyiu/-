using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Models.Response
{
    public class ManagerExt
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string Mobile { get; set; }
        public string Remark { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
