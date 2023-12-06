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
                    string times = Regex.Replace(line.Substring(5 + line.IndexOf("Time:")).TrimStart(), " +", " ");
                    string[] timesInRace = Regex.Split(times, " ");

                    foreach (string time in timesInRace)
                    {
                        timings.Add(Convert.ToInt32(time));
                    }

                    long.TryParse(line.Substring(5 + line.IndexOf("Time:")).Replace(" ", "").TrimStart(), out long timeResult);
                    timePartTwo = timeResult;
                }
                
                if (line.StartsWith("Distance:"))
                {
                    string raceDistance = Regex.Replace(line.Substring(9 + line.IndexOf("Distance:")).TrimStart(), " +", " ");
                    string[] distancesInRace = Regex.Split(raceDistance, " ");

                    foreach (string distance in distancesInRace)
                    {
                        distances.Add(Convert.ToInt32(distance));
                    }

                    long.TryParse(line.Substring(9 + line.IndexOf("Distance:")).Replace(" ", "").TrimStart(), out long distanceResult);
                    distancePartTwo = distanceResult;
                }
            }
            Console.WriteLine($"Answer for part 1: {CalculatePart1(timings, distances)}");
            Console.WriteLine($"Answer for part 2: {CalculatePart2(timePartTwo, distancePartTwo)}");
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
            // QUADRATIC FORMULA WOOOOOOOO
            double a = -1;
            double b = timing;
            double c = -distance;

            double discrim = b * b - 4 * a * c;

            if(discrim < 0) {
                return -1;
            }

            double sqrtDiscrim = Math.Sqrt(discrim);
            double rootA = (-b + sqrtDiscrim) / (2 * a);
            double rootB = (-b - sqrtDiscrim) / (2 * a);

            double maxRoot = Math.Max(rootA, rootB);

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