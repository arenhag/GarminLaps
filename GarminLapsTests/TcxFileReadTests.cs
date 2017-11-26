using System;
using Xunit;
using GarminLaps;
using System.Linq;

namespace UnitTests
{
    public class TcxFileReaderTests
    {
        [Fact]
        public void ShouldBeAbleToSumIntegersProperly()
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();
            var int1 = 10;
            var int2 = 20;
            var expectedResult = 30;

            // Act
            var actualResult = tcxFileReader.JunkMethodForTest(int1, int2);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("twoLaps.tcx",2)]
        public void ShouldReturnCorrectNumberOfLaps(string testFileLocation, int expectedResult)
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
        public void ShouldReturnCorrectNumberOfTrackPointsPerLap(string testFileLocation, int lapNumber, int trackPointCount)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();
            var expectedResult = trackPointCount;

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);

            // Assert
            Assert.Equal(expectedResult, actualResult.Laps[lapNumber].TrackPoints.Count);
        }

        [Fact]
        public void ShouldHaveNonDefaultHeartRatesInTrackPointsWithHeartRate()
        {
            // Arrange
            var testFileLocation = "noHeartRateBpm.tcx";
            var tcxFileReader = new TcxFileReader();
            var expectedResult = 0;

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.HeartRateBpm != null && b.HeartRateBpm < 1));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }

        [Fact]
        public void ShouldHaveTimeInAllTrackPoints()
        {
            // Arrange
            var testFileLocation = "twoLaps.tcx";
            var tcxFileReader = new TcxFileReader();
            var expectedResult = 0;

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.DateTime == DateTime.MinValue));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }
    }
}
