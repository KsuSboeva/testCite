using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCite.Models
{
    
    public class Setting
    {
        public int Id { get; set; }
        // int? TemplateId { get; set; }
        //public Template Template { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public virtual ICollection<Template> Templates { get; set; }

        public Setting()
        {
            Templates = new List<Template>();
            DynamicValues = new List<DynamicValue>();

        }

        public virtual ICollection<DynamicValue> DynamicValues { get; set; }

       
    }
}