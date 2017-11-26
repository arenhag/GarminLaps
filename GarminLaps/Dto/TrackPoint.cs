using System;

namespace GarminLaps.Dto
{
    public class TrackPoint
    {
        public int? HeartRateBpm { get; set; }
        public DateTime DateTime { get; internal set; }
    }
}