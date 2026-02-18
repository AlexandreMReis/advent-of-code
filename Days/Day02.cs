namespace AdvendOfCode.Days;

public class Day02
{
    public long Run(bool runDemo = false)
    {
        var idsRangesStr = string.Empty;
        if (runDemo)
        {
            idsRangesStr = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
        }
        else
        {
            idsRangesStr = File.ReadAllText("input-d2.txt");
        }

        var idsRanges = idsRangesStr.Split(",").ToList();

        var invalidIds = GetInvalidIdsPartTwo(idsRanges);

        return invalidIds.Sum();
    }

    public List<long> GetInvalidIdsPartOne(List<string> idsRanges)
    {
        var invalidIds = new List<long>();

        foreach(var idsRange in idsRanges)
        {
            var minMax = idsRange.Split('-').Select(v => long.Parse(v)).ToList();
            var max = minMax[1];
            for(var i = minMax.First(); i <= max; i++)
            {
                var nbrStr = i.ToString();

                var midLength = nbrStr.Length / 2;

                var firstHalf = nbrStr[..midLength];
                var secondHalf = nbrStr[midLength..];

                if (firstHalf == secondHalf) 
                {
                    invalidIds.Add(i);
                }
            }
        }

        return invalidIds;
    }

    public List<long> GetInvalidIdsPartTwo(List<string> idsRanges)
    {
        var invalidIds = new List<long>();

        foreach (var idsRange in idsRanges)
        {
            var minMax = idsRange.Split('-').Select(v => long.Parse(v)).ToList();
            var min = minMax[0];
            var max = minMax[1];
            for (var currentId = min; currentId <= max; currentId++)
            {
                var nbrStr = currentId.ToString();
                if(nbrStr.Length < 2)
                {
                    continue;
                }

                var halfIdLength = (nbrStr.Length + 1) / 2;

                for (var k = 1; k <= halfIdLength; k++)
                {
                    var partToCompare = nbrStr[..k];

                    var allParts = Enumerable.Range(0, nbrStr.Length / partToCompare.Length)
                        .Select(x => nbrStr.Substring(x * partToCompare.Length, partToCompare.Length))
                        .ToArray();

                    var haveAllElements = (allParts.Length * partToCompare.Length) == nbrStr.Length;

                    if (haveAllElements && allParts.All(v => v == partToCompare))
                    {
                        invalidIds.Add(currentId);
                        break;
                    }
                }
            }
        }

        invalidIds = invalidIds.Order().ToList();

        return invalidIds;
    }
}
