using System;
using System.Collections.Generic;

namespace CM.Models
{
    public partial class CmMembers
    {
        public long Id { get; set; }
        public string OpenId { get; set; }
        public string UnionId { get; set; }
        public string UnionOpenId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Nick { get; set; }
        public int? SexType { get; set; }
        public string Photo { get; set; }
        public DateTime CreateDate { get; set; }
        public string RealName { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string DetailedAddress { get; set; }
        public string IdCard { get; set; }
        public long Integral { get; set; }
        public long UserIntegral { get; set; }
        public int State { get; set; }
        public sbyte Disabled { get; set; }
        public DateTime LastLonginDate { get; set; }
    }
}
