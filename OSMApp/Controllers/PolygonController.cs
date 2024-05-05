using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
                var reader = new NetTopologySuite.IO.GeoJsonReader();
                NetTopologySuite.Geometries.Geometry geom = reader.Read<NetTopologySuite.Geometries.Geometry>(locationJson);

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

                Console.WriteLine(polygon.Location.GetType());


                bool coordinatesExist = _polygonManager.TGetByCoordinate(polygon.Location) != null;
                Console.WriteLine("coor" + coordinatesExist);
                bool nameExists = _polygonManager.TGetByName(polygon.PolygonName) != null;
                Console.WriteLine("name exists ->" + nameExists);

                bool numberExists = _polygonManager.TGetByNumber(polygon.PolygonNumber) != null;
                Console.WriteLine("Number exists: " + numberExists);

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
                    responseMessage.Data = polygon;
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
        public Response QueryPoints([FromQuery] string? PolygonName, [FromQuery] int? PolygonNumber)
        {
            try
            {

                if (string.IsNullOrEmpty(PolygonName) && !PolygonNumber.HasValue  )
                {
                    var points = _polygonManager.GetList();
                    responseMessage.Data = points;
                    responseMessage.Success = true;
                    responseMessage.Message = "Get Points list successfully";

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
    }
}