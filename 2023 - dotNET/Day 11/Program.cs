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
            int expansion = 1000000;

            Dictionary<int, int> rowCosts = new Dictionary<int, int>();
            Dictionary<int, int> colCosts = new Dictionary<int, int>();

            for (int colIndex = galaxyBoard[0].Count - 1; colIndex >= 0; colIndex--)
            {
                if (galaxyBoard.All(row => row[colIndex] == '.'))
                {
                    colCosts[colIndex] = colCosts.ContainsKey(colIndex) ? colCosts[colIndex] + 1 : 1;
                }
            }

            for (int rowIndex = galaxyBoard.Count - 1; rowIndex >= 0; rowIndex--)
            {
                if (galaxyBoard[rowIndex].All(col => col == '.'))
                {
                    rowCosts[rowIndex] = rowCosts.ContainsKey(rowIndex) ? rowCosts[rowIndex] + 1 : 1;
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
                        int rowDistance = Math.Abs(galaxy1.Key.Item1 - galaxy2.Key.Item1);
                        int colDistance = Math.Abs(galaxy1.Key.Item2 - galaxy2.Key.Item2);

                        rowDistance += rowCosts.Where(kvp => kvp.Key <= Math.Max(galaxy1.Key.Item1, galaxy2.Key.Item1) && kvp.Key > Math.Min(galaxy1.Key.Item1, galaxy2.Key.Item1)).Sum(kvp => kvp.Value) * (expansion - 1);
                        colDistance += colCosts.Where(kvp => kvp.Key <= Math.Max(galaxy1.Key.Item2, galaxy2.Key.Item2) && kvp.Key > Math.Min(galaxy1.Key.Item2, galaxy2.Key.Item2)).Sum(kvp => kvp.Value) * (expansion - 1);

                        // Calculate the total distance
                        long distance = rowDistance + colDistance;

                        galaxyDistances[Tuple.Create(galaxy1.Value, galaxy2.Value)] = distance;
                        distanceList.Add(distance);
                    }
                }
            }

            long totalDistance = distanceList.Sum();

            Console.WriteLine("Total Manhattan Distance : " + totalDistance);
        }
    }
}