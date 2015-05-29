using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fitness.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string instructorName { get; set; }
        public decimal Price { get; set; }
        public decimal cLength { get; set; }
        public string timeTable { get; set; }
    }

    public class ServiceDBContext : DbContext
    {
        public DbSet<Service> Services { get; set; }
    }
}