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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var command = new NpgsqlCommand($"SELECT *, ST_AsText(\"Location\") AS location_text  FROM public.\"Polygons\" WHERE \"PolygonId\" = @PolygonId LIMIT 1", connection);
                command.Parameters.AddWithValue("@PolygonId", PolygonId);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read((string)reader["location_text"]);
                    var polygon = new Polygon
                    {
                        PolygonId = (int)reader["PolygonId"],
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
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
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

        public List<Polygon> GetPolygonList()
        {
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var commandAll = new NpgsqlCommand($"SELECT *, ST_AsText(\"Location\") AS location_text FROM public.\"Polygons\"", connection);

                connection.Open();
                var reader = commandAll.ExecuteReader();

                List<Polygon> polygons = new List<Polygon>();

                while (reader.Read())
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read((string)reader["location_text"]);

                    Polygon polygon = new Polygon
                    {
                        PolygonName = (string)reader["PolygonName"],
                        PolygonNumber = (int)reader["PolygonNumber"],
                        Location = geometry,
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };

                    polygons.Add(polygon);
                    

                }
                connection.Close();
                return polygons;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing SQL query: " + ex.Message);
                return null;
            }
        }

        public Polygon GetByNumberName(string PolygonName, int? PolygonNumber)
        {
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();

                var command = new NpgsqlCommand($"SELECT *, ST_AsText(\"Location\") AS location_text FROM public.\"Polygons\" WHERE (\"PolygonName\" = @PolygonName AND \"PolygonNumber\" = @PolygonNumber) LIMIT 1", connection);
                command.Parameters.AddWithValue("@PolygonName", PolygonName);
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
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
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

        public Polygon GetByCoordinateName(string PolygonName, NetTopologySuite.Geometries.Geometry? Location)
        {
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var wktWriter = new WKTWriter();
                var locationWKT = wktWriter.Write(Location);

                var command = new NpgsqlCommand($"SELECT *, ST_AsText(\"Location\") AS location_text " +
                                $"FROM public.\"Polygons\" " +
                                $"WHERE \"PolygonName\" = @PolygonName " +
                                $"AND ST_Equals(\"Location\", ST_GeomFromText(@LocationWKT, 4326)) " +
                                $"LIMIT 1", connection);

                command.Parameters.AddWithValue("@PolygonName", PolygonName);
                command.Parameters.AddWithValue("@LocationWKT", locationWKT);


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
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
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

        public Polygon GetByCoordinateNumber(int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location)
        {
            
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var wktWriter = new WKTWriter();
                var locationWKT = wktWriter.Write(Location);

                var command = new NpgsqlCommand($"SELECT *, ST_AsText(\"Location\") AS location_text " +
                                 $"FROM public.\"Polygons\" " +
                                 $"WHERE \"PolygonNumber\" = @PolygonNumber " +
                                 $"AND ST_Equals(\"Location\", ST_GeomFromText(@LocationWKT, 4326)) " +
                                 $"LIMIT 1", connection);

                command.Parameters.AddWithValue("@PolygonNumber", PolygonNumber);
                command.Parameters.AddWithValue("@LocationWKT", locationWKT);


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
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
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

        public Polygon GetByCoordinateNumberName(int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location, string PolygonName)
        {
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var wktWriter = new WKTWriter();
                var locationWKT = wktWriter.Write(Location);

                var command = new NpgsqlCommand(
                $"SELECT *, ST_AsText(\"Location\") AS location_text " +
                $"FROM public.\"Polygons\" " +
                $"WHERE \"PolygonName\" = @PolygonName " +
                $"AND \"PolygonNumber\" = @PolygonNumber " +
                $"AND ST_Equals(\"Location\", ST_GeomFromText('{locationWKT}', 4326))", connection);

                command.Parameters.AddWithValue("@PolygonName", PolygonName);
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
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
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

        public Polygon UpdatePolygon(int? PolygonId, int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location, string PolygonName)
        {
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var wktWriter = new WKTWriter();
                var locationWKT = wktWriter.Write(Location);

                var command = new NpgsqlCommand(
                    $"UPDATE public.\"Polygons\" " +
                    $"SET \"PolygonName\" = @NewPolygonName, " +
                    $"\"PolygonNumber\" = @NewPolygonNumber, " +
                    $"\"Location\" = ST_GeomFromText('{locationWKT}', 4326), " +
                    $"\"UpdatedAt\" = NOW() " +
                    $"WHERE \"PolygonId\" = @PolygonId", connection);

                command.Parameters.AddWithValue("@NewPolygonName", PolygonName);
                command.Parameters.AddWithValue("@NewPolygonNumber", PolygonNumber);
                command.Parameters.AddWithValue("@PolygonId", PolygonId);

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read(locationWKT);
                    var updatedPolygon = new Polygon
                    {
                        PolygonId = (int)PolygonId,
                        PolygonName = PolygonName,
                        PolygonNumber = (int)PolygonNumber,
                        Location = geometry,
                        UpdatedAt = DateTime.Now
                    };
                    return updatedPolygon;
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
