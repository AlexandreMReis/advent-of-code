using System.Diagnostics;

namespace AdvendOfCode.Days;

public class Day07
{
    public long Run(bool runDemo = false)
    {
        var inputStr = string.Empty;
        if (runDemo)
        {
            inputStr = ".......S.......\r\n...............\r\n.......^.......\r\n...............\r\n......^.^......\r\n...............\r\n.....^.^.^.....\r\n...............\r\n....^.^...^....\r\n...............\r\n...^.^...^.^...\r\n...............\r\n..^...^.....^..\r\n...............\r\n.^.^.^.^.^...^.\r\n...............";
        }
        else
        {
            inputStr = File.ReadAllText("Inputs\\input-d7.txt");
        }

        var rows = inputStr.Split("\r\n").ToList();

        var sw = Stopwatch.StartNew();

        var output = GetTimesBeamWasSplittedPartOne(rows);
        sw.Stop();
        Console.WriteLine("Result part one: " + output);
        Console.WriteLine("Elapsed time on part one: " + sw.ElapsedMilliseconds); //48ms

        sw = Stopwatch.StartNew();

        output = GetHowManyDifferentTimelinesPartTwo(rows);

        sw.Stop();
        Console.WriteLine("Result part two: " + output);
        Console.WriteLine("Elapsed time on part two: " + sw.ElapsedMilliseconds); //1.6ms

        return output;
    }
    
    public long GetTimesBeamWasSplittedPartOne(List<string> input)
    {
        var counter = 0;

        var rows = input.Select(row => row.ToArray()).ToArray();

        for (var i = 1; i < rows.Count(); i++)
        {
            var currentRow = rows[i];
            var rowBefore = rows[i - 1];

            for (var j = 0; j < currentRow.Length; j++)
            {
                if (currentRow[j] == '.' && (rowBefore[j] == 'S' || rowBefore[j] == '|'))
                {
                    currentRow[j] = '|';
                }
                else if(rowBefore[j] == '|' && currentRow[j] == '^')
                {
                    counter++;
                    if (j > 1 && currentRow[j - 1] == '.') currentRow[j-1] = '|';

                    if ( j < currentRow.Length && currentRow[j + 1] == '.') currentRow[j+1] = '|';
                }
            }
        }

        return counter;
    }

    public long GetHowManyDifferentTimelinesPartTwo(List<string> input)
    {
        return ExplorePathMemorizedNbrPaths(input);
    }

    private string[][] ConvertToArrayOfStrings(List<string> input)
    {
        var temp = new List<List<string>>();
        foreach (var el in input)
        {
            var row = new List<string>();
            foreach (var ch in el)
            {
                row.Add(ch.ToString());
            }

            temp.Add(row);
        }

        return temp.Select(row => row.ToArray()).ToArray();
    }

    public long ExplorePathMemorizedNbrPaths(List<string> input)
    {
        var inputArray = ConvertToArrayOfStrings(input);

        for (int i = 0; i < inputArray.Length; i++)
        {
            var row = inputArray[i];
            for(int j = 0; j < row.Length; j++)
            {
                var el = inputArray[i][j];
                if (i == 0 && el != "S") {
                    continue;
                }
                else if (el == "S")
                {
                    inputArray[i + 1][j] = "1";
                    break;
                }

                var elOnTop = inputArray[i - 1][j];
                if (elOnTop == ".") { 
                    continue; 
                }

                if (el == "^")
                {
                    if(!long.TryParse(elOnTop, out long numberOnTop))
                    {
                        throw new Exception($"Should be able to parse numberOnTop: {elOnTop} on i = {i} and j = {j}");
                    }

                    // build nbr of paths left to split ^
                    PropagateNbrOfPaths(inputArray, i, j - 1, numberOnTop);

                    // build nbr of paths right to split ^
                    PropagateNbrOfPaths(inputArray, i, j + 1, numberOnTop);
                }
                else if(el == "." && long.TryParse(elOnTop, out long numberOnTop))
                {
                    inputArray[i][j] = elOnTop;
                }

                el = inputArray[i][j];
            }
        }

        long totalNbrOfPaths = 0;
        foreach(var el in inputArray[inputArray.Length - 1])
        {
            if (long.TryParse(el, out long pathsCounter))
            {
                totalNbrOfPaths += pathsCounter;
            }
        }

        return totalNbrOfPaths;
    }

    private void PropagateNbrOfPaths(string[][] inputArray, int i, int j, long nbrPathsToPropagate)
    {
        if(i < 0 || i >= inputArray.Length || j < 0 || j >= inputArray[i].Length)
        {
            return;
        }
        var targetEl = inputArray[i][j];
        var topOfTargetEl = inputArray[i - 1][j];
        if (targetEl == "^")
        {
            return;
        }
        else if (targetEl == "." && topOfTargetEl == ".")
        {
            inputArray[i][j] = nbrPathsToPropagate.ToString();
        }
        else if (long.TryParse(targetEl, out long nbrPathsOnTargetEl))
        {
            inputArray[i][j] = (nbrPathsOnTargetEl + nbrPathsToPropagate).ToString();
        }
        else if (long.TryParse(topOfTargetEl, out long nbrPathsOnTopOfTargetEl))
        {
            inputArray[i][j] = (nbrPathsOnTopOfTargetEl + nbrPathsToPropagate).ToString();
        }
        else
        {
            throw new Exception($"Should be able to parse targetEl: {targetEl} on position ({i}, {j})");
        }
    }

