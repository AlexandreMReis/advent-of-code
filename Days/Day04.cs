using System.Diagnostics;

namespace AdvendOfCode.Days;

public class Day04
{
    public long Run(bool runDemo = false)
    {
        var linesOfRollsStr = string.Empty;
        if (runDemo)
        {
            linesOfRollsStr = "..@@.@@@@.\r\n@@@.@.@.@@\r\n@@@@@.@.@@\r\n@.@@@@..@.\r\n@@.@@@@.@@\r\n.@@@@@@@.@\r\n.@.@.@.@@@\r\n@.@@@.@@@@\r\n.@@@@@@@@.\r\n@.@.@@@.@.";
        }
        else
        {
            linesOfRollsStr = File.ReadAllText("input-d4.txt");
        }

        var linesOfRolls = linesOfRollsStr.Split("\r\n").ToList();

        var matrixOfRolls = new List<char[]>();

        for (int i = 0; i < linesOfRolls.Count; i++)
        {
            var lineOfRoll = linesOfRolls[i];
            matrixOfRolls.Add(lineOfRoll.ToCharArray());
        }

        var sw = Stopwatch.StartNew();

        var output = GetHowManyRollsCanBeAccessedPartOne(matrixOfRolls);
        sw.Stop();
        Console.WriteLine("Elapsed time on part one: " + sw.ElapsedMilliseconds); //7 ms

        sw = Stopwatch.StartNew();

        output = GetHowManyRollsCanBeAccessedPartTwo(matrixOfRolls); //43 ms
        sw.Stop();
        Console.WriteLine("Elapsed time on part two: " + sw.ElapsedMilliseconds);

        return output;
    }

    public int GetHowManyRollsCanBeAccessedPartOne(List<char[]> matrixOfRolls)
    {
        var output = 0;

        for(int i = 0; i < matrixOfRolls.Count; i++)
        {
            var lineOfRolls = matrixOfRolls[i];
            for(int j = 0; j < lineOfRolls.Length; j++)
            {
                if(matrixOfRolls[i][j] == '@' && CanAccessRoll(matrixOfRolls, i, j))
                {
                    output++;
                }
            }
        }
 
        return output;
    }

    public int GetHowManyRollsCanBeAccessedPartTwo(List<char[]> matrixOfRolls)
    {
        var output = 0;
        var nextMatrixOfRolls = matrixOfRolls
                                    .Select(row => (char[]) row.Clone())
                                    .ToList();

        for (int i = 0; i < matrixOfRolls.Count; i++)
        {
            var lineOfRolls = matrixOfRolls[i];
            for (int j = 0; j < lineOfRolls.Length; j++)
            {
                if (matrixOfRolls[i][j] == '@' && CanAccessRoll(matrixOfRolls, i, j))
                {
                    nextMatrixOfRolls[i][j] = 'X';
                    output++;
                }
            }
        }

        if(output > 0)
        {
            return output + GetHowManyRollsCanBeAccessedPartTwo(nextMatrixOfRolls);
        }

        return output;
    }

    private bool CanAccessRoll(List<char[]> matrixOfRolls, int y, int x)
    {
        var adjacentRolls = 0;

        if (GetRoll(matrixOfRolls, y - 1, x - 1) == '@')
        {
            adjacentRolls++;
        }

        if (GetRoll(matrixOfRolls, y - 1, x) == '@')
        {
            adjacentRolls++;
        }

        if (GetRoll(matrixOfRolls, y - 1, x + 1) == '@')
        {
            adjacentRolls++;
        }

        if (GetRoll(matrixOfRolls, y, x - 1) == '@')
        {
            adjacentRolls++;
        }

        if (GetRoll(matrixOfRolls, y, x + 1) == '@')
        {
            adjacentRolls++;
        }

        if (GetRoll(matrixOfRolls, y + 1, x - 1) == '@')
        {
            adjacentRolls++;
        }

        if (GetRoll(matrixOfRolls, y + 1, x) == '@')
        {
            adjacentRolls++;
        }

        if (GetRoll(matrixOfRolls, y + 1, x + 1) == '@')
        {
            adjacentRolls++;
        }

        return adjacentRolls < 4;
    }

    private char GetRoll(List<char[]> matrixOfRolls, int y, int x)
    {
        var output = '.';

        try
        {
            output =  matrixOfRolls[y][x];
        }
        catch(Exception)
        {
            output = '.';
        }

        return output;
    }
}
