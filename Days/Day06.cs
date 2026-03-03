using System.Diagnostics;
using System.Drawing;

namespace AdvendOfCode.Days;

public class Day06
{
    public long Run(bool runDemo = false)
    {
        var inputStr = string.Empty;
        if (runDemo)
        {
            inputStr = "123 328  51 64 \r\n 45 64  387 23 \r\n  6 98  215 314\r\n*   +   *   + ";
        }
        else
        {
            inputStr = File.ReadAllText("input-d6.txt");
        }

        var rows = inputStr.Split("\r\n").ToList();

        var matrix = new List<List<string>>();
        foreach (var row in rows)
        {
            var columnsByRow = row.Split(' ').Where(el => !string.IsNullOrWhiteSpace(el)).ToList();
            matrix.Add(columnsByRow);
        }

        var sw = Stopwatch.StartNew();

        var output = GetHomeWorkSolutionPartOne(matrix);
        sw.Stop();
        Console.WriteLine("Result part one: " + output);
        Console.WriteLine("Elapsed time on part one: " + sw.ElapsedTicks); //48ms

        sw = Stopwatch.StartNew();

        output = GetHomeWorkSolutionPartTwo(rows);

        sw.Stop();
        Console.WriteLine("Result part two: " + output);
        Console.WriteLine("Elapsed time on part two: " + sw.ElapsedTicks); //1.6ms

        return output;
    }

    // Demo input
    //        123  328  51  64 
    //         45  64  387  23 
    //          6  98  215  314
    //        *    +   *    +

    // le da direita para a esquerda e de cima para baixo.
    // Reads from right to left and from top to bottom.
    // Iteration 1: The last element of the first row is empty, so put 0 in currentNumber.
    // Iteration 2: Moving to the next row and keeping the column, it's a space ' ' and there is still only 0 in currentNumber.
    // Add another 0.
    // Iteration 3: Still in the same column and in the next row, get the 4 and concatenate to 00, so currentNumber = 004.
    // Iteration 4: No more rows, close the number with 004.
    // Move to the next column and return to iteration 1.
    // Iteration 1: The second-to-last element of the first row is 4, put 4 in currentNumber.
    // Iteration 2: Moving to the next row and keeping the column, it's 3, so currentNumber = 43.
    // Iteration 3: Still in the same column and in the next row, get 1 and concatenate to 43, so currentNumber = 431.
    // Iteration 4: No more rows, close the number with 431.
    // Move to the next column and return to iteration 1.
    // Iteration 1: The third-to-last element of the first row is 6, put 6 in currentNumber.
    // Iteration 2: Moving to the next row and keeping the column, it's 2, so currentNumber = 62.
    // Iteration 3: Still in the same column and in the next row, get 3 and concatenate to 62, so currentNumber = 623.
    // Iteration 4: No more rows, close the number with 623.
    // Move to the next column and return to iteration 1.
    // Iteration 1: Before the third-to-last element of the first row is empty ' ', put 0 in currentNumber.
    // Iteration 2: Moving to the next row and keeping the column, it's empty ' ', so currentNumber = 00.
    // Iteration 3: Still in the same column and in the next row, get empty ' ' and concatenate to 00, so currentNumber = 000.
    // Iteration 4: No more rows and currentNumber = three zeros, meaning 3 empty spaces, which means we've reached the end of the problem.
    // We have the problem, now we go to the last operand in the operands list and sum the three values or multiply them.
    // At the end, we remove this last problem and this last operand and run this algorithm again until we've cleared all the problems from the input.
    // iteracao 1 ultimo elemento da primeira linha é vazio coloca 0 no currentNumber
    // iteracao 2 passando para a proxima linha e mantendo a coluna vem que é espaco vazio ' ' e ainda só existe 0 no currentNumber
    // acrescenta mais um 0 
    // iteracao 3 ainda na mesma coluna e na proxima linha apanha o 4 e concatena ao 00 ficando currentNumber = 004
    // iteracao 4 nao temos mais linhas fechamos o numero com 004.
    // passamos á proxima coluna e voltamos á iteracao 1
    // iteracao 1 penultimo elemento da primeira linha é 4 coloca 4 no currentNumber
    // iteracao 2 passando para a proxima linha e mantendo a coluna vem que é 3 entao currentNumber = 43
    // iteracao 3 ainda na mesma coluna e na proxima linha apanha 1 e concatena ao 43 ficando currentNumber = 431
    // iteracao 4 nao temos mais linhas fechamos o numero com 431.
    // passamos á proxima coluna e voltamos á iteracao 1
    // iteracao 1 antepenultimo elemento da primeira linha é 6 coloca 6 no currentNumber
    // iteracao 2 passando para a proxima linha e mantendo a coluna vem que é 2 entao currentNumber = 62
    // iteracao 3 ainda na mesma coluna e na proxima linha apanha 3 e concatena ao 62 ficando currentNumber = 623
    // iteracao 4 nao temos mais linhas fechamos o numero com 623.
    // passamos á proxima coluna e voltamos á iteracao 1
    // iteracao 1 antes do antepenultimo elemento da primeira linha é vazio ' ' coloca 0 no currentNumber
    // iteracao 2 passando para a proxima linha e mantendo a coluna vem que vazio ' ' entao currentNumber = 00
    // iteracao 3 ainda na mesma coluna e na proxima linha apanha vazio ' ' e concatena ao 00 ficando currentNumber = 000
    // iteracao 4 nao temos mais linhas e currentNumber = trez zeros ou seja 3 espacos vazios querdizer que chegamos ao fim do problema
    //temos o problema, agora vamos ao ultimo operand da lists dos operands e sumamos os tres valores ou multiplamos.
    // no fim removemos este ultimo problema e este ultimo operando e voltamos a correr este algoritmo até termos limpo todo os problemas do input.

