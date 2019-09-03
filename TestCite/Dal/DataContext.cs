using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TestCite.Models;

namespace TestCite.Dal
{
    public class DataContext : DbContext
    {
        //static DataContext()
        //{
        //    Database.SetInitializer<DataContext>(new DataTest());
        //}

        public DataContext() : base("DBConnection")
        {

        }

        //public DbSet<Astro> Astros { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DynamicValue> dynamicValues { get; set; }

      

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }

}