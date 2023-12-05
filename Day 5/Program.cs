// AUTHOR: RICARDO MARTENS
// DATE: 5/12/2023
// DAY 5 - ADVENT OF CODE

using Utilities;

namespace Day05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isMapSection = false;
            long minLocation = long.MaxValue;
            string[] lines = AoCUtilities.GetInput();
            List<long> initialSeedList = new List<long>();

            Dictionary<string, List<(long, long, long)>> mappings = new Dictionary<string, List<(long, long, long)>>();
            string mapName = "";


            foreach (string line in lines)
            {
                if (line.StartsWith("seeds:"))
                {
                    int seedIndex = line.IndexOf(":");
                    string[] seeds = line.Substring(seedIndex + 1).TrimStart().Split(" ");

                    foreach (string seed in seeds)
                    {
                        _ = long.TryParse(seed, out long s);
                        initialSeedList.Add(s);
                    }
                }

                else if (line.Contains("map:"))
                {
                    isMapSection = true;
                    mapName = line.Substring(0, line.IndexOf("map:")).TrimEnd();
                    mappings[mapName] = new List<(long, long, long)>();
                }

                else if (isMapSection && !string.IsNullOrWhiteSpace(line))
                {
                    int mapIndex = line.IndexOf(":");
                    string[] row = line[(mapIndex + 1)..].Split(" ");

                    if (row.Length >= 3)
                    {
                        bool parseDestination = long.TryParse(row[0], out long destinationRangeStart);
                        bool parseSource = long.TryParse(row[1], out long sourceRangeStart);
                        bool parseRange = long.TryParse(row[2], out long rangeLength);

                        if (!parseDestination || !parseSource || !parseRange)
                        {
                            Console.WriteLine("Error parsing values. Please check the input.");
                        }
                        else
                        {
                            mappings[mapName].Add((destinationRangeStart, sourceRangeStart, rangeLength));
                        }
                    }

                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    isMapSection = false;
                }
            }

            minLocation = CalculatePart1(minLocation, mappings, initialSeedList);
            Console.WriteLine($"The lowest location for part 1 is: {minLocation}");

            minLocation = CalculatePart2(minLocation, mappings, initialSeedList);
            Console.WriteLine($"The lowest location for part 2 is: {minLocation}");
        }

        private static long CalculatePart1(long minLocation, Dictionary<string, List<(long, long, long)>> mappings, List<long> initialSeedList)
        {
            List<long> locations = new List<long>();

            for (int i = 0; i < initialSeedList.Count; i++)
            {
                long soil = GetMappedValue("seed-to-soil", initialSeedList[i], mappings);
                long fertilizer = GetMappedValue("soil-to-fertilizer", soil, mappings);
                long water = GetMappedValue("fertilizer-to-water", fertilizer, mappings);
                long light = GetMappedValue("water-to-light", water, mappings);
                long temperature = GetMappedValue("light-to-temperature", light, mappings);
                long humidity = GetMappedValue("temperature-to-humidity", temperature, mappings);
                long location = GetMappedValue("humidity-to-location", humidity, mappings);
                locations.Add(location);
                minLocation = Math.Min(minLocation, location);
            }

            return minLocation;
        }

        private static long CalculatePart2(long minLocation, Dictionary<string, List<(long, long, long)>> mappings, List<long> initialSeedList)
        {

            minLocation = mappings.Values.SelectMany(list => list).Min(tuple => tuple.Item1);

            while (true)
            {
                long seedPossibility = minLocation;

                // Try and map in reverse. 
                // Due to us mapping in reverse, its easier on the compiler to look up the most minimal location and find the correct seed, instead of the other way around
                // This way we dont have to go over all the different seeds.
                foreach (string mapNaming in new[] { "humidity-to-location", "temperature-to-humidity", "light-to-temperature", "water-to-light", "fertilizer-to-water", "soil-to-fertilizer", "seed-to-soil" })
                {
                    seedPossibility = GetMappedValue(mapNaming, seedPossibility, mappings, reverse: true);
                }

                bool isValidSeed = false;
                for (int i = 0; i < initialSeedList.Count; i += 2)
                {
                    if (seedPossibility >= initialSeedList[i] && seedPossibility < initialSeedList[i] + initialSeedList[i + 1])
                    {
                        isValidSeed = true;
                        break;
                    }
                }

                if (isValidSeed)
                {
                    break;
                }

                minLocation++;
            }

            return minLocation;
        }


        private static long GetMappedValue(string mapName, long source, Dictionary<string, List<(long, long, long)>> mappings, bool reverse = false)
        {
            List<(long, long, long)> map = mappings[mapName];

            foreach (var (destinationRangeStart, sourceRangeStart, rangeLength) in map)
            {
                if (reverse)
                {
                    if (source >= destinationRangeStart && source < destinationRangeStart + rangeLength)
                    {
                        return source + (sourceRangeStart - destinationRangeStart);
                    }
                }
                else
                {
                    if (source >= sourceRangeStart && source < sourceRangeStart + rangeLength)
                    {
                        return source + (destinationRangeStart - sourceRangeStart);
                    }
                }
            }

            return source;
        }
    }
}
