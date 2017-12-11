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
            // 2) tell the recalculator to redistribute the laps according to a new given pattern
                // make this persist in the DTO model
                // check that this "something" has performed correct calculations regarding heart rates and calories
            LapData newLapDto = lapRecalculator.RedistributeLaps(oldLapDto, durationsForNewLaps);

            // Assert
            Assert.Equal(durationsForNewLaps.Count + 1, newLapDto.Laps.Count);
        }
    }
}