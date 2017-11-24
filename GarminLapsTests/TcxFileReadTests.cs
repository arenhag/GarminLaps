using System;
using Xunit;
using GarminLaps;

namespace UnitTests
{
    public class TcxFileReaderTests
    {
        [Fact]
        public void ShouldBeAbleToSumIntegersProperly()
        {
            // Arrange
            var blaha = new TcxFileReader();
            var int1 = 10;
            var int2 = 20;
            var expectedResult = 30;

            // Act
            var actualResult = blaha.JunkMethodForTest(int1, int2);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("twoLaps.tcx",2)]
        public void ShouldReturnCorrectNumberOfLaps(string testFileLocation, int expectedResult)
        {
            // Arrange
            var blaha = new TcxFileReader();
            
            // Act
            var actualResult = blaha.ReadTcxFile(testFileLocation);

            // Assert
            Assert.Equal(expectedResult, actualResult.Laps.Count);
        }

        [Theory]
        [InlineData("twoLaps.tcx", 0, 801)]
        [InlineData("twoLaps.tcx", 1, 672)]
        public void ShouldReturnCorrectNumberOfTrackPointsPerLap(string testFileLocation, int lapNumber, int trackPointCount)
        {
            // Arrange
            var blaha = new TcxFileReader();
            var expectedResult = trackPointCount;

            // Act
            var actualResult = blaha.ReadTcxFile(testFileLocation);

            // Assert
            Assert.Equal(expectedResult, actualResult.Laps[lapNumber].TrackPoints.Count);
        }
    }
}
