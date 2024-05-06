using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite;
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
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var command = new NpgsqlCommand($"SELECT *, ST_AsText(\"Location\") AS location_text  FROM public.\"Polygons\" WHERE \"PolygonName\" = @PolygonName LIMIT 1", connection);
                command.Parameters.AddWithValue("@PolygonName", PolygonName);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read((string)reader["location_text"]);
                    var polygon = new Polygon
                    {
                        PolygonName = (string)reader["PolygonName"],
                        PolygonNumber = (int)reader["PolygonNumber"],
                        Location = geometry,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    connection.Close();
                    return polygon;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing SQL query: " + ex.Message);
                return null;
            }
        }


        public Polygon GetByNumber(int? PolygonNumber)
        {
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var command = new NpgsqlCommand($"SELECT *, ST_AsText(\"Location\") AS location_text  FROM public.\"Polygons\" WHERE \"PolygonNumber\" = @PolygonNumber LIMIT 1", connection);
                command.Parameters.AddWithValue("@PolygonNumber", PolygonNumber);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read((string)reader["location_text"]);
                    var polygon = new Polygon
                    {
                        PolygonName = (string)reader["PolygonName"],
                        PolygonNumber = (int)reader["PolygonNumber"],
                        Location = geometry,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    connection.Close();
                    return polygon;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing SQL query: " + ex.Message);
                return null;
            }
        }


        public Polygon GetByCoordinate(NetTopologySuite.Geometries.Geometry Location)
        {
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var wktWriter = new WKTWriter();
                var locationWKT = wktWriter.Write(Location);

                var command = new NpgsqlCommand($"SELECT *, ST_AsText(\"Location\") AS location_text FROM public.\"Polygons\" WHERE ST_Equals(\"Location\", (ST_GeomFromText('{locationWKT}', 4326))) LIMIT 1", connection);

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read((string)reader["location_text"]);
                    var polygon = new Polygon
                    {
                        PolygonName = (string)reader["PolygonName"],
                        PolygonNumber = (int)reader["PolygonNumber"],
                        Location = geometry,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    connection.Close();
                    return polygon;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing SQL query: " + ex.Message);
                return null;
            }
        }






    }
}
