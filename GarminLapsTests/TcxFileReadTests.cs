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

        [Fact]
        public void ShouldReturnCorrectNumberOfLaps()
        {
            // Arrange
            var blaha = new TcxFileReader();
            var expectedResult = 2;
            var testFileLocation = "twoLaps.tcx";

            // Act
            var actualResult = blaha.ReadTcxFile(testFileLocation);

            // Assert
            Assert.Equal(expectedResult, actualResult.Laps.Count);
        }
    }
}
