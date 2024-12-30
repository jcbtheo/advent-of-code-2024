using AdventOfCode2024.Puzzles;

namespace AdventOfCode2024
{
    public class Program
    {
        static void Main(string[] args)
        {
            GetPuzzleInstanceForDay(7).Solve();
        }
        private static IPuzzle GetPuzzleInstanceForDay(int day)
        {
            var t = Type.GetType($"AdventOfCode2024.Puzzles.Day{day}");
            var instance = Activator.CreateInstance(t!) as IPuzzle;

            Console.WriteLine($"Day {day}:");

            return instance!;
        }
    }
}