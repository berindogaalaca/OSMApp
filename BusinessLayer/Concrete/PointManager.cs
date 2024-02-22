using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
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
        IPointDal _PointDal;
        public PointManager(IPointDal pointDal)
        {
            _PointDal = pointDal;
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

        public Point TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Point t)
        {
            _PointDal.Update(t);
        }
    }
}
