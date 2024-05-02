using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Polygon
    {
        [Key]
        public int PolygonId { get; set; }
        public string PolygonName { get; set; }
        public int PolygonNumber { get; set; }
        [Column(TypeName = "geometry(Polygon, 4326)")]
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
