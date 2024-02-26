using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host= localhost; Database=OSMApp ; Username=postgres; Password=berin123");
        }
        public DbSet<Point> Points { get; set; }
    }
}
