using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace test.Models
{
    public class helloworld
    {
        public int ID { get; set; }
        public string man { get; set; }
        public int age { get; set; }
        public string introduce { get; set; }
        public string skill { get; set; }
        public string href { get; set; }
        public decimal Price { get; set; }
    }

    public class helloworldDBContext : DbContext
    {
        public DbSet<helloworld> helloworld { get; set; }
    }

 
}