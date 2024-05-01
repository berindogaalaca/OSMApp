using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
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

        public NetTopologySuite.Geometries.Geometry ConvertJsonToGeometry(JsonElement locationElement)
        {
            string json = locationElement.GetRawText();
            var reader = new NetTopologySuite.IO.GeoJsonReader();
            return reader.Read<NetTopologySuite.Geometries.Geometry>(json);

        }
        [HttpPost]
        public Response AddPolygon([FromBody] JsonElement requestBody)
        {
            try
            {
                var polygonName = requestBody.GetProperty("PolygonName").GetString();
                var polygonNumber = requestBody.GetProperty("PolygonNumber").GetInt32();
                var locationElement = requestBody.GetProperty("Location");
                var createdAt = DateTime.UtcNow;
                var updatedAt = DateTime.UtcNow;

            

                NetTopologySuite.Geometries.Geometry geometry = ConvertJsonToGeometry(locationElement);

                var polygon = new Polygon
                {
                    PolygonName = polygonName,
                    PolygonNumber = polygonNumber,
                    Location = geometry,
                    CreatedAt = createdAt,
                    UpdatedAt = updatedAt
                };
                Console.WriteLine("coor" + geometry.GetType());

                bool coordinatesExist = geometry == null && _polygonManager.TGetByCoordinate(geometry) != null;

                bool nameExists = !string.IsNullOrEmpty(polygon.PolygonName) && _polygonManager.TGetByName(polygon.PolygonName) != null;
                Console.WriteLine(nameExists);

                Console.WriteLine("Number exists: " + _polygonManager.TGetByNumber(polygonNumber) != null);

                bool numberExists = polygonNumber != null && _polygonManager.TGetByNumber(polygonNumber) != null;




                 if (!coordinatesExist || nameExists|| numberExists)
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
    }
}
