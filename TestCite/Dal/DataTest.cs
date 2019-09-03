using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCite.Models;

namespace TestCite.Dal
{
    public class DataTest : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
    {
        int i = 0;
        protected override void Seed(DataContext context)
        {
            var templates = new List<Template>
        {

        new Template{ TemplateId= i, Name ="lonso"},
        new Template{TemplateId = i++,Name="Alonso"},
        new Template{TemplateId = i++,Name="Anand"},
        new Template{TemplateId = i++ ,Name="Barzdukas"},
        new Template{TemplateId = i++, Name="Li"},
        new Template{TemplateId = i++ ,Name="Justice"},
        new Template{TemplateId = i++ ,Name="Norman"},
        new Template{TemplateId = i++ ,Name="Olivetto"}
        };

            templates.ForEach(s => context.Templates.Add(s));
            context.SaveChanges();
            var settings = new List<Setting>
       
           
        {
        new Setting{Name="1050",},
        new Setting{Name="4022",},
        new Setting{Name="4041",},
        new Setting{Name="1045",},
        new Setting{Name="3141" },
        new Setting{Name="2021"},
        new Setting{Name="1050"},
        new Setting{Name="1050",},
        new Setting{Name="4022"},
        new Setting{Name="4041"},
        new Setting{Name="1045"},
        new Setting{Name="3141"},
        };
            settings.ForEach(s => context.Settings.Add(s));
            context.SaveChanges();
        }
        
    }
}