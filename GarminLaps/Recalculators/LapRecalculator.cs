using System;
using System.Collections.Generic;
using System.Linq;
using GarminLaps.Dto;

namespace GarminLaps.Recalculators
{
    public class LapRecalculator
    {
        public LapData RedistributeLaps(LapData oldLapDto, List<TimeSpan> durationsForNewLaps)
        {
            var newLapDto = new LapData();

            foreach (var timedLap in durationsForNewLaps)
            {
                newLapDto.Laps.Add(new Lap());
            }

            newLapDto.Laps.Add(new Lap());
            
            var sumOfCalories = oldLapDto.Laps.Sum(a => a.Calories);
            var sumOfHeartRates = oldLapDto.Laps.SelectMany(a => a.TrackPoints).Sum(b => b.HeartRateBpm);

            return newLapDto;
        }
    }
}