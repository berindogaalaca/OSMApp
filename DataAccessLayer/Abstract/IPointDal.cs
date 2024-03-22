using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IPointDal:IGenericDal<Point>
    {
        Point GetByName(string PointName);
        Point GetByNumber(int? PointNumber);
        Point GetByCoordinate(double? Latitude, double? Longitude);
        Point GetByNumberName(string PointName, int? PointNumber );
        Point GetByCoordinateName(string PointName, double? Latitude, double? Longitude);
        Point GetByCoordinateNumber(int? PointNumber, double? Latitude, double? Longitude);
        Point GetByCoordinateNumberName(int? PointNumber, double? Latitude, double? Longitude,string Name);
        Point GetById(int? PointId);
    }
}
