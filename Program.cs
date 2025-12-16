// part 2 between 6527 and 7462
var inputMovementsDemo = "L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82";

var inputMovements = File.ReadAllText("input-d1-p1.txt");

var movements = inputMovements.Split("\r\n").ToList();

var timesReachedZero = CalculateTimesReachedOrPassedZero(movements);

Console.WriteLine("timesReachedZero=" + timesReachedZero);

int CalculateTimesReachedOrPassedZero(List<string> movements)
{
    var startPosition = 50;

    var currentPosition = startPosition;

    var timesReachedZero = 0;

    foreach (var movement in movements)
    {
        var direction = movement[0];
        var nbrOfPositions = int.Parse(movement[1..]);
        var test = "";
        var nbrOfTurns = 0;

        if(direction == 'L')
        {
            nbrOfPositions = nbrOfPositions * -1;
        }

        if(Math.Abs(nbrOfPositions) > 99) 
        { 
            timesReachedZero += Math.Abs(nbrOfPositions / 100);

            nbrOfPositions = nbrOfPositions % 100;
        }

        if (nbrOfPositions < 0 && Math.Abs(nbrOfPositions) > currentPosition && currentPosition != 0)
        {
            timesReachedZero++;
        }

        else if(currentPosition + nbrOfPositions > 100)
        {
            timesReachedZero++;
        }

        else if((nbrOfPositions + currentPosition == 0) || (nbrOfPositions +currentPosition) == 100)
        {
            timesReachedZero++;
        }

        currentPosition = (currentPosition + nbrOfPositions) % 100;
        if(currentPosition < 0)
        {
            currentPosition = 100 - Math.Abs(currentPosition);
        }
    }

    return timesReachedZero;
}


(int endPosition, int timesPassedOnZero) handleNegativePosition(int position) 
    => ((position % 100) + 100, Math.Abs(position / 100) + 1);

(int endPosition, int timesPassedOnZero) handleBigPosition(int position) 
    => (position % 100, Math.Abs(position / 100));
