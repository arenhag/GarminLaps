using System;

namespace GarminLaps.Dto
{
    public class TrackPoint
    {
        public DateTime DateTime { get; internal set; }
        public int? HeartRateBpm { get; set; }
        public Position Position { get; set; }
    }
}