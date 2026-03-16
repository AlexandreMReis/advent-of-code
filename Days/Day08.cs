using System.Diagnostics;

namespace AdvendOfCode.Days;

// part one answer 3600 too low
// part one answer 191352 too low

public class Day08
{
    public double Run(bool runDemo = false)
    {
        var inputStr = string.Empty;
        var nbrOfConnections = 0;
        if (runDemo)
        {
            inputStr = "162,817,812\r\n57,618,57\r\n906,360,560\r\n592,479,940\r\n352,342,300\r\n466,668,158\r\n542,29,236\r\n431,825,988\r\n739,650,466\r\n52,470,668\r\n216,146,977\r\n819,987,18\r\n117,168,530\r\n805,96,715\r\n346,949,466\r\n970,615,88\r\n941,993,340\r\n862,61,35\r\n984,92,344\r\n425,690,689";
            nbrOfConnections = 10;
        }
        else
        {
            inputStr = File.ReadAllText("Inputs\\input-d8.txt");
            nbrOfConnections = 1000;
        }

        var rows = inputStr.Split("\r\n").ToList();

        //var sw = Stopwatch.StartNew();

        ////var output = ResolvePartOne(rows, nbrOfConnections);
        //sw.Stop();
        //Console.WriteLine("Result part one: " + output);
        //Console.WriteLine("Elapsed time on part one: " + sw.ElapsedMilliseconds);

        var sw = Stopwatch.StartNew();

        var output = ResolvePartTwo(rows);

        sw.Stop();
        Console.WriteLine("Result part two: " + output);
        Console.WriteLine("Elapsed time on part two: " + sw.ElapsedMilliseconds);

        return output;
    }

    //Kruskal algorithm - Minimum spanning tree truncated
    public double ResolvePartOne(List<string> input, int nbrOfConnections)
    {
        var points = ParsePoints(input);

        var pathsDistances = BuildPathDistances(points);

        var currentCircuits = new List<Circuit>() { };
        foreach(var point in points)
        {
            currentCircuits.Add(new Circuit() { Points = new List<Point3D>() { point } });
        }

        var connectionsTaken = 0;
        while (connectionsTaken < nbrOfConnections)
        {
            var edge = pathsDistances.Dequeue();
            AddEdgeToCircuits(edge, currentCircuits);
            connectionsTaken++;
        }

        int output = 1;
        foreach(var circuit in currentCircuits.OrderByDescending(c => c.Points.Count).Take(3))
        {
            output = output * circuit.Points.Count;
        }

        return output;
    }

    public List<Point3D> ParsePoints(List<string> input)
    {
        var points = new List<Point3D>();
        int pi = 0;
        foreach (var point in input)
        {
            Console.WriteLine("On point iterator: " + pi);
            pi++;
            var coordinates = point.Split(',');
            if (coordinates.Length != 3)
            {
                throw new ArgumentException("Invalid 3D point");
            }

            points.Add(new Point3D()
            {
                X = int.Parse(coordinates[0]),
                Y = int.Parse(coordinates[1]),
                Z = int.Parse(coordinates[2]),
            });
        }

        return points;
    }

    public PriorityQueue<(Point3D, Point3D), double> BuildPathDistances(List<Point3D> points)
    {
        var pathsDistances = new PriorityQueue<(Point3D, Point3D), double>();
        for (int i = 0; i < points.Count; i++)
        {
            var startPoint = points[i];
            var endPoints = points.Where(p => p.GetKey() != startPoint.GetKey()).ToList();
            for (var j = 0; j < endPoints.Count; j++)
            {
                if (i > j) continue;
                var endPoint = endPoints[j];
                Console.WriteLine($"({i}, {j})");
                var distance = startPoint.GetDistanceTo(endPoint);
                pathsDistances.Enqueue((startPoint, endPoint), distance);
            }
        }

        return pathsDistances;
    }

    public double ResolvePartTwo(List<string> input)
    {
        var points = ParsePoints(input);

        var pathsDistances = BuildPathDistances(points);

        var currentCircuits = new List<Circuit>() { };
        foreach (var point in points)
        {
            currentCircuits.Add(new Circuit() { Points = new List<Point3D>() { point } });
        }

        var connectionsTaken = 0;
        (Point3D, Point3D) edge = new();
        while (true)
        {
            edge = pathsDistances.Dequeue();
            AddEdgeToCircuits(edge, currentCircuits);
            connectionsTaken++;
            if(currentCircuits.Count == 1)
            {
                break;
            }
        }

        return edge.Item1.X * edge.Item2.X;
    }

    public void AddEdgeToCircuits((Point3D, Point3D) edge, List<Circuit> circuits)
    {
        var isRedundant = circuits.Any(c => c.Points.Any(p => p.GetKey() == edge.Item1.GetKey()) && c.Points.Any(p => p.GetKey() == edge.Item2.GetKey()));
        if (isRedundant) return;

        var circuitA = circuits.FirstOrDefault(c => c.Points.Any(p => p.GetKey() == edge.Item1.GetKey()));
        var circuitB = circuits.FirstOrDefault(c => c.Points.Any(p => p.GetKey() == edge.Item2.GetKey()));
        if(circuitA == null || circuitB == null)
        {
            throw new Exception("Circuit not found");
        }

        circuitA.Points.AddRange(circuitB.Points);
        var index = circuits.FindLastIndex(c => c.GetKey() == circuitB.GetKey());
        circuits.RemoveAt(index);
    }
}

public class Point3D {
    public double X {get; set;}
    public double Y {get; set;}
    public double Z {get; set;}

    public Point3D() { }

    public double GetDistanceTo(Point3D point)
    {
		var xSquare = Math.Pow(this.X - point.X, 2);
		var ySquare = Math.Pow(this.Y - point.Y, 2);
        var zSquare = Math.Pow(this.Z - point.Z, 2);

        return Math.Sqrt(xSquare + ySquare + zSquare);
	}

    public string GetKey() => $"{X}_{Y}_{Z}";
}

public class Circuit
{
    public List<Point3D> Points { get; set; } = new();

    public string GetKey() => string.Join('|', Points.Select(p => p.GetKey()));
}