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
            var allTrackPoints = oldLapDto.Laps.SelectMany(a => a.TrackPoints);

            var upcomingLapStartTime = oldLapDto.Laps[0].TrackPoints[0].DateTime.Subtract(new TimeSpan(1)); // remove one tick to make sure that the first track point of all gets included

            foreach (var timedLap in durationsForNewLaps)
            {
                var upcomingLapEndTime = upcomingLapStartTime.Add(timedLap);
                var trackPointsToTransplantToNewLap = allTrackPoints.Where(a => a.DateTime > upcomingLapStartTime && a.DateTime <= upcomingLapEndTime);

                newLapDto.Laps.Add(new Lap()
                {
                    TrackPoints = trackPointsToTransplantToNewLap.ToList()
                });

                upcomingLapStartTime = upcomingLapEndTime;
            }

            // Make sure that any track points not covered by the stated lap times are appended to a last lap
            var remainingTrackPoints = allTrackPoints.Where(a => a.DateTime > upcomingLapStartTime);

            if (remainingTrackPoints.Count() > 0)
            {
                var trackPointsToTransplantToNewLap = allTrackPoints.Where(a => a.DateTime >= upcomingLapStartTime);

                newLapDto.Laps.Add(new Lap()
                {
                    TrackPoints = trackPointsToTransplantToNewLap.ToList()
                });
            }

            //var sumOfCalories = oldLapDto.Laps.Sum(a => a.Calories);
            //var sumOfHeartRates = oldLapDto.Laps.SelectMany(a => a.TrackPoints).Sum(b => b.HeartRateBpm);

            return newLapDto;
        }
    }
}