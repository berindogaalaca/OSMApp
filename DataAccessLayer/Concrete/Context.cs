using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using NetTopologySuite.Geometries;
//using NetTopologySuite.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host= localhost;Database=gis;Username=postgres;Password=berin123");
        }
        public DbSet<Point> Points { get; set; }
        public DbSet<Polygon> Polygons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Polygon>().ToTable("Polygons");

            var geometryConverter = new ValueConverter<NetTopologySuite.Geometries.Geometry, string>(
                g => g != null ? new NetTopologySuite.IO.WKTWriter().Write(g) : null,
                s => s != null ? new NetTopologySuite.IO.WKTReader().Read(s) : null
            );

            modelBuilder.Entity<Polygon>()
                .Property(p => p.Location)
                .HasColumnType("geometry")
                .HasConversion(geometryConverter);


        }
    }
    
}
