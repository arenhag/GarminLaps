using System;
using System.Collections.Generic;

namespace GarminLaps.Dto
{
    public class Lap
    {
        public Lap()
        {
            TrackPoints = new List<TrackPoint>();
        }
        
        public List<TrackPoint> TrackPoints { get; set; }
        public UInt16 Calories { get; set; }
    }
}