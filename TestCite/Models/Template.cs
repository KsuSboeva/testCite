using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestCite.Models
{
    public class Template
    {
       
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Setting> Settings { get; set; }

        public Template()
        {
            Settings = new List<Setting>();
            DynamicValues = new List<DynamicValue>();
        }

        public virtual ICollection<DynamicValue> DynamicValues { get; set; }
    }
}