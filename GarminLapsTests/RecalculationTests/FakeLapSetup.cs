using System;
using System.Collections.Generic;
using GarminLaps.Dto;

namespace GarminLapsTests.RecalculationTests
{
    internal static class FakeLapSetup
    {
        internal static LapData SetupFakeLapData(int numberOfLaps, int numberOfTrackPointsPerLap)
        {
            var lapDataToReturn = new LapData();
            
            for (int i = 0; i < numberOfLaps; i++)
            {
                lapDataToReturn.Laps.Add(SetupFakeLap(SetupFakeTrackPoints(numberOfTrackPointsPerLap)));
            }

            return lapDataToReturn;
        }

        internal static Lap SetupFakeLap(List<TrackPoint> aBunchOfTrackPoints)
        {
            return new Lap()
            {
                Calories = 123,
                TrackPoints = aBunchOfTrackPoints
            };
        }

        internal static List<TrackPoint> SetupFakeTrackPoints(int numberOfTrackPoints)
        {
            var aBunchOfTrackPoints = new List<TrackPoint>();
            var startTime = DateTime.Now;

            for (int i = 0; i < numberOfTrackPoints; i++)
            {
                aBunchOfTrackPoints.Add(new TrackPoint()
                {
                    AltitudeMeters = 100,
                    DistanceMeters = 20,
                    HeartRateBpm = 110,
                    SensorState = SensorState.Present,
                    DateTime = startTime.AddSeconds(i)
                });
            }

            return aBunchOfTrackPoints;
        }
    }
}