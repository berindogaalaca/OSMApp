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
    }
}
