using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using OSMApp.Models;
using System;

namespace OSMApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : Controller
    {
        
        PointManager _pointManager = new PointManager(new EfPointDal());
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
                    Longitude = point.Longitude
                };

                _pointManager.TAdd(createPoint);

                return new Response { Data = createPoint, Message = "Point added successfully.", Success = true };
            }
            catch (Exception e)
            {
                return new Response { Data = null, Message = e.Message, Success = false };
            }
        }

        [HttpGet]
        public Response QueryPoints([FromQuery] string? PointName, [FromQuery] int? PointNumber, [FromQuery] double? Latitude, [FromQuery] double? Longitude)
        {
            try
            {
                if (string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var points = _pointManager.GetList();
                    return new Response { Data = points, Message = "Get Points list successfully", Success = true };
                }

                else if (!string.IsNullOrEmpty(PointName)&& !PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByName = _pointManager.TGetByName(PointName);
                    if (pointGetByName != null)
                    {
                        return new Response { Data = pointGetByName, Message = "Name found", Success = true };
                    }
                    else
                    {
                        return new Response { Data = null, Message = "Name didn't find", Success = false };
                    }
                }
                else if (string.IsNullOrEmpty(PointName) && PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByNumber = _pointManager.TGetByNumber(PointNumber);
                    if (pointGetByNumber != null)
                    {
                        return new Response { Data = pointGetByNumber, Message = "Number found", Success = true };
                    }
                    else
                    {
                        return new Response { Data = null, Message = "Number didn't find", Success = false };
                    }
                }
                else if (string.IsNullOrEmpty(PointName) && !PointNumber.HasValue &&Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinate = _pointManager.TGetByCoordinate(Latitude, Longitude);
                    if (pointGetByCoordinate != null)
                    {
                        return new Response { Data = pointGetByCoordinate, Message = "Latitude and Longitude found", Success = true };
                    }
                    else
                    {
                        return new Response { Data = null, Message = "Latitude and Longitude didn't find", Success = false };
                    }
                }
                else if (string.IsNullOrEmpty(PointName) && PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinateNumber = _pointManager.TGetByCoordinateNumber(PointNumber, Latitude, Longitude);
                    if (pointGetByCoordinateNumber != null)
                    {
                        return new Response { Data = pointGetByCoordinateNumber, Message = "Coordinate and Number found", Success = true };
                    }
                    else
                    {
                        return new Response { Data = null, Message = "Coordinate and Number didn't find", Success = false };
                    }
                }
                else if (!string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinateName = _pointManager.TGetByCoordinateName(PointName, Latitude, Longitude);
                    if (pointGetByCoordinateName != null)
                    {
                        return new Response { Data = pointGetByCoordinateName, Message = "Coordinate and Name found", Success = true };
                    }
                    else
                    {
                        return new Response { Data = null, Message = "Coordinate and Name didn't find", Success = false };
                    }
                }
                else if (!string.IsNullOrEmpty(PointName) && PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByNumberName = _pointManager.TGetByNumberName(PointName, PointNumber);
                    if (pointGetByNumberName != null)
                    {
                        return new Response { Data = pointGetByNumberName, Message = "Name and Number found", Success = true };
                    }
                    else
                    {
                        return new Response { Data = null, Message = "Name and Number didn't find", Success = false };
                    }

                }
                else
                {
                    var pointGetByCoordinateNumberName = _pointManager.TGetByCoordinateNumberName(PointNumber, Latitude, Longitude, PointName);
                    if (pointGetByCoordinateNumberName != null)
                    {
                        return new Response { Data = pointGetByCoordinateNumberName, Message = "Name, Number and Coordinate found", Success = true };
                    }
                    else
                    {
                        return new Response { Data = null, Message = "Name, Number and Coordinate didn't find", Success = false };
                    }
                }
            }
            catch (Exception e)
            {
                return new Response { Data = null, Message = e.Message, Success = false };
            }
        }
    }
}
