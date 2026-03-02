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

        [Fact]
        public void RunPart2Demo()
        {
            //Arrange
            var input = GetDemoInput();

            //Act
            var result = new Day05().GetHowManyFreshIdsPartTwo(input.Item1);

            //Assert
            Assert.Equal(14, result);
        }

        [Fact]
        public void RunPart2Final()
        {
            //Arrange
            var input = GetFinalInput();

            //Act
            var result = new Day05().GetHowManyFreshIdsPartTwo(input.Item1);

            //Assert
            Assert.Equal(344423158480189, result);
        }


        [Theory]
        [InlineData(
            new long[] { 3, 5, 10, 14 }, // finalFreshIdRanges: [(3,5), (10,14)]
            new long[] { 12, 18 },       // newFreshIdRange: (12,18)
            new long[] { 3, 5, 10, 18 }  // expectedFinalFreshIdRanges: [(3,5), (10,18)]
        )]
        [InlineData(
            new long[] { 3, 5, 10, 14 }, // finalFreshIdRanges: [(3,5), (10,14)]
            new long[] { 7, 12 },       // newFreshIdRange: (7,12)
            new long[] { 3, 5, 7, 14 }  // expectedFinalFreshIdRanges: [(3,5), (7,14)]
        )]
        [InlineData(
            new long[] { 3, 5, 10, 14 }, // finalFreshIdRanges: [(3,5), (10,14)]
            new long[] { 7, 18 },       // newFreshIdRange: (7,18)
            new long[] { 3, 5, 7, 18 }  // expectedFinalFreshIdRanges: [(3,5), (7,18)]
        )]
        [InlineData(
            new long[] { 3, 5, 10, 14 }, // finalFreshIdRanges: [(3,5), (10,14)]
            new long[] { 11, 13 },       // newFreshIdRange: (11,13)
            new long[] { 3, 5, 10, 14 }  // expectedFinalFreshIdRanges: [(3,5), (10,14)]
        )]
        [InlineData(
            new long[] { 3, 5, 10, 14 }, // finalFreshIdRanges: [(3,5), (10,14)]
            new long[] { 10, 10 },       // newFreshIdRange: (10,10)
            new long[] { 3, 5, 10, 14 }  // expectedFinalFreshIdRanges: [(3,5), (10,14)]
        )]
        [InlineData(
            new long[] { 3, 5, 10, 10 }, // finalFreshIdRanges: [(3,5), (10,10)]
            new long[] { 10, 14 },       // newFreshIdRange: (10,14)
            new long[] { 3, 5, 10, 14 }  // expectedFinalFreshIdRanges: [(3,5), (10,14)]
        )]
        [InlineData(
            new long[] { 3, 5, 10, 10 }, // finalFreshIdRanges: [(3,5), (10,10)]
            new long[] { 7, 10 },       // newFreshIdRange: (7,10)
            new long[] { 3, 5, 7, 10 }  // expectedFinalFreshIdRanges: [(3,5), (7,10)]
        )]
        public void RunAddNewFreshIdRangesCustom(long[] finalFreshIdRangesArr, long[] newFreshIdRangeArr, long[] expectedFinalFreshIdRangesArr)
        {
            //Arrange
            var finalFreshIdRanges = new List<Tuple<long, long>>();
            for (int i = 0; i < finalFreshIdRangesArr.Length; i += 2)
                finalFreshIdRanges.Add(Tuple.Create(finalFreshIdRangesArr[i], finalFreshIdRangesArr[i + 1]));

            var newFreshIdRange = Tuple.Create(newFreshIdRangeArr[0], newFreshIdRangeArr[1]);

            var expectedFinalFreshIdRanges = new List<Tuple<long, long>>();
            for (int i = 0; i < expectedFinalFreshIdRangesArr.Length; i += 2)
                expectedFinalFreshIdRanges.Add(Tuple.Create(expectedFinalFreshIdRangesArr[i], expectedFinalFreshIdRangesArr[i + 1]));

            //Act
            new Day05().AddNewFreshIdRanges(finalFreshIdRanges, newFreshIdRange);

            //Assert
            Assert.Equal(finalFreshIdRanges, expectedFinalFreshIdRanges);
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