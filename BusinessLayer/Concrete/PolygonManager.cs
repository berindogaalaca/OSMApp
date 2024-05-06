using BusinessLayer.Abstract;
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
    public class PolygonManager : IPolygonService
    {
        private readonly IPolygonDal _PolygonDal;

        public PolygonManager(Context context)
        {
            _PolygonDal = (IPolygonDal?)(context ?? throw new ArgumentNullException(nameof(context)));

        }

        public PolygonManager(IPolygonDal polygonDal)
        {
            _PolygonDal = polygonDal ?? throw new ArgumentNullException(nameof(polygonDal));
        }


        public List<Polygon> GetList()
        {
            return _PolygonDal.GetList();
        }

        public void TAdd(Polygon t)
        {
            _PolygonDal.Insert(t);
        }

        public void TDelete(Polygon t)
        {
            _PolygonDal.Delete(t);
        }

        public Polygon TGetByID(int? id)
        {
            return _PolygonDal.GetById(id);
        }

        public void TUpdate(Polygon t)
        {
            _PolygonDal.Update(t);
        }
        public Polygon TGetByName(string PolygonName)
        {
            return _PolygonDal.GetByName(PolygonName);
        }

        public Polygon TGetByNumber(int? PolygonNumber)
        {
            return _PolygonDal.GetByNumber(PolygonNumber);
        }
        public Polygon TGetByCoordinate(NetTopologySuite.Geometries.Geometry? Location)
        {
            return _PolygonDal.GetByCoordinate(Location);
        }

        public List<Polygon> TGetPolygonList()
        {
            return _PolygonDal.GetPolygonList();
        }

        public Polygon TGetByNumberName(string PolygonName, int? PolygonNumber)
        {
            return _PolygonDal.GetByNumberName(PolygonName, PolygonNumber);
        }

        public Polygon TGetByCoordinateName(string PolygonName, NetTopologySuite.Geometries.Geometry? Location)
        {
            return _PolygonDal.GetByCoordinateName(PolygonName, Location);

        }

        public Polygon TGetByCoordinateNumber(int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location)
        {
            return _PolygonDal.GetByCoordinateNumber(PolygonNumber, Location);
        }

        public Polygon TGetByCoordinateNumberName(int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location, string PolygonName)
        {
            return _PolygonDal.GetByCoordinateNumberName(PolygonNumber, Location, PolygonName);
        }
    }
}
