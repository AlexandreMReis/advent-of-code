using System.Diagnostics;

namespace AdvendOfCode.Days;

public class Day05
{
    public long Run(bool runDemo = false)
    {
        var inputStr = string.Empty;
        if (runDemo)
        {
            inputStr = "3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32";
        }
        else
        {
            inputStr = File.ReadAllText("input-d5.txt");
        }

        var rangesAndIdsStr = inputStr.Split("\r\n\r\n");

        var ranges = rangesAndIdsStr[0].Split("\r\n").ToList();

        var ids = rangesAndIdsStr[1].Split("\r\n").ToList();

        var sw = Stopwatch.StartNew();

        var output = GetHowManyFreshIdsPartOne(ranges, ids);
        sw.Stop();
        Console.WriteLine("Result part one: " + output);
        Console.WriteLine("Elapsed time on part one: " + sw.ElapsedTicks); //48ms

        sw = Stopwatch.StartNew();

        output = GetHowManyFreshIdsPartTwo(ranges);
        sw.Stop();
        Console.WriteLine("Result part two: " + output);
        Console.WriteLine("Elapsed time on part two: " + sw.ElapsedTicks); //1.6ms

        return output;
    }

    public long GetHowManyFreshIdsPartOne(List<string> rangesStr, List<string> idsStr)
    {
        var ranges = rangesStr.Select(el => Tuple.Create(long.Parse(el.Split('-')[0]), long.Parse(el.Split('-')[1]))).OrderBy(el => el.Item1);

        var ids = idsStr.Select(long.Parse).ToList();

        var nbrFreshIds = GetHowManyFreshIds(ids, ranges);

        return nbrFreshIds;
    }

    private int GetHowManyFreshIds(List<long> ids, IOrderedEnumerable<Tuple<long, long>> ranges)
    {
        var nbrFreshIds = 0;

        foreach (long id in ids)
        {
            foreach (var range in ranges)
            {
                if (id >= range.Item1 && id <= range.Item2)
                {
                    nbrFreshIds++; // a fresh id
                    break;
                }
                else if (id < range.Item1)
                {
                    break; // not a fresh id
                }
            }
        }

        return nbrFreshIds;
    }

    public long GetHowManyFreshIdsPartTwo(List<string> rangesStr)
    {
        List<Tuple<long,long>> finalFreshIdRanges = [];

        var ranges = rangesStr.Select(el => Tuple.Create(long.Parse(el.Split('-')[0]), long.Parse(el.Split('-')[1])))
                              .OrderBy(e => e.Item1)
                              .ToList();

        for (int i = 0; i < ranges.Count; i++)
        {
            var range = ranges[i];

            if (!finalFreshIdRanges.Any()) { finalFreshIdRanges.Add(range); }
            else
            {
                AddNewFreshIdRanges(finalFreshIdRanges, range);
                finalFreshIdRanges = finalFreshIdRanges.OrderBy(r => r.Item1).ToList();
            }
        }

        var nbrFreshIds = CountFreshIds(finalFreshIdRanges);

        return nbrFreshIds;
    }

    public void AddNewFreshIdRanges(List<Tuple<long, long>> finalFreshIdRanges, Tuple<long, long> newFreshIdrange)
    {
        for (var i = 0; i < finalFreshIdRanges.Count(); i++)
        {
            // boundary case at start of list
            if(i == 0 && newFreshIdrange.Item2 < finalFreshIdRanges[i].Item1)
            {
                finalFreshIdRanges.Add(newFreshIdrange);
                break;
            }

            // boundary case at end of list
            if (i+1 == finalFreshIdRanges.Count && newFreshIdrange.Item1 > finalFreshIdRanges[i].Item2)
            {
                finalFreshIdRanges.Add(newFreshIdrange);
                break;
            }

            // usual case with no matches
            if (newFreshIdrange.Item1 > finalFreshIdRanges[i].Item2 &&
                newFreshIdrange.Item2 < finalFreshIdRanges[i+1].Item1)
            {
                finalFreshIdRanges.Add(newFreshIdrange);
                break;
            }

            // finalFreshIdRanges: [(3,5), (10,14)]
            // newFreshIdRange: (7,18)
            // expectedFinalFreshIdRanges: [(3,5), (7,18)]
            if (newFreshIdrange.Item1 < finalFreshIdRanges[i].Item1 &&
                newFreshIdrange.Item2 > finalFreshIdRanges[i].Item2)
            {
                finalFreshIdRanges[i] = Tuple.Create(newFreshIdrange.Item1, newFreshIdrange.Item2);
                break;
            }

            // finalFreshIdRanges: [(3,5), (10,14)]
            // newFreshIdRange: (11,13)
            // expectedFinalFreshIdRanges: [(3,5), (10,14)]
            if (newFreshIdrange.Item1 > finalFreshIdRanges[i].Item1 &&
                newFreshIdrange.Item2 < finalFreshIdRanges[i].Item2)
            {
                break;
            }

            //// fresh ids at start range already defined
            ///case1: finalFreshIdRanges = [(3,5),(10,14)]
            /// newFreshIdRange = (12,18)
            ///finalFreshIdRanges = [(3,5),(10,18)]
            ///
            ///case2: finalFreshIdRanges = [(3,5),(10,10)]
            /// newFreshIdRange = (10,14)
            ///finalFreshIdRanges = [(3,5),(10,14)]
            if (newFreshIdrange.Item1 <= finalFreshIdRanges[i].Item2 &&
                newFreshIdrange.Item2 > finalFreshIdRanges[i].Item2)
            {
                finalFreshIdRanges[i] = Tuple.Create(finalFreshIdRanges[i].Item1, newFreshIdrange.Item2);
                break;
            }

            //// fresh ids at end range already defined
            ///case1: finalFreshIdRanges = [(3,5),(10,14)]
            /// newFreshIdRange = (7,12)
            ///finalFreshIdRanges = [(3,5),(7,14)]
            ///
            ///case2: finalFreshIdRanges = [(3,5),(10,10)]
            /// newFreshIdRange = (7,10)
            ///finalFreshIdRanges = [(3,5),(7,10)]
            if (newFreshIdrange.Item1 < finalFreshIdRanges[i].Item1 &&
                newFreshIdrange.Item2 >= finalFreshIdRanges[i].Item1)
            {
                finalFreshIdRanges[i] = Tuple.Create(newFreshIdrange.Item1, finalFreshIdRanges[i].Item2);
                break;
            }
        }
    }

    public long CountFreshIds(List<Tuple<long, long>> finalFreshIdRanges)
    {
        long output = 0;
        foreach(var range in finalFreshIdRanges)
        {
            output += (range.Item2 - range.Item1) + 1;
        }

        return output;
    }
}
