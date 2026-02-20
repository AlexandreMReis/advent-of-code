using AdvendOfCode.Days;

namespace UnitTests
{
    public class Day04Tests
    {
        [Fact]
        public void RunPart1Demo()
        {
            //Arrange
            var rolls = GetDemoInput();

            //Act
            var result = new Day04().GetHowManyRollsCanBeAccessedPartOne(rolls);

            //Assert
            Assert.Equal(13, result);
        }

        [Fact]
        public void RunPart1Final()
        {
            //Arrange
            var rolls = GetFinalInput();

            //Act
            var result = new Day04().GetHowManyRollsCanBeAccessedPartOne(rolls);

            //Assert
            Assert.Equal(1480, result);
        }

        [Fact]
        public void RunPart2Demo()
        {
            //Arrange
            var rolls = GetDemoInput();

            //Act
            var result = new Day04().GetHowManyRollsCanBeAccessedPartTwo(rolls);

            //Assert
            Assert.Equal(43, result);
        }

        [Fact]
        public void RunPart2Final()
        {
            //Arrange
            var rolls = GetFinalInput();

            //Act
            var result = new Day04().GetHowManyRollsCanBeAccessedPartTwo(rolls);

            //Assert
            Assert.Equal(8899, result);
        }

        #region Private Methods

        private List<char[]> GetDemoInput()
        {
            string input = "..@@.@@@@.\r\n@@@.@.@.@@\r\n@@@@@.@.@@\r\n@.@@@@..@.\r\n@@.@@@@.@@\r\n.@@@@@@@.@\r\n.@.@.@.@@@\r\n@.@@@.@@@@\r\n.@@@@@@@@.\r\n@.@.@@@.@.";

            var linesOfRolls = input.Split("\r\n").ToList();

            var matrixOfRolls = new List<char[]>();

            for (int i = 0; i < linesOfRolls.Count; i++)
            {
                var lineOfRoll = linesOfRolls[i];
                matrixOfRolls.Add(lineOfRoll.ToCharArray());
            }


            return matrixOfRolls;
        }

        private List<char[]> GetFinalInput()
        {
            string input = File.ReadAllText("input-d4.txt");
            
            var linesOfRolls = input.Split("\r\n").ToList();

            var matrixOfRolls = new List<char[]>();

            for (int i = 0; i < linesOfRolls.Count; i++)
            {
                var lineOfRoll = linesOfRolls[i];
                matrixOfRolls.Add(lineOfRoll.ToCharArray());
            }


            return matrixOfRolls;
        }

        #endregion Private Methods
    }
}