using AdvendOfCode.Days;

namespace UnitTests
{
    public class Day01Tests
    {
        [Fact]
        public void RunDemo()
        {
            //Arrange
            var movements = GetDemoInput();

            //Act
            var result = (new Day01()).CalculateTimesReachedOrPassedZero(movements);

            //Assert
            Assert.Equal(6, result);
        }

        [Fact]
        public void RunFinal()
        {
            //Arrange
            var movements = GetFinalInput();

            //Act
            var result = (new Day01()).CalculateTimesReachedOrPassedZero(movements);

            //Assert
            Assert.Equal(6638, result);
        }

        #region Private Methods

        private List<string> GetDemoInput()
        {
            string input = "L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82";
            return input.Split("\r\n").ToList();
        }

        private List<string> GetFinalInput()
        {
            string input = File.ReadAllText("input-d1.txt");
            return input.Split("\r\n").ToList();
        }

        #endregion Private Methods
    }
}