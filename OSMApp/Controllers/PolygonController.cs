using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OSMApp.Models;
using System.Text.Json;

namespace OSMApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolygonController : Controller
    {
        Response responseMessage = new Response();
        PolygonManager _polygonManager = new PolygonManager(new EfPolygonDal());

        [HttpPost]
        public Response AddPolygon([FromBody] JsonElement requestBody)
        {
            if (requestBody.ValueKind == JsonValueKind.Null)
            {
                responseMessage.Success = false;
                responseMessage.Message = "Request data not found.";
                responseMessage.Data = null;
                return responseMessage;
            }
            if (requestBody.GetProperty("PolygonName").ValueKind != JsonValueKind.String ||
                requestBody.GetProperty("PolygonNumber").ValueKind != JsonValueKind.Number ||
                !requestBody.TryGetProperty("Location", out var location) ||
                location.ValueKind != JsonValueKind.Object)
            {
                responseMessage.Success = false;
                responseMessage.Message = "Request data is missing or invalid.";
                responseMessage.Data = null;
                return responseMessage;
            }
            string name = requestBody.GetProperty("PolygonName").GetString();
            int polygonNumber = requestBody.GetProperty("PolygonNumber").GetInt32();
            if (polygonNumber==null)
            {
                responseMessage.Success = false;
                responseMessage.Message = "Number field is invalid";
                responseMessage.Data = null;
                
            }
            var locationJson = location.GetRawText();
            try
            {
                var reader = new GeoJsonReader();
                NetTopologySuite.Geometries.Polygon geom = null;
                geom = reader.Read<NetTopologySuite.Geometries.Polygon>(locationJson);

                if (geom == null)
                {
                    responseMessage.Success = false;
                    responseMessage.Message = "Geometry cannot be null";
                    responseMessage.Data = null;
                    return responseMessage;
                }

                var polygon = new Polygon
                {
                    PolygonName = name,
                    PolygonNumber = polygonNumber,
                    Location = geom,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                bool coordinatesExist = _polygonManager.TGetByCoordinate(polygon.Location)!= null;
                bool numberExists = _polygonManager.TGetByNumber(polygon.PolygonNumber) != null;
                bool nameExists = _polygonManager.TGetByName(polygon.PolygonName) != null;

                if (coordinatesExist || nameExists || numberExists)
                {
                    responseMessage.Success = false;
                    responseMessage.Message = "The provided polygon coordinates, name, or number already exist.";
                    responseMessage.Data = null;
                }
                else
                {
                    _polygonManager.TAdd(polygon);
                    responseMessage.Success = true;
                    responseMessage.Message = "Polygon added successfully.";
                    responseMessage.Data = requestBody;
                }
            }
            catch (Exception e)
            {
                responseMessage.Success = false;
                responseMessage.Message = e.Message;
                responseMessage.Data = requestBody;
            }

            return responseMessage;
        }

        [HttpGet]
        public Response QueryPoints([FromQuery] string? PolygonName, [FromQuery] int? PolygonNumber, [FromQuery] string? Location)
        {
           
            
            try
            {
                    
                if (string.IsNullOrEmpty(PolygonName) && !PolygonNumber.HasValue && string.IsNullOrEmpty(Location))
                {
                    var polygons = _polygonManager.TGetPolygonList();
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };

                    string json = JsonConvert.SerializeObject(polygons, settings);

                    responseMessage.Data = json;
                    responseMessage.Success = true;
                    responseMessage.Message = "Get Polygon list successfully";

                }

                else if (!string.IsNullOrEmpty(PolygonName) && !PolygonNumber.HasValue && string.IsNullOrEmpty(Location))
                {
                    var polygonGetByName = _polygonManager.TGetByName(PolygonName);

                    var wktWriter = new WKTWriter();
                    var locationWKT = wktWriter.Write(polygonGetByName.Location);
                    int number = polygonGetByName.PolygonNumber;
                    var json = ConvertToJSON(PolygonName, number, locationWKT);
                    if (polygonGetByName != null)
                    {
                        responseMessage.Data = json;
                        responseMessage.Success = true;
                        responseMessage.Message = "Name found";

                    }
                    else
                    {
                        responseMessage.Data = null;
                        responseMessage.Success = false;
                        responseMessage.Message = "Name didn't found";

                    }
                }
                else if (string.IsNullOrEmpty(PolygonName) && PolygonNumber.HasValue && string.IsNullOrEmpty(Location))
                {
                    var polygonGetByNumber = _polygonManager.TGetByNumber(PolygonNumber);

                    var wktWriter = new WKTWriter();
                    var locationWKT = wktWriter.Write(polygonGetByNumber.Location);
                    int number = polygonGetByNumber.PolygonNumber;
                    var json = ConvertToJSON(PolygonName, number, locationWKT);
                    if (polygonGetByNumber != null)
                    {
                        responseMessage.Data = json;
                        responseMessage.Success = true;
                        responseMessage.Message = "Number found";

                    }
                    else
                    {
                        responseMessage.Data = null;
                        responseMessage.Success = false;
                        responseMessage.Message = "Number didn't found";

                    }
                }
                else if (string.IsNullOrEmpty(PolygonName) && !PolygonNumber.HasValue && !string.IsNullOrEmpty(Location))
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read(Location);
                    var polygonGetByCoordinate = _polygonManager.TGetByCoordinate(geometry);

                    var wktWriter = new WKTWriter();
                    var locationWKT = wktWriter.Write(polygonGetByCoordinate.Location);
                    int number = polygonGetByCoordinate.PolygonNumber;
                    var json = ConvertToJSON(polygonGetByCoordinate.PolygonName, number, locationWKT);
                    if (polygonGetByCoordinate != null)
                    {
                        responseMessage.Data = json;
                        responseMessage.Success = true;
                        responseMessage.Message = "Coordinate found";
                    }
                    else
                    {
                        responseMessage.Data = null;
                        responseMessage.Success = false;
                        responseMessage.Message = "Coordinate didn't found";
                    }
                }
                else if (string.IsNullOrEmpty(PolygonName) && PolygonNumber.HasValue && !string.IsNullOrEmpty(Location))
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read(Location);
                    var polygonGetByCoordinateNumber = _polygonManager.TGetByCoordinateNumber(PolygonNumber, geometry);

                    var wktWriter = new WKTWriter();
                    var locationWKT = wktWriter.Write(polygonGetByCoordinateNumber.Location);
                    int number = polygonGetByCoordinateNumber.PolygonNumber;
                    var json = ConvertToJSON(PolygonName, number, locationWKT);
                    if (polygonGetByCoordinateNumber != null)
                    {
                        responseMessage.Data = json;
                        responseMessage.Success = true;
                        responseMessage.Message = "Coordinate and Number found";
                    }
                    else
                    {
                        responseMessage.Data = null;
                        responseMessage.Success = false;
                        responseMessage.Message = "Coordinate and Number didn't found";
                    }
                }
                else if (!string.IsNullOrEmpty(PolygonName) && !PolygonNumber.HasValue && !string.IsNullOrEmpty(Location))
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read(Location);
                    var polygonGetByCoordinateName = _polygonManager.TGetByCoordinateName(PolygonName, geometry);

                    var wktWriter = new WKTWriter();
                    var locationWKT = wktWriter.Write(polygonGetByCoordinateName.Location);
                    int number = polygonGetByCoordinateName.PolygonNumber;
                    var json = ConvertToJSON(PolygonName, number, locationWKT);
                    if (polygonGetByCoordinateName != null)
                    {
                        responseMessage.Data = json;
                        responseMessage.Success = true;
                        responseMessage.Message = "Coordinate and Name found";
                    }
                    else
                    {
                        responseMessage.Data = null;
                        responseMessage.Success = false;
                        responseMessage.Message = "Coordinate and Name didn't found";
                    }
                }
                else if (!string.IsNullOrEmpty(PolygonName) && PolygonNumber.HasValue && string.IsNullOrEmpty(Location))
                {
                    var polygonGetByNumberName = _polygonManager.TGetByNumberName(PolygonName, PolygonNumber);
                    var wktWriter = new WKTWriter();
                    var locationWKT = wktWriter.Write(polygonGetByNumberName.Location);
                    int number = polygonGetByNumberName.PolygonNumber;
                    var json = ConvertToJSON(PolygonName, number, locationWKT);

                   
                    if (polygonGetByNumberName != null)
                    {
                        responseMessage.Data = json;
                        responseMessage.Success = true;
                        responseMessage.Message = "Name and Number found";
                    }
                    else
                    {
                        responseMessage.Data = null;
                        responseMessage.Success = false;
                        responseMessage.Message = "Name and Number didn't found";
                    }
                }
                else
                {
                    WKTReader wktReader = new WKTReader();
                    NetTopologySuite.Geometries.Geometry geometry = wktReader.Read(Location);
                    var polygonGetByCoordinateNumberName = _polygonManager.TGetByCoordinateNumberName(PolygonNumber, geometry, PolygonName);
                    var wktWriter = new WKTWriter();
                    var locationWKT = wktWriter.Write(geometry);
                    int number = polygonGetByCoordinateNumberName.PolygonNumber;
                    var json = ConvertToJSON(PolygonName, number, locationWKT);

                    if (polygonGetByCoordinateNumberName != null)
                    {
                        responseMessage.Data = json;
                        responseMessage.Success = true;
                        responseMessage.Message = "Name, Number and Coordinate found";
                    }
                    else
                    {
                        responseMessage.Data = null;
                        responseMessage.Success = false;
                        responseMessage.Message = "Name, Number and Coordinate didn't found";
                    }
                }
            }
            catch (Exception e)
            {
                responseMessage.Data = null;
                responseMessage.Success = false;
                responseMessage.Message = e.Message;
            }
            return responseMessage;
        }
        public class PolygonInfo
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public string Location { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        public string ConvertToJSON(string name, int number, string locationWKT)
        {
            var polygonInfo = new PolygonInfo
            {
                Name = name,
                Number = number,
                Location = locationWKT
            };

            return polygonInfo.ToString();
        }

        [HttpDelete("{PolygonId:int}")]
        public Response DeleteValue(int? PolygonId)
        {
            try
            {
                var polygonValue = _polygonManager.TGetByID(PolygonId);
                Console.WriteLine(polygonValue);
                var wktWriter = new WKTWriter();
                var locationWKT = wktWriter.Write(polygonValue.Location);
                int number = polygonValue.PolygonNumber;
                var json = ConvertToJSON(polygonValue.PolygonName, number, locationWKT);
                if (polygonValue == null)
                {
                    responseMessage.Success = false;
                    responseMessage.Message = "Polygon value undefined";
                    responseMessage.Data = null;
                }
                else
                {
                    _polygonManager.TDelete(polygonValue);
                    responseMessage.Success = true;
                    responseMessage.Message = "Polygon deleted successfully.";
                    responseMessage.Data = json;
                }
            }
            catch (Exception e)
            {
                responseMessage.Success = false;
                responseMessage.Message = e.Message;
                responseMessage.Data = null;
            }
            return responseMessage;
        }
        [HttpPut("{PolygonId}")]
        public Response UpdatePoint(int? PolygonId, [FromBody] JsonElement requestBody)
        {
            try
            {
              
                var existingPolygon = _polygonManager.TGetByID(PolygonId);

                if (existingPolygon == null)
                {
                    responseMessage.Success = false;
                    responseMessage.Message = "Undefined point";
                    responseMessage.Data = null;
                }
                string name = requestBody.GetProperty("PolygonName").GetString();
                int polygonNumber = requestBody.GetProperty("PolygonNumber").GetInt32();
                var location = requestBody.GetProperty("Location");

                int polygonId = existingPolygon.PolygonId;
                var locationJson = location.GetRawText();
                var reader = new GeoJsonReader();
                NetTopologySuite.Geometries.Polygon geom = null;
                geom = reader.Read<NetTopologySuite.Geometries.Polygon>(locationJson);

                var updatepolygon= _polygonManager.UpdatePolygon(polygonId,polygonNumber, geom, name);

                var wktWriter = new WKTWriter();
                var locationWKT = wktWriter.Write(updatepolygon.Location);
                int number = updatepolygon.PolygonNumber;
                var json = ConvertToJSON(name, number, locationWKT);


                responseMessage.Success = false;
                responseMessage.Message = "Updated point";
                responseMessage.Data = json;
            }
            catch (Exception e)
            {
                responseMessage.Success = false;
                responseMessage.Message = e.Message;
                responseMessage.Data = null;
            }
            return responseMessage;

        }
    }
}