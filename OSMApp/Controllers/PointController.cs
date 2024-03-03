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
        Response response = new Response();
        PointManager _pointManager = new PointManager(new EfPointDal());
        [HttpPost]
        public Response AddPoint([FromBody] Point point)
        {
            try
            {

                bool nameExists = !string.IsNullOrEmpty(point.PointName) && _pointManager.TGetByName(point.PointName) != null;
                bool numberExists = point.PointNumber != null && _pointManager.TGetByNumber(point.PointNumber) != null;
                bool coordinatesExist = point.Latitude != null && point.Longitude != null && _pointManager.TGetByCoordinate(point.Latitude, point.Longitude) != null;


                if (nameExists || numberExists || coordinatesExist)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "The provided point coordinates, name, or number already exist.";
                }
                else
                {
                  
                    _pointManager.TAdd(point);
                    response.Success = true;
                    response.Message = "Point added successfully.";
                    response.Data = point;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                response.Data = null;
            }
            return response;
        }

        [HttpGet]
        public Response QueryPoints([FromQuery] string? PointName, [FromQuery] int? PointNumber, [FromQuery] double? Latitude, [FromQuery] double? Longitude)
        {
            try
            {
                if (string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var points = _pointManager.GetList();
                    response.Data = points;
                    response.Success = true;
                    response.Message = "Get Points list successfully";

                }

                else if (!string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByName = _pointManager.TGetByName(PointName);
                    if (pointGetByName != null)
                    {
                        response.Data = pointGetByName;
                        response.Success = true;
                        response.Message = "Name found";

                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "Name didn't found";

                    }
                }
                else if (string.IsNullOrEmpty(PointName) && PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByNumber = _pointManager.TGetByNumber(PointNumber);
                    if (pointGetByNumber != null)
                    {
                        response.Data = pointGetByNumber;
                        response.Success = true;
                        response.Message = "Number found";

                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "Number didn't found";

                    }
                }
                else if (string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinate = _pointManager.TGetByCoordinate(Latitude, Longitude);
                    if (pointGetByCoordinate != null)
                    {
                        response.Data = pointGetByCoordinate;
                        response.Success = true;
                        response.Message = "Latitude and Longitude found";
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "Latitude and Longitude didn't found";
                    }
                }
                else if (string.IsNullOrEmpty(PointName) && PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinateNumber = _pointManager.TGetByCoordinateNumber(PointNumber, Latitude, Longitude);
                    if (pointGetByCoordinateNumber != null)
                    {
                        response.Data = pointGetByCoordinateNumber;
                        response.Success = true;
                        response.Message = "Coordinate and Number found";
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "Coordinate and Number didn't found";
                    }
                }
                else if (!string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinateName = _pointManager.TGetByCoordinateName(PointName, Latitude, Longitude);
                    if (pointGetByCoordinateName != null)
                    {
                        response.Data = pointGetByCoordinateName;
                        response.Success = true;
                        response.Message = "Coordinate and Name found";
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "Coordinate and Name didn't found";
                    }
                }
                else if (!string.IsNullOrEmpty(PointName) && PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByNumberName = _pointManager.TGetByNumberName(PointName, PointNumber);
                    if (pointGetByNumberName != null)
                    {
                        response.Data = pointGetByNumberName;
                        response.Success = true;
                        response.Message = "Name and Number found";
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "Name and Number didn't found"; 
                    }
                }
                else
                {
                    var pointGetByCoordinateNumberName = _pointManager.TGetByCoordinateNumberName(PointNumber, Latitude, Longitude, PointName);
                    if (pointGetByCoordinateNumberName != null)
                    {
                        response.Data = pointGetByCoordinateNumberName;
                        response.Success = true;
                        response.Message = "Name, Number and Coordinate found";
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "Name, Number and Coordinate didn't found";
                    }
                }
            }
            catch (Exception e)
            {
                response.Data = null;
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }
    }
}
