using AdvendOfCode.Days;

namespace UnitTests
{
    public class Day02Tests
    {
        [Fact]
        public void RunPart1Demo()
        {
            //Arrange           
            var idsRanges = GetDemoInput();

            //Act
            var result = (new Day02()).GetInvalidIdsPartOne(idsRanges).Sum();

            //Assert
            Assert.Equal(1227775554, result);
        }

        [Fact]
        public void RunPart1Final()
        {
            //Arrange
            var idsRanges = GetFinalInput();

            //Act
            var result = (new Day02()).GetInvalidIdsPartOne(idsRanges).Sum();

            //Assert
            Assert.Equal(18893502033, result);
        }

        [Fact]
        public void RunPart2Demo()
        {
            //Arrange      
            var idsRanges = GetDemoInput();

            //Act
            var result = (new Day02()).GetInvalidIdsPartTwo(idsRanges).Sum();

            //Assert
            Assert.Equal(4174379265, result);
        }

        [Fact]
        public void RunPart2Final()
        {
            //Arrange
            var idsRanges = GetFinalInput();

            //Act
            var result = (new Day02()).GetInvalidIdsPartTwo(idsRanges).Sum();

            //Assert
            Assert.Equal(26202168557, result);
        }

        #region Private Methods

        private List<string> GetDemoInput()
        {
            string input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

            return input.Split(",").ToList();
        }

        private List<string> GetFinalInput()
        {
            string input = File.ReadAllText("input-d2.txt");
            return input.Split(",").ToList();
        }

        #endregion Private Methods
    }
}