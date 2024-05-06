using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IPolygonDal:IGenericDal<Polygon>
    {
        Polygon GetByName(string PolygonName);
        Polygon GetByNumber(int? PolygonNumber);
        Polygon GetById(int? PolygonId);
        Polygon GetByCoordinate(NetTopologySuite.Geometries.Geometry? Location);
        List<Polygon> GetPolygonList();
        Polygon GetByNumberName(string PolygonName, int? PolygonNumber);
        Polygon GetByCoordinateName(string PolygonName, NetTopologySuite.Geometries.Geometry? Location);
        Polygon GetByCoordinateNumber(int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location);
        Polygon GetByCoordinateNumberName(int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location, string PolygonName);
    }
}
