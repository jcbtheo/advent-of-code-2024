using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzles
{
    public class Day3 : IPuzzle
    {
        public void Solve()
        {
            //PartOne();
            PartTwo();
        }

        public static void PartOne()
        {
            var line = File.ReadAllText("inputs/day3.txt");

            var regexPattern = @"mul\(\d{1,3},\d{1,3}\)";
            var total = 0;
            foreach(Match match in Regex.Matches(line, regexPattern).Cast<Match>())
            {
                total += MultiplyFromString(match.Value);
            }

            Console.WriteLine(total);
        }

        public static int MultiplyFromString(string expression)
        {
            var pattern = @"\d{1,3}";

            var matches = Regex.Matches(expression, pattern);

            if (matches.Count != 2)
            {
                throw new ArgumentException(expression);
            }

            return int.Parse(matches[0].Value) * int.Parse(matches[1].Value);
        }

        public static void PartTwo()
        {
            var line = File.ReadAllText("inputs/day3.txt");

            var total = 0;
            foreach(var mul in GetActiveMuls(line))
            {
                total += MultiplyFromString(mul);
            }

            Console.WriteLine(total);
        }

        public static List<string> GetActiveMuls(string line)
        {
            var doToken = "do()";
            var dontToken = "don't()";
            var mulTokenPattern = @"mul\(\d{1,3},\d{1,3}\)";

            var include = true;

            var tokens = new List<string>();

            for (int i = 0; i < line.Length; i++)
            {
                if (i + doToken.Length <= line.Length && line.Substring(i, doToken.Length) == doToken)
                {
                    include = true;
                    i += doToken.Length - 1;
                }

                if (i + dontToken.Length <= line.Length && line.Substring(i, dontToken.Length) == dontToken)
                {
                    include = false;
                    i += dontToken.Length - 1;
                }

                if (include)
                {
                    var mulStringMin = 8;
                    var mulStringMax = 12;

                    for (int j = mulStringMin; j <= mulStringMax; j++)
                    {
                        if (!(i + j > line.Length))
                        {
                            var match = Regex.Match(line.Substring(i, j), mulTokenPattern);
                            if (match.Success)
                            {
                                tokens.Add(match.Value);
                                i += j - 1;
                                break;
                            }
                        }
                    }
                }
            }

            return tokens;
        }
    }
}
