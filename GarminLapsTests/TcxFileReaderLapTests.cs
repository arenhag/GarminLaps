using System;
using Xunit;
using GarminLaps;
using System.Linq;
using GarminLaps.Dto;

namespace UnitTests
{
    public class TcxFileReaderLapTests
    {
        [Theory]
        [InlineData("twoLaps.tcx",2)]
        public void Should_return_correct_number_of_laps(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();
            
            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);

            // Assert
            Assert.Equal(expectedResult, actualResult.Laps.Count);
        }

        [Theory]
        [InlineData("twoLaps.tcx", 0, 801)]
        [InlineData("twoLaps.tcx", 1, 672)]
        public void Should_return_correct_number_of_track_points_per_lap(string testFileLocation, int lapNumber, int trackPointCount)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();
            var expectedResult = trackPointCount;

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);

            // Assert
            Assert.Equal(expectedResult, actualResult.Laps[lapNumber].TrackPoints.Count);
        }
    }
}
