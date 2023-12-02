using System.Diagnostics;

namespace Utilities
{
    public static class AoCUtilities
    {
        public static string[] GetInput()
        {
            string[] lines = File.ReadAllLines("input.txt");
            return lines;
        }
    }
}