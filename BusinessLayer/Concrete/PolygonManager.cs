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
            Console.WriteLine(PolygonName.GetType());
            return _PolygonDal.GetByName(PolygonName);
        }

        public Polygon TGetByNumber(int? PolygonNumber)
        {
            Console.WriteLine("tget"+PolygonNumber);
            Console.WriteLine("getby" + _PolygonDal.GetByNumber(PolygonNumber));
            return _PolygonDal.GetByNumber(PolygonNumber);
        }
        public Polygon TGetByCoordinate(NetTopologySuite.Geometries.Geometry? Location)
        {
            return _PolygonDal.GetByCoordinate(Location);
        }

    }
}
