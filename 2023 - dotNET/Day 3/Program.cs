// AUTHOR: RICARDO MARTENS
// DATE: 3/12/2023
// DAY 3 - ADVENT OF CODE

using System.Text.RegularExpressions;
using Utilities;

namespace Day03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = AoCUtilities.GetInput();
            List<int> engineNumbers = new List<int>();

            HashSet<string> addedNumbersPositions = new HashSet<string>();
            var regx = new Regex("[^a-zA-Z0-9_.]");

            int totalGearProduct = 0;
            int rows = lines.Length;
            int cols = lines.Max(line => line.Length);

            char[,] charArray = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    charArray[i, j] = lines[i][j];
                }
            }

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < lines[i].Length; j++)
                {
                    // Check Above = -row
                    // Check under = +row
                    // Check left = -col
                    // Check right = +col
                    AddAdjacentNumbers(engineNumbers, addedNumbersPositions, regx, charArray, i, j, rows, cols);
                    AddDiagonalNumbers(engineNumbers, addedNumbersPositions, regx, charArray, i, j, rows, cols);

                    var gearNumbers = CheckForGears(charArray, i, j, rows, cols);
                    if (gearNumbers.Item1 != 0 && gearNumbers.Item2 != 0)
                    {
                        int product = gearNumbers.Item1 * gearNumbers.Item2;
                        totalGearProduct += product;
                        Console.WriteLine($"Product of adjacent numbers: {product}");
                    }
                }
            }

            Console.WriteLine(engineNumbers.Sum());
            Console.WriteLine($"Total product of all gears: {totalGearProduct}");
        }

        private static (string number, int start, int end) CheckDigits(char[,] charArray, int i, int j, int cols)
        {
            string digit = charArray[i, j].ToString();
            int start = j;
            int end = j;
            int k = j - 1;

            // Check left for more numbers
            while (k >= 0 && char.IsDigit(charArray[i, k]))
            {
                digit = charArray[i, k] + digit;
                start = k;
                k--;
            }

            k = j + 1;

            // Check right for more numbers
            while (k < cols && char.IsDigit(charArray[i, k]))
            {
                digit += charArray[i, k];
                end = k;
                k++;
            }

            Console.WriteLine($"The number adjacent to the symbol is: {digit}");
            return (digit, start, end);
        }

        private static void AddAdjacentNumbers(List<int> engineNumbers, HashSet<string> addedNumbersPositions, Regex regx, char[,] charArray, int i, int j, int rows, int cols)
        {
            // Up
            if (i - 1 > 0)
            {
                if (charArray[i, j] == '*')
                {
                    if (regx.IsMatch(charArray[i - 1, j].ToString()))
                    {
                        if (char.IsDigit(charArray[i, j]))
                        {
                            var result = CheckDigits(charArray, i, j, cols);
                            string positions = $"{i},{result.start}-{result.end}";

                            if (!addedNumbersPositions.Contains(positions))
                            {
                                engineNumbers.Add(Convert.ToInt32(result.number));
                                addedNumbersPositions.Add(positions);
                            }
                        }
                    }
                }
            }

            // Down
            if (i + 1 < rows)
            {
                if (regx.IsMatch(charArray[i + 1, j].ToString()))
                {
                    if (char.IsDigit(charArray[i, j]))
                    {
                        var result = CheckDigits(charArray, i, j, cols);
                        string positions = $"{i},{result.start}-{result.end}";

                        if (!addedNumbersPositions.Contains(positions))
                        {
                            engineNumbers.Add(Convert.ToInt32(result.number));
                            addedNumbersPositions.Add(positions);
                        }
                    }
                }
            }

            // Left
            if (j - 1 > 0)
            {
                if (regx.IsMatch(charArray[i, j - 1].ToString()))
                {
                    if (char.IsDigit(charArray[i, j]))
                    {
                        var result = CheckDigits(charArray, i, j, cols);
                        string positions = $"{i},{result.start}-{result.end}";

                        if (!addedNumbersPositions.Contains(positions))
                        {
                            engineNumbers.Add(Convert.ToInt32(result.number));
                            addedNumbersPositions.Add(positions);
                        }
                    }
                }
            }
            
            // Right
            if (j + 1 < cols)
            {
                if (regx.IsMatch(charArray[i, j + 1].ToString()))
                {
                    if (char.IsDigit(charArray[i, j]))
                    {
                        var result = CheckDigits(charArray, i, j, cols);
                        string positions = $"{i},{result.start}-{result.end}";

                        if (!addedNumbersPositions.Contains(positions))
                        {
                            engineNumbers.Add(Convert.ToInt32(result.number));
                            addedNumbersPositions.Add(positions);
                        }
                    }
                }
            }
        }

        private static void AddDiagonalNumbers(List<int> engineNumbers, HashSet<string> addedNumbersPositions, Regex regx, char[,] charArray, int i, int j, int rows, int cols)
        {
            // Up and to the left
            if (i - 1 > 0 && j - 1 > 0)
            {
                if (regx.IsMatch(charArray[i - 1, j - 1].ToString()))
                {
                    if (char.IsDigit(charArray[i, j]))
                    {
                        var result = CheckDigits(charArray, i, j, cols);
                        string positions = $"{i},{result.start}-{result.end}";

                        if (!addedNumbersPositions.Contains(positions))
                        {
                            engineNumbers.Add(Convert.ToInt32(result.number));
                            addedNumbersPositions.Add(positions);
                        }
                    }
                }
            }

            // Up and to the right
            if (i - 1 > 0 && j + 1 < cols)
            {
                if (regx.IsMatch(charArray[i - 1, j + 1].ToString()))
                {
                    if (char.IsDigit(charArray[i, j]))
                    {
                        var result = CheckDigits(charArray, i, j, cols);
                        string positions = $"{i},{result.start}-{result.end}";

                        if (!addedNumbersPositions.Contains(positions))
                        {
                            engineNumbers.Add(Convert.ToInt32(result.number));
                            addedNumbersPositions.Add(positions);
                        }
                    }
                }
            }

            // Down and to the left
            if (i + 1 < rows && j - 1 > 0)
            {
                if (regx.IsMatch(charArray[i + 1, j - 1].ToString()))
                {
                    if (char.IsDigit(charArray[i, j]))
                    {
                        var result = CheckDigits(charArray, i, j, cols);
                        string positions = $"{i},{result.start}-{result.end}";

                        if (!addedNumbersPositions.Contains(positions))
                        {
                            engineNumbers.Add(Convert.ToInt32(result.number));
                            addedNumbersPositions.Add(positions);
                        }
                    }
                }
            }

            // Down and to the right
            if (i + 1 < rows && j + 1 < cols)
            {
                if (regx.IsMatch(charArray[i + 1, j + 1].ToString()))
                {
                    if (char.IsDigit(charArray[i, j]))
                    {
                        var result = CheckDigits(charArray, i, j, cols);
                        string positions = $"{i},{result.start}-{result.end}";

                        if (!addedNumbersPositions.Contains(positions))
                        {
                            engineNumbers.Add(Convert.ToInt32(result.number));
                            addedNumbersPositions.Add(positions);
                        }
                    }
                }
            }
        }

        private static (int, int) CheckForGears(char[,] charArray, int i, int j, int rows, int cols)
        {
            // Check if current character is a gear
            if (charArray[i, j] == '*')
            {
                int[] adjacentNumbers = new int[2];
                int count = 0;
                HashSet<string> addedNumbersPositions = new HashSet<string>();

                // Up
                if (i - 1 > 0 && char.IsDigit(charArray[i - 1, j]))
                {
                    var result = CheckDigits(charArray, i - 1, j, cols);
                    string positions = $"{i - 1},{result.start}-{result.end}";

                    if (!addedNumbersPositions.Contains(positions))
                    {
                        adjacentNumbers[count++] = Convert.ToInt32(result.number);
                        addedNumbersPositions.Add(positions);
                    }
                }
                    

                // Down
                if (count < 2 && i + 1 < rows && char.IsDigit(charArray[i + 1, j]))
                {
                    var result = CheckDigits(charArray, i + 1, j, cols);
                    string positions = $"{i + 1},{result.start}-{result.end}";

                    if (!addedNumbersPositions.Contains(positions))
                    {
                        adjacentNumbers[count++] = Convert.ToInt32(result.number);
                        addedNumbersPositions.Add(positions);
                    }
                }
                    

                // Left
                if (count < 2 && j - 1 > 0 && char.IsDigit(charArray[i, j - 1]))
                {
                    var result = CheckDigits(charArray, i, j - 1, cols);
                    string positions = $"{i},{result.start}-{result.end}";

                    if (!addedNumbersPositions.Contains(positions))
                    {
                        adjacentNumbers[count++] = Convert.ToInt32(result.number);
                        addedNumbersPositions.Add(positions);
                    }
                }
                    

                // Right
                if (count < 2 && j + 1 < cols && char.IsDigit(charArray[i, j + 1]))
                {
                    var result = CheckDigits(charArray, i, j + 1, cols);
                    string positions = $"{i},{result.start}-{result.end}";

                    if (!addedNumbersPositions.Contains(positions))
                    {
                        adjacentNumbers[count++] = Convert.ToInt32(result.number);
                        addedNumbersPositions.Add(positions);
                    }
                }
                    

                // Check Up left
                if (count < 2 && i - 1 > 0 && j - 1 >= 0 && char.IsDigit(charArray[i - 1, j - 1]))
                {
                    var result = CheckDigits(charArray, i - 1, j - 1, cols);
                    string positions = $"{i - 1},{result.start}-{result.end}";

                    if (!addedNumbersPositions.Contains(positions))
                    {
                        adjacentNumbers[count++] = Convert.ToInt32(result.number);
                        addedNumbersPositions.Add(positions);
                    }
                }
                    

                // Check Up right
                if (count < 2 && i - 1 > 0 && j + 1 < cols && char.IsDigit(charArray[i - 1, j + 1]))
                {
                    var result = CheckDigits(charArray, i - 1, j + 1, cols);
                    string positions = $"{i - 1},{result.start}-{result.end}";

                    if (!addedNumbersPositions.Contains(positions))
                    {
                        adjacentNumbers[count++] = Convert.ToInt32(result.number);
                        addedNumbersPositions.Add(positions);
                    }
                }
                    

                // Check Down left
                if (count < 2 && i + 1 < rows && j - 1 >= 0 && char.IsDigit(charArray[i + 1, j - 1]))
                {
                    var result = CheckDigits(charArray, i + 1, j - 1, cols);
                    string positions = $"{i + 1},{result.start}-{result.end}";

                    if (!addedNumbersPositions.Contains(positions))
                    {
                        adjacentNumbers[count++] = Convert.ToInt32(result.number);
                        addedNumbersPositions.Add(positions);
                    }
                }
                    

                // Check Down right
                if (count < 2 && i + 1 < rows && j + 1 < cols && char.IsDigit(charArray[i + 1, j + 1]))
                {
                    var result = CheckDigits(charArray, i + 1, j + 1, cols);
                    string positions = $"{i + 1},{result.start}-{result.end}";

                    if (!addedNumbersPositions.Contains(positions))
                    {
                        adjacentNumbers[count++] = Convert.ToInt32(result.number);
                        addedNumbersPositions.Add(positions);
                    }
                }
                    
                // If exactly two numbers are adjacent, it's a gear
                if (count == 2)
                {
                    Console.WriteLine($"Gear found at position ({i}, {j}) with adjacent numbers {adjacentNumbers[0]} and {adjacentNumbers[1]}");
                    return (adjacentNumbers[0], adjacentNumbers[1]);
                }
            }

            return (0, 0); // Return (0, 0) if no gear is found
        }

    }
}