using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCite.Models;

namespace TestCite.ViewModels
{
    public class ViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}