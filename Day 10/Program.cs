// AUTHOR: RICARDO MARTENS
// DATE: 10/12/2023
// DAY 10 - ADVENT OF CODE

using System.Drawing;

using Utilities;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = AoCUtilities.GetInput();

            int rows = lines.Length;
            int cols = lines.Max(line => line.Length);

            char[,] pipeBoard = new char[rows, cols];
            bool[,] visited = new bool[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    pipeBoard[i, j] = lines[i][j];
                }
            }

            int startX = 0, startY = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (pipeBoard[i, j] == 'S')
                    {
                        startX = i;
                        startY = j;
                        break;
                    }
                }
            }

            int maxDistance = 0;
            List<Point> points = new List<Point>();
            TraversePipe(pipeBoard, visited, startX, startY, ref maxDistance, points);

            Console.WriteLine($"The farthest point from the start is {maxDistance} steps away.");

            // First, mark all cells outside the loop
            for (int i = 0; i < rows; i++)
            {
                FloodFill(pipeBoard, i, 0, '.', 'O'); // Left edge
                FloodFill(pipeBoard, i, cols - 1, '.', 'O'); // Right edge
            }
            for (int j = 0; j < cols; j++)
            {
                FloodFill(pipeBoard, 0, j, '.', 'O'); // Top edge
                FloodFill(pipeBoard, rows - 1, j, '.', 'O'); // Bottom edge
            }

            // Now count Inside cells
            int countEnclosed = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (pipeBoard[i, j] == '.')
                    {
                        countEnclosed++;
                    }
                }
            }

            Console.WriteLine($"The total number of enclosed cells is {countEnclosed}.");

            // Calculate the area using the Shoelace formula
            double area = CalculateArea(points);

            // Calculate the number of boundary points
            int boundaryPoints = points.Count;

            // Calculate the number of interior points using Pick's theorem
            int interiorPoints = CalculateInteriorPoints(area, boundaryPoints);

            Console.WriteLine($"The total number of enclosed cells is {interiorPoints}.");
        }
        static void TraversePipe(char[,] pipeBoard, bool[,] visited, int startX, int startY, ref int maxDistance, List<Point> points)
        {
            int rows = pipeBoard.GetLength(0);
            int cols = pipeBoard.GetLength(1);

            Queue<Tuple<int, int, int>> queue = new Queue<Tuple<int, int, int>>();
            queue.Enqueue(new Tuple<int, int, int>(startX, startY, 0));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int x = current.Item1;
                int y = current.Item2;
                int distance = current.Item3;

                if (x < 0 || x >= rows || y < 0 || y >= cols || visited[x, y] || pipeBoard[x, y] == '.')
                {
                    continue;
                }

                visited[x, y] = true;
                maxDistance = Math.Max(maxDistance, distance);
                points.Add(new Point { X = x, Y = y });

                switch (pipeBoard[x, y])
                {
                    case '|':
                    case 'S':
                        queue.Enqueue(new Tuple<int, int, int>(x - 1, y, distance + 1)); // Up
                        queue.Enqueue(new Tuple<int, int, int>(x + 1, y, distance + 1)); // Down
                        break;
                    case '-':
                        queue.Enqueue(new Tuple<int, int, int>(x, y - 1, distance + 1)); // Left
                        queue.Enqueue(new Tuple<int, int, int>(x, y + 1, distance + 1)); // Right
                        break;
                    case 'L':
                        queue.Enqueue(new Tuple<int, int, int>(x, y + 1, distance + 1)); // Right
                        queue.Enqueue(new Tuple<int, int, int>(x - 1, y, distance + 1)); // Up
                        break;
                    case 'J':
                        queue.Enqueue(new Tuple<int, int, int>(x, y - 1, distance + 1)); // Left
                        queue.Enqueue(new Tuple<int, int, int>(x - 1, y, distance + 1)); // Up
                        break;
                    case '7':
                        queue.Enqueue(new Tuple<int, int, int>(x, y - 1, distance + 1)); // Left
                        queue.Enqueue(new Tuple<int, int, int>(x + 1, y, distance + 1)); // Down
                        break;
                    case 'F':
                        queue.Enqueue(new Tuple<int, int, int>(x, y + 1, distance + 1)); // Right
                        queue.Enqueue(new Tuple<int, int, int>(x + 1, y, distance + 1)); // Down
                        break;
                }
            }
        }

        static void FloodFill(char[,] pipeBoard, int x, int y, char target, char replacement)
        {
            int rows = pipeBoard.GetLength(0);
            int cols = pipeBoard.GetLength(1);

            if (x < 0 || x >= rows || y < 0 || y >= cols || pipeBoard[x, y] != target)
            {
                return;
            }

            pipeBoard[x, y] = replacement;

            // Check the four directly adjacent cells
            FloodFill(pipeBoard, x - 1, y, target, replacement); // Up
            FloodFill(pipeBoard, x + 1, y, target, replacement); // Down
            FloodFill(pipeBoard, x, y - 1, target, replacement); // Left
            FloodFill(pipeBoard, x, y + 1, target, replacement); // Right

            // Check the four diagonally adjacent cells
            FloodFill(pipeBoard, x - 1, y - 1, target, replacement); // Up-left
            FloodFill(pipeBoard, x - 1, y + 1, target, replacement); // Up-right
            FloodFill(pipeBoard, x + 1, y - 1, target, replacement); // Down-left
            FloodFill(pipeBoard, x + 1, y + 1, target, replacement); // Down-right
        }

        // Function to calculate the area using the Shoelace formula
        public static double CalculateArea(List<Point> points)
        {
            double area = 0;
            int j = points.Count - 1;

            for (int i = 0; i < points.Count; i++)
            {
                area += (points[j].X + points[i].X) * (points[j].Y - points[i].Y);
                j = i;
            }

            return Math.Abs(area / 2);
        }

        // Function to calculate the number of interior points using Pick's theorem
        public static int CalculateInteriorPoints(double area, int boundaryPoints)
        {
            return (int)(area - (boundaryPoints / 2.0) + 1);
        }
    }
}