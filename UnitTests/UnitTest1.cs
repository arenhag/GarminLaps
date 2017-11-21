using System;
using Xunit;
using GarminLaps;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var blaha = new XmlParsing();
            var int1 = 10;
            var int2 = 20;
            var expectedResult = 30;

            // Act
            var actualResult = blaha.JunkMethodForTest(int1, int2);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
