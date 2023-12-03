// AUTHOR: RICARDO MARTENS
// DATE: 2/12/2023
// DAY 2 - ADVENT OF CODE
using Utilities;

namespace Day02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = AoCUtilities.GetInput();

            int redCubeLimit = 12;
            int greenCubeLimit = 13;
            int blueCubeLimit = 14;

            int highestBlue = 0;
            int highestRed = 0;
            int highestGreen = 0;

            int fullSum = 5050;
            int fullPowerSum = 0;

            foreach (string line in lines)
            {
                int gameIdIndex = line.IndexOf(": ");
                string gameIdString = line.Substring(0, gameIdIndex);
                string setString = line.Substring(gameIdIndex + 1);

                string[] setArray = setString.Split(';');
                List<string> gamesToBlock = new List<string>();

                foreach (string set in setArray)
                {
                    string[] cubeInSet = set.Split(",");
                    foreach (string cube in cubeInSet)
                    {
                        string cubeAmountIndexWithoutSpace = cube.TrimStart();
                        int cubeAmountIndex = cubeAmountIndexWithoutSpace.IndexOf(" ");
                        int cubesAmount = Convert.ToInt32(cubeAmountIndexWithoutSpace.Substring(0, cubeAmountIndex));
                        string cubesColor = cubeAmountIndexWithoutSpace.Substring(cubeAmountIndex + 1);

                        if (cubesColor == "red" && cubesAmount >= highestRed)
                        {
                            highestRed = cubesAmount;
                        }
                        else if (cubesColor == "green" && cubesAmount >= highestGreen)
                        {
                            highestGreen = cubesAmount;
                        }
                        else if (cubesColor == "blue" && cubesAmount >= highestBlue)
                        {
                            highestBlue = cubesAmount;
                        }

                        if (cubesColor == "red" && cubesAmount > redCubeLimit || cubesColor == "green" && cubesAmount > greenCubeLimit || cubesColor == "blue" && cubesAmount > blueCubeLimit)
                        {
                            Console.WriteLine($"The game that currently is impossible due to amount red, green or blue cubes = {gameIdString}");
                            if (!gamesToBlock.Contains(gameIdString))
                            {
                                gamesToBlock.Add(gameIdString);
                            }
                        }
                    }
                }

                foreach (string game in gamesToBlock)
                {
                    int gameId = Convert.ToInt32(game.Substring(5));
                    fullSum -= gameId;
                }

                Console.WriteLine($"Highest red, green and blue in the game is for red: {highestRed}, green {highestGreen}, blue {highestBlue} in {gameIdString}");

                int power = highestRed * highestGreen * highestBlue;
                fullPowerSum += power;

                highestRed = 0;
                highestGreen = 0;
                highestBlue = 0;
                Console.WriteLine(fullPowerSum);
            }
        }
    }
}