using System.Diagnostics;

namespace AdvendOfCode.Days;

public class Day05
{
    public int Run(bool runDemo = false)
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
        Console.WriteLine("Elapsed time on part one: " + sw.ElapsedMilliseconds);

        sw = Stopwatch.StartNew();

        output = GetHowManyFreshIdsPartTwo(ranges);
        sw.Stop();
        Console.WriteLine("Result part two: " + output);
        Console.WriteLine("Elapsed time on part two: " + sw.ElapsedMilliseconds);

        return output;
    }

    private List<List<long>> BreakIntoMultipleLists(List<long> ids, int listSize = 1000)
    {
        var output = new List<List<long>>();

        for(int i = 0; i < (ids.Count()/listSize); i++)
        {
            var from = i * listSize;
            var to = i * listSize + listSize;

            output.Add(ids[from..to]);
        }

        return output;
    }

    public int GetHowManyFreshIdsPartOne(List<string> rangesStr, List<string> idsStr)
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

    //public async Task<int> GetHowManyFreshIdsPartOneAsync(List<string> rangesStr, List<string> idsStr)
    //{
    //    var ranges = rangesStr.Select(el => Tuple.Create(long.Parse(el.Split('-')[0]), long.Parse(el.Split('-')[1]))).OrderBy(el => el.Item1);

    //    var ids = idsStr.Select(long.Parse).ToList();

    //    var splittedIds = BreakIntoMultipleLists(ids, 2);
        
    //    var nbrFreshIds = 0;

    //    var tasksToRun = new List<Task<int>>();

    //    foreach(var splittedId in splittedIds)
    //    {
    //        tasksToRun.Add(GetHowManyFreshIdsAsync(splittedId, ranges));
    //    }

    //    await Task.WhenAll(tasksToRun);

    //    foreach(var task in tasksToRun)
    //    {
    //        nbrFreshIds += task.Result;
    //    }
 
    //    return nbrFreshIds;
    //}

    //private async Task<int> GetHowManyFreshIdsAsync(List<long> ids, IOrderedEnumerable<Tuple<long, long>> ranges)
    //{
    //    return await new Task<int>(() =>
    //    {
    //        var nbrFreshIds = 0;

    //        foreach (long id in ids)
    //        {
    //            foreach (var range in ranges)
    //            {
    //                if (id > range.Item1 && id < range.Item2)
    //                {
    //                    nbrFreshIds++; // a fresh id
    //                    break;
    //                }
    //                else if (id < range.Item1)
    //                {
    //                    break; // not a fresh id
    //                }
    //            }
    //        }

    //        return nbrFreshIds;
    //    });
    //}

    public int GetHowManyFreshIdsPartTwo(List<string> rangesStr)
    {
        HashSet<long> freshIds = [];

        var ranges = rangesStr.Select(el => Tuple.Create(long.Parse(el.Split('-')[0]), long.Parse(el.Split('-')[1]))).OrderBy(el => el.Item1);

        foreach (var range in ranges)
        {
            for(long i = range.Item1; i <= range.Item2; i++)
            {
                if (!freshIds.Contains(i))
                {
                    freshIds.Add(i);
                }
            }
        }

        return freshIds.Count;
    }
}
