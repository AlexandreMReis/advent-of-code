
// part 2 between 6527 and 7462
var inputMovementsDemo = "L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82";

var inputMovements = File.ReadAllText("input-d1-p1.txt");

var movements = inputMovementsDemo.Split("\r\n").ToList();

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

        currentPosition += nbrOfPositions;


        if(currentPosition < 0)
        {
            var negativePositionResult = handleNegativePosition(currentPosition);
            currentPosition = negativePositionResult.endPosition;
            timesReachedZero += negativePositionResult.timesPassedOnZero;
        }

        if (currentPosition > 99)
        {
            var bigPositionResult = handleBigPosition(currentPosition);
            currentPosition = bigPositionResult.endPosition;
            timesReachedZero += bigPositionResult.timesPassedOnZero;
        }

        if (currentPosition == 0)
        {
            timesReachedZero++;
        }
    }

    return timesReachedZero;
}


(int endPosition, int timesPassedOnZero) handleNegativePosition(int position) 
    => ((position % 100) + 100, Math.Abs(position / 100) + 1);

(int endPosition, int timesPassedOnZero) handleBigPosition(int position) 
    => (position % 100, Math.Abs(position / 100));
