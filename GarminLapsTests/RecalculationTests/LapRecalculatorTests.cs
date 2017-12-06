using System;
using System.Collections.Generic;
using GarminLaps.Dto;
using GarminLaps.Recalculators;
using Xunit;

namespace GarminLapsTests.RecalculationTests
{
    public class LapRecalculatorTests
    {
        [Fact]
        public void Should_something()
        {
            // Arrange
            var lapRecalculator = new LapRecalculator();
            var oldLapDto = SetupFakeLapData(2, 1000);

            var durationsForNewLaps = new List<TimeSpan>()
            {
                new TimeSpan(0,0,14),
                new TimeSpan(0,0,12),
                new TimeSpan(0,0,12),
                new TimeSpan(0,0,14),
                new TimeSpan(0,0,12),
                new TimeSpan(0,0,14),
                new TimeSpan(0,0,12),
                new TimeSpan(0,0,14)
            };
            
            LapData newLapDto = lapRecalculator.RedistributeLaps(oldLapDto, durationsForNewLaps);

            // Act
            // 2) tell "something" to redistribute the laps according to a new given pattern
                // make this persist in the DTO model
                // check that this "something" has performed correct calculations regarding heart rates and calories

            // Assert
            Assert.False(true);
        }

        private LapData SetupFakeLapData(int numberOfLaps, int numberOfTrackPointsPerLap)
        {
            var lapDataToReturn = new LapData();
            
            for (int i = 0; i < numberOfLaps; i++)
            {
                lapDataToReturn.Laps.Add(NewMethod(ReturnTrackPoints(numberOfTrackPointsPerLap)));
            }

            return lapDataToReturn;
        }

        private static Lap NewMethod(List<TrackPoint> aBunchOfTrackPoints)
        {
            return new Lap()
            {
                Calories = 123,
                TrackPoints = aBunchOfTrackPoints
            };
        }

        private static List<TrackPoint> ReturnTrackPoints(int numberOfTrackPoints)
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