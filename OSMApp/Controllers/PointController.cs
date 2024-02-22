using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using OSMApp.Models;
using System.Diagnostics;

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


    }
}
