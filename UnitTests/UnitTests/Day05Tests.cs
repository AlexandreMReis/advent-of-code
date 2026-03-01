using AdvendOfCode.Days;

namespace UnitTests
{
    public class Day05Tests
    {
        [Fact]
        public void RunPart1Demo()
        {
            //Arrange
            var input = GetDemoInput();

            //Act
            var result = new Day05().GetHowManyFreshIdsPartOne(input.Item1, input.Item2);

            //Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void RunPart1Final()
        {
            //Arrange
            var input = GetFinalInput();

            //Act
            var result = new Day05().GetHowManyFreshIdsPartOne(input.Item1, input.Item2);

            //Assert
            Assert.Equal(505, result);
        }

        #region Private Methods

        private Tuple<List<string>, List<string>> GetDemoInput()
        {
            var inputStr = "3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32";

            var rangesAndIdsStr = inputStr.Split("\r\n\r\n");

            var ranges = rangesAndIdsStr[0].Split("\r\n").ToList();

            var ids = rangesAndIdsStr[1].Split("\r\n").ToList();

            return Tuple.Create(ranges, ids);
        }

        private Tuple<List<string>, List<string>> GetFinalInput()
        {
            var inputStr = File.ReadAllText("input-d5.txt");

            var rangesAndIdsStr = inputStr.Split("\r\n\r\n");

            var ranges = rangesAndIdsStr[0].Split("\r\n").ToList();

            var ids = rangesAndIdsStr[1].Split("\r\n").ToList();

            return Tuple.Create(ranges, ids);
        }

        #endregion Private Methods
    }
}