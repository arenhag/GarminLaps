using System;
using Xunit;
using GarminLaps;
using System.Linq;

namespace UnitTests
{
    public class TcxFileReaderTests
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

        [Theory]
        [InlineData("twoLaps.tcx", 1473)]
        [InlineData("onlyTime.tcx", 1)]
        public void Should_have_time_in_all_track_points(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.DateTime != null));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }

        [Theory]
        [InlineData("twoLaps.tcx", 1473)]
        [InlineData("onlyTime.tcx", 0)]
        public void Should_have_non_null_heart_rates_in_track_points_with_heart_rate(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.HeartRateBpm != null));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }

        [Theory]
        [InlineData("twoLaps.tcx", 1473)]
        [InlineData("onlyTime.tcx", 0)]
        public void Should_have_non_null_positions_in_track_points_with_position(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.Position != null));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }
    }
}
