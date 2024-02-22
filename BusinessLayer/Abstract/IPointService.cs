using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPointService:IGenericService<Point>
    {
        Point TGetByName(string PointName);
        Point TGetByNumber(int? PointNumber);
        Point TGetByCoordinate(double? Latitude, double? Longitude);
        Point TGetByNumberName(string PointName, int? PointNumber);
        Point TGetByCoordinateName(string PointName, double? Latitude, double? Longitude);
        Point TGetByCoordinateNumber(int? PointNumber, double? Latitude, double? Longitude);
        Point TGetByCoordinateNumberName(int? PointNumber, double? Latitude, double? Longitude, string Name);

    }
}
