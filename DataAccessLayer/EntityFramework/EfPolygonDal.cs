using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfPolygonDal : GenericRepository<Polygon>, IPolygonDal
    {
        private readonly Context _context;

        public EfPolygonDal(Context context = null)
        {
            _context = context ?? new Context();
        }

        public Polygon GetById(int? PolygonId)
        {
            return _context.Polygons.SingleOrDefault(p => p.PolygonId == PolygonId);
        }

        public Polygon GetByName(string PolygonName)
        {
            Console.WriteLine(PolygonName);

            var polygon = _context.Polygons.AsEnumerable().FirstOrDefault(p => p.PolygonName == PolygonName);
            return polygon;
        }


        public Polygon GetByNumber(int? PolygonNumber)
        {
            return _context.Polygons.SingleOrDefault(p => p.PolygonNumber == PolygonNumber);
        }

        public Polygon GetByCoordinate(NetTopologySuite.Geometries.Geometry Location)
        {
            return _context.Polygons.SingleOrDefault(p => p.Location.Intersects(Location));
        }
    }
}
