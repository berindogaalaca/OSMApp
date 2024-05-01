using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Polygon
    {
        [Key]
        public int PolygonId { get; set; }

        public string PolygonName { get; set; }

        public int PolygonNumber { get; set; }

        public Geometry Location { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
