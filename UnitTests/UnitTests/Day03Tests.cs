using AdvendOfCode.Days;

namespace UnitTests
{
    public class Day03Tests
    {
        [Fact]
        public void RunPart1Demo()
        {
            //Arrange
            var banks = GetDemoInput();

            //Act
            var result = (new Day03()).GetBanksHighestJoltagesPartOne(banks);

            //Assert
            Assert.Equal(357, result);
        }

        [Fact]
        public void RunPart1Final()
        {
            //Arrange
            var banks = GetFinalInput();

            //Act
            var result = (new Day03()).GetBanksHighestJoltagesPartOne(banks);

            //Assert
            Assert.Equal(16812, result);
        }

        [Fact]
        public void RunPart2Demo()
        {
            //Arrange
            var banks = GetDemoInput();

            //Act
            var result = (new Day03()).GetBanksHighestJoltagesPartTwo(banks);

            //Assert
            Assert.Equal(3121910778619, result);
        }

        [Fact]
        public void RunPart2Final()
        {
            //Arrange
            var banks = GetFinalInput();

            //Act
            var result = (new Day03()).GetBanksHighestJoltagesPartTwo(banks);

            //Assert
            Assert.Equal(166345822896410, result);
        }

        #region Private Methods

        private List<string> GetDemoInput()
        {
            string input = "987654321111111\r\n811111111111119\r\n234234234234278\r\n818181911112111";
            return input.Split("\r\n").ToList();
        }

        private List<string> GetFinalInput()
        {
            string input = File.ReadAllText("input-d3.txt");
            return input.Split("\r\n").ToList();
        }

        #endregion Private Methods
    }
}