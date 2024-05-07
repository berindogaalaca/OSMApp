using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPolygonService : IGenericService<Polygon>
    {
        Polygon TGetByName(string PolygonName);
        Polygon TGetByNumber(int? PolygonNumber);
        Polygon TGetByID(int? PolygonId);
        Polygon TGetByCoordinate(NetTopologySuite.Geometries.Geometry? Location);
        List<Polygon> TGetPolygonList();
        Polygon TGetByNumberName(string PolygonName, int? PolygonNumber);
        Polygon TGetByCoordinateName(string PolygonName, NetTopologySuite.Geometries.Geometry? Location);
        Polygon TGetByCoordinateNumber(int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location);
        Polygon TGetByCoordinateNumberName(int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location, string PolygonName);
        Polygon UpdatePolygon(int? PolygonId, int? PolygonNumber, NetTopologySuite.Geometries.Geometry? Location, string PolygonName);

    }
}
