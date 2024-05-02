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
            Console.WriteLine("get" + PolygonName.GetType());
            
            var polygon = _context.Polygons.SingleOrDefault(p => p.PolygonName == PolygonName);
            Console.WriteLine("po" + polygon);
            return polygon;
        }


        public Polygon GetByNumber(int? PolygonNumber)
        {
            var polygon = _context.Polygons.FirstOrDefault(p => p.PolygonNumber == PolygonNumber);

            Console.WriteLine(polygon);
            return polygon;
        }

        public Polygon GetByCoordinate(string Location)
        {
            return _context.Polygons.SingleOrDefault(p => p.Location==Location);
        }
    }
}