    private long SolveFirstProblemFromRight(List<string> input, List<string> operands)
    {
        List<long> problems = new List<long>();
        
        int i = input[0].Length - 1;
        while (i >= 0 )
        {
            string currentNumber = string.Empty;
            foreach (var row in input)
            {
                var currChar = row[i]; //last element

                if(currChar == ' ')
                {
                    if(currentNumber.Length == 0 || currentNumber.All(c => c == '0'))
                    {
                        currentNumber = currentNumber + '0';
                    }
                }
                else
                {
                    currentNumber = currentNumber + currChar;
                }
            }

            if(currentNumber.All(e => e == '0'))
            {
                break;
            }

            problems.Add(long.Parse(currentNumber));

            i--;
        }

        RemoveLastColumns(problems, input);

        if (operands.Last() == "*")
        {
            long mul = 1;
            foreach (var el in problems)
            {
                mul *= el;
            }

            operands.RemoveAt(operands.Count - 1);

            return mul;
        }
        else if(operands.Last() == "+")
        {
            operands.RemoveAt(operands.Count - 1);

            return problems.Sum();
        }
        else
        {
            throw new Exception("Invalid operand");
        }
    }

    private void RemoveLastColumns(List<long> problems, List<string> input)
    {
        var nbrDigits = problems.Count + 1;

        for (int i = 0; i < input.Count; i++)
        {
            if (input[i].Length > nbrDigits)
            {
                input[i] = new string(input[i].Take(input[i].Length - nbrDigits).ToArray());
            }
            else
            {
                input[i] = string.Empty;
            }
        }
    }

    private List<List<string>> InstantiateMatrix(int outerSize, int innerSize)
    {
        var output = new List<List<string>>();

        for(var i = 0;i < outerSize; i++)
        {
            output.Add(Enumerable.Repeat("-", innerSize).ToList());
        }

        return output;
    }

    public List<List<string>> ReverseMatrix(List<List<string>> matrix)
    {
        if (matrix.Count == 0)
        {
            return new List<List<string>>();
        }

        var reversedMatrix = InstantiateMatrix(matrix[0].Count(), matrix.Count());

        for (var i = 0; i < matrix.Count; i++)
        {
            for (var j = 0; j < matrix[i].Count; j++)
            {
                var element = matrix[i][j];
                reversedMatrix[j][i] = element;
            }
        }

        return reversedMatrix;
    }

    public long GetHomeWorkSolutionPartOne(List<List<string>> matrix)
    {
        //      123 328  51  64
        //       45  64 387  23
        //        6  98 215 314
        //        *   +   *   +

        var reversedMatrix = ReverseMatrix(matrix);

        long result = 0;
        foreach(var row in reversedMatrix)
        {
            var longRow = row.Where(e => e != "*" && e != "+").Select(e => long.Parse(e));
            var signal = row.LastOrDefault();
            if (signal == "+")
            {
                var sum = longRow.Sum();
                result += sum;
            }

            if(signal == "*")
            {
                long mul = 1;
                foreach(var el in longRow)
                {
                    mul *= el;
                }
                result += mul;
            }
        }

        return result;
    }

    public long GetHomeWorkSolutionPartTwo(List<string> rows)
    {
        // Demo input
        //        123  328  51  64
        //         45  64  387  23
        //          6  98  215  314
        //        *    +   *    +

        //  The rightmost problem is 4 + 431 + 623 = 1058
        //  The second problem from the right is 175 * 581 * 32 = 3253600
        //  The third problem from the right is 8 + 248 + 369 = 625
        //  Finally, the leftmost problem is 356 * 24 * 1 = 8544
        //  Now, the grand total is 1058 + 3253600 + 625 + 8544 = 3263827.

        // Final input example
        //        886  63  27  258
        //        652  49  14   97
        //        65  143  36    8
        //        8   686 695    9
        //        *   +   +    *

        long result = 0;

        var operands = rows[rows.Count - 1].Split(' ').Where(el => !string.IsNullOrWhiteSpace(el)).ToList();

        //remove operands from input
        rows.RemoveAt(rows.Count - 1);

        while (rows[0].Length > 0)
        {
            result += SolveFirstProblemFromRight(rows, operands);
        }

        return result;
    }

    public List<long> ExtractRightToLeftInColumnNumbersPlus(List<string> inputList)
    {
        var count = inputList.Max(el => el.Length);

        var output = new List<long>();

        for (int i = count-1; i >= 0; i--)
        {
            string num = "";
            foreach (var val in inputList)
            {
                if (val.Length > i)
                {
                    var el = val[i];
                    num += el;
                }
            }

            output.Add(long.Parse(num));
        }

        return output;
    }

    public List<long> ExtractRightToLeftInColumnNumbersMult(List<string> inputList)
    {
        var count = inputList.Max(el => el.Length); // 3

        var output = new List<long>();

        for (int i = 0; i < count; i++)
        {
            string num = "";
            for (int j = 0; j < inputList.Count; j++)
            {
                var val = inputList[j];
                if (val.Length == (count - i))
                {
                    num += val.LastOrDefault();
                    inputList[j] = new string(val.Take(val.Length - 1).ToArray());
                }
            }

            output.Add(long.Parse(num));
        }

        return output;
    }
}
