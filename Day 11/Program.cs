// AUTHOR: RICARDO MARTENS
// DATE: 11/12/2023
// DAY 11 - ADVENT OF CODE

using Utilities;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = AoCUtilities.GetInput().ToList();

            List<List<char>> galaxyBoard = lines.Select(s => s.ToList()).ToList();

            int galaxyCount = galaxyBoard.Count;

            for (int colIndex = galaxyBoard[0].Count - 1; colIndex >= 0; colIndex--)
            {
                if (galaxyBoard.All(row => row[colIndex] == '.'))
                {
                    foreach (var row in galaxyBoard)
                    {
                        Console.WriteLine(colIndex);
                        row.Insert(colIndex, '.');
                    }
                }
            }

            List<char> dottedRow = Enumerable.Repeat('.', galaxyBoard[0].Count).ToList();

            for (int rowIndex = galaxyBoard.Count - 1; rowIndex >= 0; rowIndex--)
            {
                if (galaxyBoard[rowIndex].All(col => col == '.'))
                {
                    galaxyBoard.Insert(rowIndex, dottedRow);
                    rowIndex--;
                }
            }

            Dictionary<Tuple<int, int>, int> galaxyNumbers = new Dictionary<Tuple<int, int>, int>();

            List<long> distanceList = new List<long>();

            int counter = 1;

            for (int i = 0; i < galaxyBoard.Count; i++)
            {
                for (int j = 0; j < galaxyBoard[i].Count; j++)
                {
                    if (galaxyBoard[i][j] == '#')
                    {
                        galaxyNumbers[Tuple.Create(i, j)] = counter++;
                    }
                }
            }

            Dictionary<Tuple<int, int>, long> galaxyDistances = new Dictionary<Tuple<int, int>, long>();

            foreach (var galaxy1 in galaxyNumbers)
            {
                foreach (var galaxy2 in galaxyNumbers)
                {
                    if (galaxy1.Value < galaxy2.Value)
                    {
                        long distance = Math.Abs(galaxy1.Key.Item1 - galaxy2.Key.Item1) + Math.Abs(galaxy1.Key.Item2 - galaxy2.Key.Item2);
                        galaxyDistances[Tuple.Create(galaxy1.Value, galaxy2.Value)] = distance;
                        distanceList.Add(distance);
                    }
                }
            }

            long totalDistance = distanceList.Sum();

            Console.WriteLine("Total Manhattan Distance for 1 rows/columns: " + totalDistance);
        }
    }
}