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
    }
}
