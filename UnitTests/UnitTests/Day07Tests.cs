using AdvendOfCode.Days;

namespace UnitTests
{
    public class Day07Tests
    {
        [Fact]
        public void RunPart1Demo()
        {
            //Arrange
            var input = GetDemoInput();

            //Act
            var result = new Day07().GetTimesBeamWasSplittedPartOne(input);

            //Assert
            Assert.Equal(21, result);
        }

        [Fact]
        public void RunPart1Final()
        {
            //Arrange
            var input = GetFinalInput();

            //Act
            var result = new Day07().GetTimesBeamWasSplittedPartOne(input);

            //Assert
            Assert.Equal(1518, result);
        }

        [Fact]
        public void RunPart2Demo()
        {
            //Arrange
            var input = GetDemoInput();

            //Act
            var result = new Day07().GetHowManyDifferentTimelinesPartTwo(input);

            //Assert
            Assert.Equal(40, result);
        }

        [Fact]
        public void RunPart2Final()
        {
            //Arrange
            var input = GetFinalInput();

            //Act
            var result = new Day07().GetHowManyDifferentTimelinesPartTwo(input);

            //Assert
            Assert.Equal(25489586715621, result);
        }

        #region Private Methods

        private List<string> GetDemoInput()
        {
            var inputStr = ".......S.......\r\n...............\r\n.......^.......\r\n...............\r\n......^.^......\r\n...............\r\n.....^.^.^.....\r\n...............\r\n....^.^...^....\r\n...............\r\n...^.^...^.^...\r\n...............\r\n..^...^.....^..\r\n...............\r\n.^.^.^.^.^...^.\r\n...............";

            var rows = inputStr.Split("\r\n").ToList();

            return rows;
        }

        private List<string> GetFinalInput()
        {
            var inputStr = File.ReadAllText("Inputs\\input-d7.txt");

            var rows = inputStr.Split("\r\n").ToList();

            return rows;
        }

        #endregion Private Methods
    }
}