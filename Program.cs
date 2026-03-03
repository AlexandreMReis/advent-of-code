using AdvendOfCode.Days;

Console.WriteLine("Welcome to Advent of Code");
int day = -1;
var runProgram = true;
long result = -1;

while (runProgram)
{
    Console.WriteLine("Which day challenge would you like to run? (Press 1 to 24) ");
    while (!int.TryParse(Console.ReadLine(), out day))
    {
        Console.WriteLine("Error reading your day. Please try again.");
    }

    Console.WriteLine("Would you like to run the demo input? (y/n) ");
    var isDemoRun = Console.ReadLine() == "y";

    switch (day)
    {
        case 1:
            result = new Day01().Run(isDemoRun);
            break;

        case 2:
            result = new Day02().Run(isDemoRun);
            break;
        case 3:
            result = new Day03().Run(isDemoRun);
            break;
        case 4:
            result = new Day04().Run(isDemoRun);
            break;
        case 5:
            result = new Day05().Run(isDemoRun);
            break;

        case 6:
            result = new Day06().Run(isDemoRun);
            break;
        default:

            break;
    }

    if(result == -1)
    {
        Console.WriteLine("Error: Invalid day");
    }
    else
    {
        Console.WriteLine($"Day {day} is demo {isDemoRun} Result = {result}");
    }

    Console.WriteLine($"Exit? (y/n): ");
    runProgram = Console.ReadLine() != "y";
}


