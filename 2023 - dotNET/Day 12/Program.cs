// AUTHOR: RICARDO MARTENS
// DATE: 12/12/2023
// DAY 12 - ADVENT OF CODE

using System.Reflection.Metadata.Ecma335;
using System.Text;
using Utilities;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = AoCUtilities.GetInput().ToList();
            int total = 0;

            foreach (string line in lines)
            {
                int index = line.IndexOf(" ");
                string conditionRecords = line.Substring(0, index);
                HashSet<string> generatedSequences = new();

                string repeat = string.Concat(Enumerable.Repeat(conditionRecords + '?', 5));
                conditionRecords = repeat.Remove(repeat.Length - 1);
                StringBuilder conditionRecordsBuilder = new StringBuilder(conditionRecords);
                Console.WriteLine(conditionRecords);
                List<int> groupings = line.Substring(index + 1).Split(",").Select(int.Parse).ToList();
                List<int> repeatList = Enumerable.Repeat(groupings, 5).SelectMany(x => x).ToList();

                Possibilities(conditionRecordsBuilder, repeatList, ref total);

                Console.WriteLine(total);
            }
        }

        private static int Possibilities(StringBuilder builder, List<int> groupings, ref int total)
        {
            int count = 0;
            Queue<StringBuilder> queue = new Queue<StringBuilder>();
            queue.Enqueue(new StringBuilder(builder.ToString()));

            while (queue.Count > 0)
            {
                StringBuilder current = queue.Dequeue();
                int questionMarkIndex = current.ToString().IndexOf('?');

                if (questionMarkIndex == -1)
                {
                    //Console.WriteLine($"No question mark found anymore, the result string is {current}");
                    List<int> newGrouping = new List<int>();
                    int counter = 0;
                    current.Insert(current.Length, '.');
                    for (int i = 0; i < current.Length; i++)
                    {
                        if (current[i] == '#')
                        {
                            counter++;
                        }
                        else if (current[i] == '.')
                        {
                            newGrouping.Add(counter);
                            counter = 0;
                        }
                    }
                    var filteredGroup = newGrouping.Where(i => i != 0).ToList();
                    if (filteredGroup.SequenceEqual(groupings))
                    {
                        count++;
                        //Console.WriteLine($"Both groupings are equal, meaning {current} is a valid combination");
                    }
                }
                else
                {
                    StringBuilder replacedWithHash = new StringBuilder(current.ToString());
                    replacedWithHash[questionMarkIndex] = '#';
                    queue.Enqueue(replacedWithHash);

                    StringBuilder replacedWithDot = new StringBuilder(current.ToString());
                    replacedWithDot[questionMarkIndex] = '.';
                    queue.Enqueue(replacedWithDot);
                }
            }
            total += count;
            return count;
        }
    }
}