namespace AdventOfCode2024.Puzzles
{
    public class Day5 : IPuzzle
    {
        public void Solve()
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var pairs = File.ReadAllLines("inputs/day5.txt");

            var updates = File.ReadAllLines("inputs/day5.1.txt");

            var total = 0;

            foreach (var update in updates)
            {
                var pageIndexDict = new Dictionary<string, int>();

                var pages = update.Split(",");
                for (int i = 0; i < pages.Length; i++)
                {
                    pageIndexDict.Add(pages[i], i);
                }

                if (UpdateFitsPairs(pageIndexDict, pairs))
                {
                    // get middle page number
                    var middleIndex = pages.Length / 2;

                    total += int.Parse(pages[middleIndex]);
                }
                else
                {
                    // fix pairs....

                }
            }

            Console.WriteLine(total);
        }

        private static bool UpdateFitsPairs(Dictionary<string, int> pageIndexDict, string[] pairs)
        {
            foreach (var pair in pairs)
            {
                var splitPair = pair.Split("|");

                if (pageIndexDict.TryGetValue(splitPair[0], out var leftIndex) && pageIndexDict.TryGetValue(splitPair[1], out var rightIndex))
                {
                    if (leftIndex > rightIndex) return false;
                }
            }

            return true;
        }

        private static void PartTwo()
        {
            var pairs = File.ReadAllLines("inputs/day5.txt");
            var updates = File.ReadAllLines("inputs/day5.1.txt");

            var total = 0;

            var leftPageBeforePagesDict = new Dictionary<string, List<string>>();

            foreach (var pair in pairs)
            {
                var splitPair = pair.Split("|");

                var leftValue = splitPair[0];
                var rightValue = splitPair[1];

                if (leftPageBeforePagesDict.TryGetValue(leftValue, out var afterList))
                {
                    afterList.Add(rightValue);
                }
                else
                {
                    leftPageBeforePagesDict[leftValue] = [rightValue];
                }
            }

            foreach (var update in updates)
            {
                var pageIndexDict = new Dictionary<string, int>();

                var pages = update.Split(",").ToList();
                for (int i = 0; i < pages.Count; i++)
                {
                    pageIndexDict.Add(pages[i], i);
                }

                if (!UpdateFitsPairs(pageIndexDict, pairs))
                {
                    // spent way too long trying to implement a crappy bubble sort before googling and remembering that you
                    // can super easily make custom sorting comparers 
                    pages.Sort((x, y) =>
                    {
                        // x is less than y
                        if (leftPageBeforePagesDict.TryGetValue(x, out var afterListX))
                        {
                            if (afterListX.Contains(y))
                            {
                                return -1;
                            }
                        }

                        // x is greater than y
                        if (leftPageBeforePagesDict.TryGetValue(y, out var afterListY))
                        {
                            if (afterListY.Contains(x))
                            {
                                return 1;
                            }
                        }

                        return 0; // both values are same or there is no order specified 
                    });

                    var middleIndex = pages.Count / 2;

                    total += int.Parse(pages[middleIndex]);
                }
            }

            Console.WriteLine(total);
        }
    }
}
