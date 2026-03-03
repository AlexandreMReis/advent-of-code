using AdvendOfCode.Days;

namespace UnitTests
{
    public class Day06Tests
    {
        [Fact]
        public void RunPart1Demo()
        {
            //Arrange
            var input = GetDemoInput();

            //Act
            var result = new Day06().GetHomeWorkSolutionPartOne(input);

            //Assert
            Assert.Equal(4277556, result);
        }

        [Fact]
        public void RunPart1Final()
        {
            //Arrange
            var input = GetFinalInput();

            //Act
            var result = new Day06().GetHomeWorkSolutionPartOne(input);

            //Assert
            Assert.Equal(5873191732773, result);
        }

        [Fact]
        public void RunPart2Demo()
        {
            //Arrange
            var input = GetDemoPartTwoInput();

            //Act
            var result = new Day06().GetHomeWorkSolutionPartTwo(input);

            //Assert
            Assert.Equal(3263827, result);
        }

        [Fact]
        public void RunPart2Final()
        {
            //Arrange
            var input = GetFinalPartTwoInput();

            //Act
            var result = new Day06().GetHomeWorkSolutionPartTwo(input);

            //Assert
            Assert.Equal(11386445308378, result);
        }

        #region Private Methods

        private List<List<string>> GetDemoInput()
        {
            var inputStr = "123 328  51 64 \r\n 45 64  387 23 \r\n  6 98  215 314\r\n*   +   *   + ";

            var rows = inputStr.Split("\r\n");

            var matrix = new List<List<string>>();
            foreach(var row in rows)
            {
                var columnsByRow = row.Split(' ').Where(el => !string.IsNullOrWhiteSpace(el)).ToList();
                matrix.Add(columnsByRow);
            }

            return matrix;
        }

        private List<string> GetDemoPartTwoInput()
        {
            var inputStr = "123 328  51 64 \r\n 45 64  387 23 \r\n  6 98  215 314\r\n*   +   *   + ";

            return inputStr.Split("\r\n").ToList();
        }

        private List<List<string>> GetFinalInput()
        {
            var inputStr = File.ReadAllText("input-d6.txt");

            var rows = inputStr.Split("\r\n");

            var matrix = new List<List<string>>();
            foreach (var row in rows)
            {
                var columnsByRow = row.Split(' ').Where(el => !string.IsNullOrWhiteSpace(el)).ToList();
                matrix.Add(columnsByRow);
            }

            return matrix;
        }

        private List<string> GetFinalPartTwoInput()
        {
            var inputStr = File.ReadAllText("input-d6.txt");

            return inputStr.Split("\r\n").ToList();
        }

        #endregion Private Methods
    }
}