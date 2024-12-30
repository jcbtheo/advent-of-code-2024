using System.Text;

namespace AdventOfCode2024.Puzzles
{
    public class Equation
    {
        public long TestValue { get; set; }
        public List<long> Arguments { get; set; } = null!;
    }

    public class Day7 : IPuzzle
    {
        public void Solve()
        {
            //PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var lines = File.ReadAllLines("inputs/day7.txt");

            var eqs = lines
                .Select(line =>
                {
                    var halves = line.Split(":");

                    return new Equation
                    {
                        TestValue = long.Parse(halves[0]),
                        Arguments = halves[1].Trim().Split(" ").Select(x => long.Parse(x)).ToList()
                    };
                })
                .ToList();

            // 000 (+++) to 111 (***)

            // 0 -> 7 base 2

            var cumulativeTotal = 0l;
            foreach (var eq in eqs)
            {
                var numBits = eq.Arguments.Count - 1;

                var all1s = new int[numBits];
                Array.Fill(all1s, 1);

                var max = Convert.ToInt32(string.Join("", all1s), 2);

                var ix = 0;
                while (ix <= max)
                {
                    var bitArrayStrings = Convert.ToString(ix, 2).PadLeft(numBits, '0').ToCharArray();
                    //Console.WriteLine(bitArrayStrings);

                    var total = eq.Arguments[0];
                    for (var i = 0; i < numBits; i++)
                    {
                        switch (bitArrayStrings[i])
                        {
                            case '0':
                                total += eq.Arguments[i + 1];
                                break;
                            case '1':
                                total *= eq.Arguments[i + 1];
                                break;
                            default:
                                throw new ArgumentException();
                        };
                    }

                    if (total == eq.TestValue)
                    {
                        cumulativeTotal += total;
                        break;
                    }
                    ix++;
                }
            }

            Console.WriteLine(cumulativeTotal);
        }

        private void PartTwo()
        {
            var lines = File.ReadAllLines("inputs/day7.txt");

            var eqs = lines
                .Select(line =>
                {
                    var halves = line.Split(":");

                    return new Equation
                    {
                        TestValue = long.Parse(halves[0]),
                        Arguments = halves[1].Trim().Split(" ").Select(x => long.Parse(x)).ToList()
                    };
                })
                .ToList();

            var cumulativeTotal = 0L;
            foreach (var eq in eqs)
            {
                var numBits = eq.Arguments.Count - 1;

                var all2s = new int[numBits];
                Array.Fill(all2s, 2);

                var max = BaseFromString(all2s);

                var ix = 0;
                while (ix <= max)
                {
                    var bitArrayStrings = Base3FromInt(ix, numBits);

                    var total = eq.Arguments[0];
                    for (var i = 0; i < numBits; i++)
                    {
                        switch (bitArrayStrings[i])
                        {
                            case '0':
                                total += eq.Arguments[i + 1];
                                break;
                            case '1':
                                total *= eq.Arguments[i + 1];
                                break;
                            case '2':
                                total = long.Parse(total + eq.Arguments[i + 1].ToString());
                                break;
                            default:
                                throw new ArgumentException();
                        };
                    }

                    if (total == eq.TestValue)
                    {
                        cumulativeTotal += total;
                        break;
                    }
                    ix++;
                }
            }

            Console.WriteLine(cumulativeTotal);
        }

        private static string Base3FromInt(int i, int operandsCount)
        {
            var builder = new StringBuilder();
            do
            {
                builder.Append(i % 3);
                i /= 3;
            } while (i > 0);

            var output = new string(builder.ToString().Reverse().ToArray());

            return output!.PadLeft(operandsCount, '0');
        }

        private static long BaseFromString(int[] input)
        {
            var total = 0L;

            for (int i = 0; i < input.Length; i++)
            {
                total += (long)(input[i] * Math.Pow(3, i));
            }

            return total;
        }
    }
}
