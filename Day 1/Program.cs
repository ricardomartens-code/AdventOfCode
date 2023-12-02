// AUTHOR: RICARDO MARTENS
// DATE: 1/12/2023
// DAY 1 - ADVENT OF CODE

namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            int sum = 0;

            // We need to add the end of each letter back to the string, otherwise overlapping words will go wrong.
            Dictionary<string, string> numberTable = new Dictionary<string, string>{
                {"one","1e"},{"two","2o"},{"three","3e"},{"four","4r"},{"five","5e"},{"six","6x"},
                {"seven","7n"},{"eight","8t"},{"nine","9e"}};

            foreach (string line in lines)
            {
                string newLine = line;
                // We need to order the keys from left to right, because otherwise
                // we get issues by words being replaced that are too the rigth of the string first
                var keys = numberTable.Keys.OrderBy(key => line.IndexOf(key)).ToList();

                foreach (var key in keys)
                {
                    newLine = newLine.Replace(key, numberTable[key]);
                }

                char firstDigit = newLine.First(char.IsDigit);
                char lastDigit = newLine.Last(char.IsDigit);

                string combination = firstDigit.ToString() + lastDigit.ToString();
                sum += Convert.ToInt32(combination);
                Console.WriteLine($"sum is now : {sum}");
            }
        }
    }
}




