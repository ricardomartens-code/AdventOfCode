// AUTHOR: RICARDO MARTENS
// DATE: 4/12/2023
// DAY 4 - ADVENT OF CODE

using Utilities;

namespace Day04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = AoCUtilities.GetInput();
            Dictionary<int, int> cardCopyDictionary = InitializeCardCopyDictionary(lines.Length);
            int totalPoints = 0;

            foreach (string line in lines)
            {
                int gameNumber = GetGameNumber(line);
                string[] winningNumbers = GetWinningNumbers(line);
                string[] ownedNumbers = GetOwnedNumbers(line);

                int numOfInstances = cardCopyDictionary[gameNumber];
                int newInstances = CalculateNewInstances(winningNumbers, ownedNumbers);
                int points = 0;

                for (int i = 0; i < ownedNumbers.Length; i++)
                {
                    for (int j = 0; j < winningNumbers.Length; j++)
                    {
                        if (ownedNumbers[i] == winningNumbers[j])
                        {
                            int cardNumber = i + 1;

                            if (cardNumber <= lines.Length)
                            {
                                if (points == 0)
                                {
                                    points += 1;
                                }
                                else
                                {
                                    points *= 2;
                                }
                                Console.WriteLine($"Your amount of points are now: {points}");
                            }
                        }
                    }
                }

                totalPoints += points;

                UpdateCardCopyDictionary(cardCopyDictionary, gameNumber, newInstances, numOfInstances, lines.Length);

                Console.WriteLine($"Your amount of points are now: {points}");
            }
            Console.WriteLine($"Your total amount of points are: {totalPoints}");

            int totalInstances = CalculateTotalInstances(cardCopyDictionary);
            Console.WriteLine($"Total number of instances: {totalInstances}");
        }

        private static Dictionary<int, int> InitializeCardCopyDictionary(int length)
        {
            var dictionary = new Dictionary<int, int>();
            for (int k = 1; k <= length; k++)
            {
                dictionary[k] = 1;
            }
            return dictionary;
        }

        private static int GetGameNumber(string line)
        {
            int cardNumbersIndex = line.IndexOf(":");
            string game = line.Substring(0, cardNumbersIndex);
            return Convert.ToInt32(game.Substring(cardNumbersIndex - 3).TrimStart());
        }

        private static string[] GetWinningNumbers(string line)
        {
            int cardNumbersIndex = line.IndexOf(":");
            string cardNumbers = line.Substring(cardNumbersIndex + 1);
            int verticalBar = cardNumbers.IndexOf("|");
            return cardNumbers.Substring(0, verticalBar).Replace("  ", " ").TrimStart().Split(" ");
        }

        private static string[] GetOwnedNumbers(string line)
        {
            int cardNumbersIndex = line.IndexOf(":");
            string cardNumbers = line.Substring(cardNumbersIndex + 1);
            int verticalBar = cardNumbers.IndexOf("|");
            return cardNumbers.Substring(verticalBar + 1).Replace("  ", " ").TrimStart().Split(" ");
        }

        private static int CalculateNewInstances(string[] winningNumbers, string[] ownedNumbers)
        {
            return ownedNumbers.Count(ownedNumber => winningNumbers.Contains(ownedNumber));
        }

        private static void UpdateCardCopyDictionary(Dictionary<int, int> cardCopyDictionary, int gameNumber, int newInstances, int numOfInstances, int length)
        {
            for (int i = gameNumber + 1; i <= gameNumber + newInstances && i <= length; i++)
            {
                cardCopyDictionary[i] += numOfInstances;
            }
        }

        private static int CalculateTotalInstances(Dictionary<int, int> cardCopyDictionary)
        {
            return cardCopyDictionary.Values.Sum();
        }
    }
}