    public long ExplorePathDFS(List<string> input, Position currentBranch) // uses O(2^n) because each new row can double the double the number of branches
    {
        var branchesToExplore = new List<Position>();
        var isFirstBranch = true;
        long pathsCounter = 0;

        while(isFirstBranch || branchesToExplore.Any())
        {
            if (!isFirstBranch)
            {
                currentBranch = branchesToExplore.Last();
                branchesToExplore.RemoveAt(branchesToExplore.Count - 1);
            }

            for (currentBranch.Row = currentBranch.Row; currentBranch.Row < input.Count; currentBranch.Row++)
            {
                
                if (input[currentBranch.Row][currentBranch.Column] == '^')
                {
                    isFirstBranch = false;
                    var leftBranch = new Position(currentBranch.Row, currentBranch.Column - 1);
                    var rightBranch = new Position(currentBranch.Row, currentBranch.Column + 1);

                    if (leftBranch.IsValid(input) && rightBranch.IsValid(input))
                    {
                        currentBranch.Column = leftBranch.Column;
                        branchesToExplore.Add(rightBranch);
                    }
                    else if (leftBranch.IsValid(input) && !rightBranch.IsValid(input))
                    {
                        currentBranch.Column = leftBranch.Column;
                    }
                    else if (!leftBranch.IsValid(input) && rightBranch.IsValid(input))
                    {
                        currentBranch.Column = rightBranch.Column;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid position to explore row: '{currentBranch.Row}' and column:'{currentBranch.Column}'");
                    }
                }
            }
            if(pathsCounter % 10000000 == 0)
            {
                Console.WriteLine("\n");
                Console.WriteLine("branches to explore:" + branchesToExplore.Count);
                Console.WriteLine("current column: " + currentBranch.Column);
            }
            
            pathsCounter++;
        }

        return pathsCounter;
    }

    public int ExplorePathRecursive(List<string> input, Position currentPosition)
    {
        for (int i = currentPosition.Row; i < input.Count; i++)
        {
            if (input[i][currentPosition.Column] == '^')
            {
                var isValidMinColumn = currentPosition.Column - 1 >= 0;
                var isValidMaxColumn = currentPosition.Column + 1 < input[i].Length;

                int counter = 0;
                if (isValidMinColumn && isValidMaxColumn)
                {
                    counter = ExplorePathRecursive(input, new Position(i, currentPosition.Column - 1))
                        + ExplorePathRecursive(input, new Position(i, currentPosition.Column + 1));
                }
                else if (isValidMinColumn && !isValidMaxColumn)
                {
                    counter = ExplorePathRecursive(input, new Position(i, currentPosition.Column - 1));
                }
                else if (!isValidMinColumn && isValidMaxColumn)
                {
                    counter = ExplorePathRecursive(input, new Position(i, currentPosition.Column + 1));
                }
                else
                {
                    throw new ArgumentException($"Invalid position to explore row: '{currentPosition.Row}' and column:'{currentPosition.Column}'");
                }

                Console.WriteLine("Current Counter is: " + counter);

                return counter;
            }
        }

        return 1;
    }

    // se resposta der um valor muito alto deve ser por causa que nao estamos a guardar os caminhos fechados e por isso estamos a replicar caminhos.
//    public long GetHowManyDifferentTimelinesPartTwo(List<string> input)
//    {
//        var counter = 0;

//        var rows = input.Select(row => row.ToArray()).ToArray();

//        var openPaths = new List<(int i, int j)>();

//        var exit = 0;

//        for (var i = 1; i < rows.Count(); i++)
//        {
//            var currentRow = rows[i];
//            var rowBefore = rows[i - 1];

//            for (var j = 0; j < currentRow.Length; j++)
//            {
//                if (currentRow[j] == '.' && (rowBefore[j] == 'S' || rowBefore[j] == '|'))
//                {
//                    currentRow[j] = '|';
//                }
//                else if (rowBefore[j] == '|' && currentRow[j] == '^')
//                {
//                    if (j < currentRow.Length && currentRow[j + 1] == '.')
//                    {

//                        openPaths.Add(new(i, j + 1));
//                    }

//                    if (j > 1 && currentRow[j - 1] == '.')
//                    {
//                        openPaths.Add(new(i, j - 1));
//                    }

//                    exit = 1;
//                    break;
//                }
//            }

//            if (exit == 1) break;
//        }

//        while (openPaths.Count > 0)
//        {
//            var currentPath = openPaths.Last();
//            rows[currentPath.i][currentPath.j] = '|';
//            openPaths.RemoveAt(openPaths.Count - 1);

//            var j = currentPath.j;
//            rows[currentPath.i][currentPath.j] = '|';

//            if (currentPath.i+1 >= rows.Count())
//            {
//                // reach end of timeline
//                //continue to next openPath
//                counter++;
//                continue;
//            }

//            for (var i = currentPath.i+1; i < rows.Count(); i++)
//            {
//                var currentRow = rows[i];
//                var rowBefore = rows[i - 1];

//                if (currentRow[j] == '.' && rowBefore[j] == '|')
//                {
//                    currentRow[j] = '|';
//                }
//                else if (rowBefore[j] == '|' && currentRow[j] == '^')
//                {
//                    if (j > 0 && currentRow[j - 1] == '.')
//                    {
//                        currentRow[j-1] = '|';
//                        if (j < currentRow.Length && currentRow[j + 1] == '.')
//                        {
//                            openPaths.Add(new(i, j + 1));
//                        }
//                    }
//                    else
//                    {
//                        if (j < currentRow.Length && currentRow[j + 1] == '.')
//                        {
//                            currentRow[j + 1] = '|';
//                        }
//                    }
//                }
//            }

//            // reach end of path, increase pathCounter
//            counter++;
//        }

//        return counter;
//    }
}

public class Position
{
    public int Row { get; set; }

    public int Column { get; set; }
    
    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public bool IsValid(List<string> input) => 
        Row >= 0 && Column >= 0 &&
        Row < input.Count && Column < input[0].Length;
}
