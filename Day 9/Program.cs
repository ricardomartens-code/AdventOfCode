// AUTHOR: RICARDO MARTENS
// DATE: 9/12/2023
// DAY 9 - ADVENT OF CODE

using Utilities;

namespace Day09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = AoCUtilities.GetInput();
            long resultPartOne = 0;
            long resultPartTwo = 0;
            foreach (string line in lines)
            {
                List<int> numbers = line.Split(' ').Select(int.Parse).ToList();
                List<List<int>> sequenceNumberList = new()
                {
                    numbers
                };

                resultPartOne += ExtrapolateTree(sequenceNumberList);
                Console.WriteLine($"The sum of all extrapolated values for part 1 is {resultPartOne}");
                resultPartTwo += ExtrapolateTree(sequenceNumberList, true);
                Console.WriteLine($"The sum of all extrapolated values for part 2 is {resultPartTwo}");

            }
        }

        public static long ExtrapolateTree(List<List<int>> sequenceList, bool partTwo = false)
        {
            long sum = 0;
            for (int i = 0; i < sequenceList.Count; i++)
            {
                if (sequenceList[i].All(number => number == 0))
                {
                    int index = i - 1;
                    if (!partTwo)
                    {
                        sequenceList[i].Add(0);

                        for (int j = sequenceList[index].Count - 1; index >= 0; j++)
                        {
                            var count = sequenceList[index].Count;
                            var calc = sequenceList[index + 1][j] + sequenceList[index][count - 1];
                            sequenceList[index].Add(calc);
                            index--;
                        }
                        sum += sequenceList[0][sequenceList[0].Count - 1];
                    }
                    else
                    {
                        sequenceList[i].Insert(0, 0);

                        for(int j = 0; index >= 0;)
                        {
                            var calc = sequenceList[index][j] - sequenceList[index + 1][j];
                            sequenceList[index].Insert(0, calc);
                            index--;
                        }
                        sum += sequenceList[0][0];
                    }
                    break;
                }

                List<int> followUpNumbers = new();

                for (int j = 0; j < sequenceList[i].Count; j++)
                {

                    if (j + 1 == sequenceList[i].Count)
                    {
                        break;
                    }
                    followUpNumbers.Add(sequenceList[i][j + 1] - sequenceList[i][j]);
                }
                sequenceList.Add(followUpNumbers);
            }

            return sum;
        }
    }
}
