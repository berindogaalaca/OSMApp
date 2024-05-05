using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.IO;
using Npgsql;
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
            var query = $"SELECT * FROM public.\"Polygons\" WHERE \"PolygonName\" = @PolygonName";

            return _context.Polygons.FromSqlRaw(query, new NpgsqlParameter("@PolygonName", NpgsqlTypes.NpgsqlDbType.Text) { Value = PolygonName }).FirstOrDefault();
        }


        public Polygon GetByNumber(int? PolygonNumber)
        {
            var query = $"SELECT * FROM public.\"Polygons\" WHERE \"PolygonNumber\" = @PolygonNumber";

            return _context.Polygons.FromSqlRaw(query, new NpgsqlParameter("@PolygonNumber", NpgsqlTypes.NpgsqlDbType.Integer) { Value = PolygonNumber }).FirstOrDefault();
        }


        public Polygon GetByCoordinate(NetTopologySuite.Geometries.Geometry Location)
        {
            var wktWriter = new WKTWriter();
            var locationWKT = wktWriter.Write(Location);
            var reader = new WKTReader();
            var geometry = reader.Read(locationWKT);

            var polygons = _context.Polygons
            .FromSqlInterpolated($"SELECT * FROM public.\"Polygons\" WHERE ST_Intersects(\"Location\", ST_Transform(ST_GeomFromText({geometry}, 4326), 4326))")
            .FirstOrDefault();

            return polygons;
        }

    }
}
