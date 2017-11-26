using System;

namespace GarminLaps.Dto
{
    public class TrackPoint
    {
        public DateTime DateTime { get; set; }
        public int? HeartRateBpm { get; set; }
        public Position Position { get; set; }
        public double? AltitudeMeters { get; set; }
        public double? DistanceMeters { get; set; }
    }
}