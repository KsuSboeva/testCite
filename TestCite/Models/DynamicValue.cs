using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCite.Models
{
    public class DynamicValue
    {
        public int Id { get; set; }
        public int? settingId { get; set; }
        public Setting setting { get; set; }
        public string settingName { get; set; }
        public int? OrderId { get; set; }
        public Order order { get; set; }
        public int? templateID { get; set; }
        public Template template { get; set; }
        public string dynamicValue { get; set; } 

    }
}