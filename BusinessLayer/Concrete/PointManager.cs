﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PointManager : IPointService
    {
        private readonly IPointDal _PointDal;

        public PointManager(Context context)
        {
            _PointDal = (IPointDal?)(context ?? throw new ArgumentNullException(nameof(context)));
            
        }

        public PointManager(IPointDal pointDal)
        {
            _PointDal = pointDal ?? throw new ArgumentNullException(nameof(pointDal));
        }


        public List<Point> GetList()
        {
           return _PointDal.GetList();
        }

        public void TAdd(Point t)
        {
           _PointDal.Insert(t);
        }

        public void TDelete(Point t)
        {
            _PointDal.Delete(t);
        }

        public Point TGetByID(int? id)
        {
            return _PointDal.GetById(id);
        }

        public Point TGetByCoordinate(double? Latitude , double? Longitude)
        {
            return _PointDal.GetByCoordinate(Latitude,Longitude);
        }

       public Point TGetByName(string PointName)
        {
        
             return _PointDal.GetByName(PointName);
        }
 
        public void TUpdate(Point t)
        {
            _PointDal.Update(t);
        }

        public Point TGetByNumber(int? PointNumber)
        {
            return _PointDal.GetByNumber(PointNumber);
        }

        public Point TGetByNumberName(string PointName, int? PointNumber)
        {
            return _PointDal.GetByNumberName(PointName,PointNumber);
        }

        public Point TGetByCoordinateName(string PointName, double? Latitude, double? Longitude)
        {
            return _PointDal.GetByCoordinateName(PointName, Latitude, Longitude);
        }

        public Point TGetByCoordinateNumber(int? PointNumber, double? Latitude, double? Longitude)
        {
            return _PointDal.GetByCoordinateNumber(PointNumber, Latitude, Longitude);
        }

        public Point TGetByCoordinateNumberName(int? PointNumber, double? Latitude, double? Longitude, string Name)
        {
            return _PointDal.GetByCoordinateNumberName(PointNumber, Latitude, Longitude, Name);
        }
    }
}
