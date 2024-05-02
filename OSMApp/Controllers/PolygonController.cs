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

        [HttpPost]
        public Response AddPolygon([FromBody] Polygon polygon)
        {
            try
            {
              
               
               // bool nameExists = !string.IsNullOrEmpty(polygon.PolygonName) && _polygonManager.TGetByName(polygon.PolygonName) != null;
                //Console.WriteLine(_polygonManager.TGetByName(polygon.PolygonName));
               // bool numberExists = polygon.PolygonNumber != null && _polygonManager.TGetByNumber(polygon.PolygonNumber) != null;
                //Console.WriteLine("Number exists: " + _polygonManager.TGetByNumber(polygon.PolygonNumber) != null);
                bool coordinatesExist = polygon.Location == null && _polygonManager.TGetByCoordinate(polygon.Location) != null;
                Console.WriteLine("coor" + coordinatesExist) ;

                if (!coordinatesExist )
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
                responseMessage.Data = polygon;
            }
            return responseMessage;
        }
    }
}