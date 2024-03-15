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
        Response responseMessage = new Response();
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
                    responseMessage.Data = null;
                    responseMessage.Success = false;
                    responseMessage.Message = "The provided point coordinates, name, or number already exist.";
                }
                else
                {
                  
                    _pointManager.TAdd(point);
                    responseMessage.Success = true;
                    responseMessage.Message = "Point added successfully.";
                    responseMessage.Data = point;
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

        [HttpGet]
        public Response QueryPoints([FromQuery] string? PointName, [FromQuery] int? PointNumber, [FromQuery] double? Latitude, [FromQuery] double? Longitude)
        {
            try
            {
                if (string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var points = _pointManager.GetList();
                    responseMessage.Data = points;
                    responseMessage.Success = true;
                    responseMessage.Message = "Get Points list successfully";

                }

                else if (!string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByName = _pointManager.TGetByName(PointName);
                    if (pointGetByName != null)
                    {
                        responseMessage.Data = pointGetByName;
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
                else if (string.IsNullOrEmpty(PointName) && PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByNumber = _pointManager.TGetByNumber(PointNumber);
                    if (pointGetByNumber != null)
                    {
                        responseMessage.Data = pointGetByNumber;
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
                else if (string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinate = _pointManager.TGetByCoordinate(Latitude, Longitude);
                    if (pointGetByCoordinate != null)
                    {
                        responseMessage.Data = pointGetByCoordinate;
                        responseMessage.Success = true;
                        responseMessage.Message = "Latitude and Longitude found";
                    }
                    else
                    {
                        responseMessage.Data = null;
                        responseMessage.Success = false;
                        responseMessage.Message = "Latitude and Longitude didn't found";
                    }
                }
                else if (string.IsNullOrEmpty(PointName) && PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinateNumber = _pointManager.TGetByCoordinateNumber(PointNumber, Latitude, Longitude);
                    if (pointGetByCoordinateNumber != null)
                    {
                        responseMessage.Data = pointGetByCoordinateNumber;
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
                else if (!string.IsNullOrEmpty(PointName) && !PointNumber.HasValue && Latitude.HasValue && Longitude.HasValue)
                {
                    var pointGetByCoordinateName = _pointManager.TGetByCoordinateName(PointName, Latitude, Longitude);
                    if (pointGetByCoordinateName != null)
                    {
                        responseMessage.Data = pointGetByCoordinateName;
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
                else if (!string.IsNullOrEmpty(PointName) && PointNumber.HasValue && !Latitude.HasValue && !Longitude.HasValue)
                {
                    var pointGetByNumberName = _pointManager.TGetByNumberName(PointName, PointNumber);
                    if (pointGetByNumberName != null)
                    {
                        responseMessage.Data = pointGetByNumberName;
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
                    var pointGetByCoordinateNumberName = _pointManager.TGetByCoordinateNumberName(PointNumber, Latitude, Longitude, PointName);
                    if (pointGetByCoordinateNumberName != null)
                    {
                        responseMessage.Data = pointGetByCoordinateNumberName;
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
    }
}
