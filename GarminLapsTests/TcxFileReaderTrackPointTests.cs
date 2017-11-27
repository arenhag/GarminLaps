using System;
using Xunit;
using GarminLaps;
using System.Linq;
using GarminLaps.Dto;

namespace UnitTests
{
    public class TcxFileReaderTrackPointTests
    {
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

        [Theory]
        [InlineData("twoLaps.tcx", 1473)]
        [InlineData("onlyTime.tcx", 0)]
        public void Should_have_non_null_altitude_meters_in_track_points_with_altitude_meters(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.AltitudeMeters != null));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }

        [Theory]
        [InlineData("twoLaps.tcx", 1473)]
        [InlineData("onlyTime.tcx", 0)]
        public void Should_have_non_null_distance_meters_in_track_points_with_distance_meters(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.DistanceMeters != null));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }

        [Theory]
        [InlineData("twoLaps.tcx", 1)]
        [InlineData("onlyTime.tcx", 0)]
        public void Should_have_non_default_sensor_state_in_track_points_with_sensor_state(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.SensorState != SensorState.None));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }

        [Theory]
        [InlineData("twoLaps.tcx", 1)]
        [InlineData("onlyTime.tcx", 0)]
        public void Should_have_non_null_cadence_in_track_points_with_cadence(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.Cadence != null));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }

        [Theory]
        [InlineData("twoLaps.tcx", 1473)]
        [InlineData("onlyTime.tcx", 0)]
        public void Should_have_non_null_extensions_in_track_points_with_extensions(string testFileLocation, int expectedResult)
        {
            // Arrange
            var tcxFileReader = new TcxFileReader();

            // Act
            var actualResult = tcxFileReader.ReadTcxFile(testFileLocation);
            var allTrackPoints = actualResult.Laps.SelectMany(a => a.TrackPoints.Where(b => b.Extensions != null && b.Extensions.StartsWith("<Extensions")));
            var actualResultCount = allTrackPoints.Count();

            // Assert
            Assert.Equal(expectedResult, actualResultCount);
        }
    }
}
