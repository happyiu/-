using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CM.Models
{
    public partial class CmManagers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public int? RoleId { get; set; }
        [JsonIgnore]
        public string PassWord { get; set; }
        [JsonIgnore]
        public string OpenId { get; set; }
        public string Mobile { get; set; }
        public string Remark { get; set; }
        [JsonConverter(typeof(Public.DateTimeConverter))]
        public DateTime? CreateDate { get; set; }
    }
}
