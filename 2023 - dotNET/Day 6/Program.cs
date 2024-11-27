// AUTHOR: RICARDO MARTENS
// DATE: 6/12/2023
// DAY 6 - ADVENT OF CODE

using System.Text.RegularExpressions;
using Utilities;

namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = AoCUtilities.GetInput();
            List<long> timings = new List<long>();
            List<long> distances = new List<long>();
            long timePartTwo = 0;
            long distancePartTwo = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("Time:"))
                {
                    ParseLine(line, "Time:", timings, out timePartTwo);
                }
                else if (line.StartsWith("Distance:"))
                {
                    ParseLine(line, "Distance:", distances, out distancePartTwo);
                }
            }

            Console.WriteLine($"Answer for part 1: {CalculatePart1(timings, distances)}");
            Console.WriteLine($"Answer for part 2: {CalculatePart2(timePartTwo, distancePartTwo)}");
        }

        private static void ParseLine(string line, string prefix, List<long> list, out long partTwoValue)
        {
            string values = Regex.Replace(line.Substring(prefix.Length).Trim(), " +", " ");
            string[] valuesInRace = Regex.Split(values, " ");

            foreach (string value in valuesInRace)
            {
                if (long.TryParse(value, out long result))
                {
                    list.Add(result);
                }
            }

            long.TryParse(values.Replace(" ", ""), out partTwoValue);
        }

        private static int CalculatePart1(List<long> timings, List<long> distances)
        {
            int beatenRecords = 0;
            int totalBeatenRecords = 1;

            for (int i = 0; i < timings.Count; i++)
            {
                for (int j = 0; j <= timings[i]; j++)
                {
                    if (j * (timings[i] - j) > distances[i])
                    {
                        beatenRecords++;
                    }
                }
                totalBeatenRecords *= beatenRecords;
                beatenRecords = 0;
            }
            return totalBeatenRecords;
        }

        private static long CalculatePart2(long timing, long distance)
        {
            double a = -1;
            double b = timing;
            double c = -distance;

            double discrim = b * b - 4 * a * c;

            if (discrim < 0)
            {
                Console.WriteLine("No root found");
                return -1;
            }

            double sqrtDiscrim = Math.Sqrt(discrim);
            double root1 = (-b + sqrtDiscrim) / (2 * a);
            double root2 = (-b - sqrtDiscrim) / (2 * a);

            double maxRoot = Math.Max(root1, root2);

            long roundedDownRoot = (long)Math.Floor(maxRoot);
            long roundedUpRoot = (long)Math.Ceiling(maxRoot);

            long distanceDown = roundedDownRoot * (timing - roundedDownRoot);
            long distanceUp = roundedUpRoot * (timing - roundedUpRoot);

            long finalTime = distanceDown > distanceUp ? roundedDownRoot : roundedUpRoot;

            long beatenRecords = 0;
            for (long j = 0; j <= finalTime; j++)
            {
                if (j * (timing - j) > distance)
                {
                    beatenRecords++;
                }
            }

            return beatenRecords;
        }
    }
}
