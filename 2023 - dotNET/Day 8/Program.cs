// AUTHOR: RICARDO MARTENS
// DATE: 8/12/2023
// DAY 8 - ADVENT OF CODE

using Utilities;

namespace Day08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = AoCUtilities.GetInput();
            SortedDictionary<string, (string, string)> mapInstructions = ParseMapInstructions(lines);
            int[] directions = ParseDirections(lines[0]);

            var part1Result = TraversePath(mapInstructions, directions, "AAA", "ZZZ").Count - 1;
            Console.WriteLine($"We got to ZZZ in {part1Result} steps");

            var startNodes = GetStartNodes(mapInstructions);
            var pathLengths = startNodes.Select(start => TraversePath(mapInstructions, directions, start, "Z").Count - 1).ToArray();

            var part2Result = FindLCM(pathLengths);
            Console.WriteLine(part2Result);
        }

        private static SortedDictionary<string, (string, string)> ParseMapInstructions(string[] lines)
        {
            var mapInstructions = new SortedDictionary<string, (string, string)>();
            foreach (string line in lines)
            {
                if (line.Contains("="))
                {
                    int index = line.IndexOf('=');
                    string start = line.Substring(0, index).TrimEnd();
                    string[] mappings = line.Substring(index + 1).Trim('(', ')', ' ').Split(',');

                    mapInstructions[start] = (mappings[0].Trim(), mappings[1].Trim());
                }
            }
            return mapInstructions;
        }

        private static int[] ParseDirections(string line)
        {
            return line.Trim().Select(index => index == 'R' ? 1 : 0).ToArray();
        }

        private static List<string> GetStartNodes(SortedDictionary<string, (string, string)> map)
        {
            return map.Keys.Where(key => key.EndsWith("A")).ToList();
        }

        private static List<string> TraversePath(SortedDictionary<string, (string, string)> sortedMapping, int[] directions, string start, string end)
        {
            var currentNode = start;
            var path = new List<string>() { start };
            int index = 0;

            do
            {
                var directionIndex = index % directions.Length;
                currentNode = directions[directionIndex] == 0 ? sortedMapping[currentNode].Item1 : sortedMapping[currentNode].Item2;
                path.Add(currentNode);
                index++;
            } 

            while (!currentNode.EndsWith(end));

            return path;
        }

        public static long LCM(long a, long b)
        {
            return a * b / GCD(a, b);
        }

        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long FindLCM(int[] numbers)
        {
            long result = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                result = LCM(result, numbers[i]);
            }
            return result;
        }
    }
}
