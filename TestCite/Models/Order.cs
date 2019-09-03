using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCite.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public int? templateID { get; set; }
        public Template template { get; set; }

        public virtual ICollection<DynamicValue> DynamicValues { get; set; }

        public Order()
        {
            DynamicValues = new List<DynamicValue>();
        }
       
        
        

    }
}