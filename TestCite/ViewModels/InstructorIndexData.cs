using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCite.Models;

namespace TestCite.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Template> Templates { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
    }
}