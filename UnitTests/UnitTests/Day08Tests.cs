using AdvendOfCode.Days;

namespace UnitTests
{
    public class Day08Tests
    {
        [Fact]
        public void RunPart1Demo()
        {
            //Arrange
            var input = GetDemoInput();

            //Act
            var result = new Day08().ResolvePartOne(input, 10);

            //Assert
            Assert.Equal(40, result);
        }

        [Fact]
        public void RunPart1Final()
        {
            //Arrange
            var input = GetFinalInput();

            //Act
            var result = new Day08().ResolvePartOne(input, 1000);

            //Assert
            Assert.Equal(330786, result);
        }

        [Fact]
        public void RunPart2Demo()
        {
            //Arrange
            var input = GetDemoInput();

            //Act
            var result = new Day08().ResolvePartTwo(input);

            //Assert
            Assert.Equal(25272, result);
        }

        [Fact]
        public void RunPart2Final()
        {
            //Arrange
            var input = GetFinalInput();

            //Act
            var result = new Day08().ResolvePartTwo(input);

            //Assert
            Assert.Equal(3276581616, result);
        }

        #region Private Methods

        private List<string> GetDemoInput()
        {
            var inputStr = "162,817,812\r\n57,618,57\r\n906,360,560\r\n592,479,940\r\n352,342,300\r\n466,668,158\r\n542,29,236\r\n431,825,988\r\n739,650,466\r\n52,470,668\r\n216,146,977\r\n819,987,18\r\n117,168,530\r\n805,96,715\r\n346,949,466\r\n970,615,88\r\n941,993,340\r\n862,61,35\r\n984,92,344\r\n425,690,689";

            var rows = inputStr.Split("\r\n").ToList();

            return rows;
        }

        private List<string> GetFinalInput()
        {
            var inputStr = File.ReadAllText("Inputs\\input-d8.txt");

            var rows = inputStr.Split("\r\n").ToList();

            return rows;
        }

        #endregion Private Methods
    }
}