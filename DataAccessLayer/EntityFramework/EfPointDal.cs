using DataAccessLayer.Abstract;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfPointDal : GenericRepository<Point>, IPointDal
    {
        IPointDal _PointDal;

        public Point GetByCoordinate(double? Latitude, double? Longitude)
        {
            throw new NotImplementedException();
        }

        public Point GetByCoordinateName(string PointName, double? Latitude, double? Longitude)
        {
            throw new NotImplementedException();
        }

        public Point GetByCoordinateNumber(int? PointNumber, double? Latitude, double? Longitude)
        {
            throw new NotImplementedException();
        }

        public Point GetByCoordinateNumberName(int? PointNumber, double? Latitude, double? Longitude, string Name)
        {
            throw new NotImplementedException();
        }

        public Point GetByName(string PointName)
        {
            return _PointDal.GetByName(PointName);
        }



        public Point GetByNumber(int? PointNumber)
        {
            throw new NotImplementedException();
        }


        public Point GetByNumberName(string PointName, int? PointNumber)
        {
            throw new NotImplementedException();
        }



    }
}
