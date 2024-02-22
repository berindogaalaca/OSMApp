using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using OSMApp.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace OSMApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : Controller
    {
        PointManager pointManager = new PointManager(new EfPointDal());

        [HttpPost]
        public Response AddPoint([FromBody] Point point)
        {
            try
            {

                var createPoint = new Point
                {
                    PointName = point.PointName,
                    PointNumber = point.PointNumber,
                    Latitude = point.Latitude,
                    Longitude = point.Longitude,



                };
                pointManager.TAdd(createPoint);
                return new Response { Data = createPoint, Message = "Point added successfully.", Success = true };
            }
            catch (Exception e)
            {
                return new Response { Data = null, Message = e.Message, Success = false };
            }
        }

      /*  [HttpGet]
        public Response GetPoints()
        {
            var points = pointManager.GetList();
            return new Response { Data = points, Message="Get Points list successfully",Success = true };
        }*/
        [HttpGet]
        public Response QueryPoints([FromQuery] string? PointName, [FromQuery] int? PointNumber, [FromQuery] double? Latitude, [FromQuery] double? Longitude)
        {
            try
            {
                if (string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var points = pointManager.GetList();
                    return new Response { Data = points, Message = "Get Points list successfully", Success = false };
                }
                else if (!string.IsNullOrEmpty(PointName))
                {
                    var pointGetByName = pointManager.TGetByName(PointName);
                    return new Response { Data = pointGetByName, Message = "Name found", Success = true };
                }
                else if (PointNumber.HasValue)
                {
                    var pointGetByNumber = pointManager.TGetByNumber(PointNumber);
                    return new Response { Data = pointGetByNumber, Message = "Number found", Success = true };
                }
                else if (Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinate = pointManager.TGetByCoordinate(Latitude, Longitude);
                    return new Response { Data = pointGetByCoordinate, Message = "Latitude found", Success = true };
                }
                else if (PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinateNuber = pointManager.TGetByCoordinateNumber(PointNumber, Latitude, Longitude);
                    return new Response { Data = pointGetByCoordinateNuber, Message = "Coordinate and Number found", Success = true };
                }
                else if (!string.IsNullOrEmpty(PointName) && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinateName = pointManager.TGetByCoordinateName(PointName, Latitude, Longitude);
                    return new Response { Data = pointGetByCoordinateName, Message = "Coordinate and Name found", Success = true };
                }
                else if (!string.IsNullOrEmpty(PointName) && PointNumber.HasValue)
                {
                    var pointGetByNumberName = pointManager.TGetByNumberName(PointName, PointNumber);
                    return new Response { Data = pointGetByNumberName, Message = "Name and Number found", Success = true };
                }
                else 
                {
                    var pointGetByCoordinateNumberName = pointManager.TGetByCoordinateNumberName(PointNumber, Latitude, Longitude, PointName);
                    return new Response { Data = pointGetByCoordinateNumberName, Message = "Name, Number and Coordinate found", Success = true };
                }
            }

            catch (Exception e)
            {
                return new Response { Data = null, Message = e.Message, Success = false };
            }

        }

    }
}
