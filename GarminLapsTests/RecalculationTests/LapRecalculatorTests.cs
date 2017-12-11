using System;
using System.Collections.Generic;
using System.Linq;
using GarminLaps.Dto;
using GarminLaps.Recalculators;
using Xunit;

namespace GarminLapsTests.RecalculationTests
{
    public class LapRecalculatorTests
    {
        [Fact]
        public void Should_return_correct_number_of_laps()
        {
            // Arrange
            var lapRecalculator = new LapRecalculator();
            var oldLapDto = FakeLapSetup.SetupFakeLapData(2, 1000);

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

            // Act
            LapData newLapDto = lapRecalculator.RedistributeLaps(oldLapDto, durationsForNewLaps);

            // Assert
            Assert.Equal(durationsForNewLaps.Count + 1, newLapDto.Laps.Count);
        }

        [Fact]
        public void Should_return_atleast_one_track_point_in_all_laps()
        {
            // Arrange
            var lapRecalculator = new LapRecalculator();
            var oldLapDto = FakeLapSetup.SetupFakeLapData(2, 1000);

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

            // Act
            LapData newLapDto = lapRecalculator.RedistributeLaps(oldLapDto, durationsForNewLaps);
            var expected = newLapDto.Laps.Count();
            var actual = newLapDto.Laps.Where(a => a.TrackPoints.Count > 0).Count();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_not_remove_nor_add_any_track_points()
        {
            // Arrange
            var lapRecalculator = new LapRecalculator();
            var oldLapDto = FakeLapSetup.SetupFakeLapData(2, 1000);

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

            // Act
            LapData newLapDto = lapRecalculator.RedistributeLaps(oldLapDto, durationsForNewLaps);

            var expected = oldLapDto.Laps.SelectMany(a => a.TrackPoints).Count();
            var actual = newLapDto.Laps.SelectMany(a => a.TrackPoints).Count();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_not_remove_nor_add_any_heart_rates()
        {
            // Arrange
            var lapRecalculator = new LapRecalculator();
            var oldLapDto = FakeLapSetup.SetupFakeLapData(2, 1000);

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

            // Act
            LapData newLapDto = lapRecalculator.RedistributeLaps(oldLapDto, durationsForNewLaps);

            var expected = oldLapDto.Laps.SelectMany(a => a.TrackPoints).Sum(b => b.HeartRateBpm);
            var actual = newLapDto.Laps.SelectMany(a => a.TrackPoints).Sum(b => b.HeartRateBpm);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_not_change_calorie_totals()
        {
            // Arrange
            var lapRecalculator = new LapRecalculator();
            var oldLapDto = FakeLapSetup.SetupFakeLapData(2, 1000);

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

            // Act
            LapData newLapDto = lapRecalculator.RedistributeLaps(oldLapDto, durationsForNewLaps);
            var expected = oldLapDto.Laps.Sum(a => a.Calories);
            var actual = newLapDto.Laps.Sum(a => a.Calories);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}