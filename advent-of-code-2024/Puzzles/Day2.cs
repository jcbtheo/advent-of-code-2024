namespace AdventOfCode2024.Puzzles
{
    internal class Day2 : IPuzzle
    {
        public void Solve()
        {
            //PartOne();
            PartTwo();
        }

        public static void PartOne()
        {
            var totalSafe = 0;

            var lines = File.ReadAllLines("inputs/day2.txt");
            foreach (var line in lines)
            {
                if (IsSafe(line.Split(' ').Select(x => int.Parse(x)).ToList()))
                {
                    totalSafe++;
                }
            }

            Console.WriteLine(totalSafe);
        }

        private static bool IsSafe(List<int> report)
        {
            bool? increasing = null;

            for (int i = 0; i < report.Count - 1; i++)
            {
                var dif = report[i] - report[i + 1];

                if (increasing is null)
                {
                    if (dif == 0 || dif > 3 || dif < -3)
                    {
                        return false;
                    }
                    if (dif < 0)
                    {
                        increasing = true;
                    }
                    else if (dif > 0)
                    {
                        increasing = false;
                    }
                }

                if (dif == 0 || dif > 3 || dif < -3)
                {
                    return false;
                }
                if (dif < 0)
                {
                    if (increasing == false) return false;
                }
                else if (dif > 0)
                {
                    if (increasing == true) return false;
                }
            }

            return true;
        }

        public static void PartTwo()
        {
            var totalSafe = 0;

            var lines = File.ReadAllLines("inputs/day2.txt");
            foreach (var line in lines)
            {
                var report = line.Split(' ').Select(x => int.Parse(x)).ToList();

                if (IsSafe(report))
                {
                    totalSafe++;
                }
                else
                {
                    // brute force lol
                    for (int i = 0; i < report.Count; i++)
                    {
                        var newReport = new List<int>(report);
                        newReport.RemoveAt(i);

                        if (IsSafe(newReport))
                        {
                            totalSafe++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(totalSafe);
        }
    }
}
