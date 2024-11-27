// AUTHOR: RICARDO MARTENS
// DATE: 13/12/2023
// DAY 13 - ADVENT OF CODE

using Utilities;

namespace Day05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = AoCUtilities.GetInput().ToList();
            lines.Add(string.Empty);


            int rows = lines.Count;
            int cols = lines.Max(line => line.Length);
            int total = 0;
            int rowCounter = 0;
            int colCounter = 0;
            int previousRowCounter = 0;
            int previousColCounter = 0;
            int left = 0;
            int right = 0;
            int diff = 0;
            bool horizontalExists = false;

            int rowsFound = 0;
            int colsFound = 0;

            List<List<char>> mirrorStrings = new();

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    mirrorStrings.Add(line.ToList());
                }

                else
                {
                    for (int i = 0; i < mirrorStrings.Count; i++)
                    {
                        if (i + 1 != mirrorStrings.Count && mirrorStrings[i].SequenceEqual(mirrorStrings[i + 1]))
                        {
                            // Check for outward reflections
                            Console.WriteLine("Found horizontal reflection!");
                            left = i;
                            right = i + 1;
                            rowCounter++;
                            while (left - 1 >= 0 && right + 1 <= mirrorStrings.Count - 1 && mirrorStrings[left - 1].SequenceEqual(mirrorStrings[right + 1]))
                            {
                                Console.WriteLine("Found another reflection outwards");
                                rowCounter++;
                                left--;
                                right++;
                            }
                            horizontalExists = true;
                            Console.WriteLine($"Currently we have left {left} and right {right} rows as edges");

                            // Calculate how many rows are left up/down of edges
                            // Only left above if we have horizontal, so from i onwards to edge
                            if (left > 0)
                            {

                            }
                            if (rowCounter > previousRowCounter)
                            {
                                Console.WriteLine($"Found a total of {rowCounter} reflected rows, this is bigger than the {previousRowCounter} so keeping this number as its a better reflected row");
                                previousRowCounter = rowCounter;
                                rowsFound = rowCounter;
                            }
                            rowCounter = 0;
                        }
                    }

                    if (mirrorStrings.Count > 0)
                    {
                        int maxLength = mirrorStrings.Max(s => s.Count);

                        // Check for vertical reflections
                        for (int col = 0; col < maxLength - 1; col++)
                        {
                            List<char> column1 = new List<char>();
                            List<char> column2 = new List<char>();

                            for (int row = 0; row < mirrorStrings.Count; row++)
                            {
                                column1.Add(mirrorStrings[row][col]);
                                column2.Add(mirrorStrings[row][col + 1]);
                            }

                            if (column1.SequenceEqual(column2))
                            {
                                Console.WriteLine("Found vertical reflection!");
                                left = col;
                                right = col + 1;
                                colCounter++;
                                while (left - 1 >= 0 && right + 1 <= maxLength - 1 && 
                                    mirrorStrings.Select(s => s[left - 1]).SequenceEqual(mirrorStrings.Select(s => s[right + 1])))
                                {
                                    Console.WriteLine("Found another reflection outwards");
                                    colCounter++;
                                    left--;
                                    right++;
                                }
                                Console.WriteLine($"Currently we have left {left} and right {right} columns as edges");
                                if (horizontalExists && colCounter > previousRowCounter)
                                {
                                    Console.WriteLine($"Since we found a horizontal and a vertical reflection, we have to take the perfect one.");
                                    Console.WriteLine($"In this case we have better columns that reflect than rows, so taking that.");
                                    rowsFound = 0;
                                } 
                                else if (horizontalExists && previousRowCounter > colCounter)
                                {
                                    Console.WriteLine($"Since we found a horizontal and a vertical reflection, we have to take the perfect one.");
                                    Console.WriteLine($"In this case we have better rows that reflect than columns, so taking that.");
                                    colsFound = 0;
                                }
                                //Console.WriteLine($"That means we currently have a total reflections of: {colCounter} columns to the left");
                                //total += (col + 1);
                                //Console.WriteLine(total);
                                mirrorStrings.Clear();
                                if (colCounter > previousColCounter)
                                {
                                    Console.WriteLine($"Found a total of {colCounter} reflected columns, this is bigger than the {previousColCounter} so keeping this number as its a better reflected column");
                                    previousColCounter = colCounter;
                                    colsFound = colCounter;
                                }
                                colCounter = 0;
                                //break;
                            }
                        }
                        if (rowsFound > colsFound)
                        {
                            total += rowsFound * 100;
                        } else
                        {
                            total += colsFound;
                        }
                        Console.WriteLine(total);
                        rowsFound = 0;
                        colsFound = 0;
                        previousRowCounter = 0;
                        previousColCounter = 0;
                        horizontalExists = false;
                        mirrorStrings.Clear();
                    }
                }
            }
            Console.WriteLine(total);
        }
    }
}