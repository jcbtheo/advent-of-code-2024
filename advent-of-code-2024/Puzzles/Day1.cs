namespace AdventOfCode2024.Puzzles
{
    public class Day1 : IPuzzle
    {
        public void Solve()
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var left = new List<int>();
            var right = new List<int>();

            var lines = File.ReadAllLines("inputs/day1.txt");
            foreach (var line in lines)
            {
                var inputs = line.Split("   ");

                left.Add(int.Parse(inputs[0]));
                right.Add(int.Parse(inputs[1]));
            }

            left.Sort();
            right.Sort();

            var difTotal = 0;

            for (int i = 0; i < left.Count; i++)
            {
                difTotal += Math.Abs(left[i] - right[i]);
            }

            Console.WriteLine(difTotal);
        }

        private static void PartTwo()
        {
            var left = new List<int>();
            var rightDict = new Dictionary<int, int>();

            var lines = File.ReadAllLines("inputs/day1.txt");
            foreach (var line in lines)
            {
                var inputs = line.Split("   ");

                left.Add(int.Parse(inputs[0]));

                var rightInt = int.Parse(inputs[1]);
                if (!rightDict.TryGetValue(rightInt, out var value))
                {
                    rightDict.Add(rightInt, 1);
                }
                else
                {
                    rightDict[rightInt]++;
                }
            }

            var total = left
                .Select(x => 
                { 
                    if (rightDict.TryGetValue(x, out var value))
                    {
                        return x * (value);
                    }
                    return 0;
                })
                .Sum();

            Console.WriteLine(total);
        }
    }
}
