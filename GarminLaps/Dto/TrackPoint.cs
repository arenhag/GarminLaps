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
        public SensorState SensorState { get; set; }
        public Byte? Cadence { get; set; }
        public string Extensions { get; set; }
    }
}