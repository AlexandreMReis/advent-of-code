// part 2 between 6527 and 7462

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
            var dayOne = new Day01();
            result = dayOne.Run(isDemoRun);
            break;

        case 2:
            var dayTwo = new Day02();
            result = dayTwo.Run(isDemoRun);
            break;
        case 3:
            var dayThree = new Day03();
            result = dayThree.Run(isDemoRun);
            break;
        case 4:
            var dayFour = new Day04();
            result = dayFour.Run(isDemoRun);
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


