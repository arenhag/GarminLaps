using System.Collections.Generic;

namespace GarminLaps.Dto
{
    public class LapData
    {
        public LapData()
        {
            Laps = new List<Lap>();
        }

        public List<Lap> Laps { get; set; }
    }
}