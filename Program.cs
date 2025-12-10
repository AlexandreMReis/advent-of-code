

var inputMovementsDemo = "L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82";

var inputMovements = File.ReadAllText("input-d1-p1.txt");
//Console.WriteLine(inputMovements);

var movements = inputMovements.Split("\r\n").ToList();

var timesReachedZero = CalculateTimesReachedZero1stAtempt(movements);

Console.WriteLine("timesReachedZero=" + timesReachedZero);

int CalculateTimesReachedZero1stAtempt(List<string> movements) {

    var startPosition = 50;

    var currentPosition = startPosition;

    var timesReachedZero = 0;

    foreach (var movement in movements)
    {
        Console.WriteLine("positionBeforeMovement=" + currentPosition);
        Console.WriteLine("movement=" + movement);
        var direction = movement[0];
        var nbrOfPositions = int.Parse(movement[1..]);
        var test = "";
        if(direction != 'L' && direction != 'R')
        {
            test = "test123";
        }

        if(nbrOfPositions < 0 || nbrOfPositions > 99)
        {
            test = "test123";
        }

        if (direction == 'L')
        {
            currentPosition = currentPosition - nbrOfPositions;
        
            if(currentPosition < 0)
            {
                currentPosition = currentPosition + 100;
            }

            if (currentPosition == 0)
            {
                timesReachedZero++;
            }
        }
        else
        {
            currentPosition = currentPosition + nbrOfPositions;
        
            if (currentPosition > 99)
            {
                currentPosition = currentPosition - 100;
            }

            if (currentPosition == 0)
            {
                timesReachedZero++;
            }
        }
        Console.WriteLine("timesReachedZero=" + timesReachedZero);
        Console.WriteLine("positionAfterMovement=" + currentPosition);
    }



    return timesReachedZero;
}
