namespace AdventOfCode2024.Puzzles
{
    public class Day4 : IPuzzle
    {
        public void Solve()
        {
            //PartOne();
            PartTwo();
        }

        public static void PartOne()
        {
            var wordToFind = "XMAS";

            var chars = new List<List<string>>();

            var lines = File.ReadAllLines("inputs/day4.txt");
            foreach (var line in lines)
            {
                chars.Add(line.Select(x => new string(x, 1)).ToList());
            }

            var height = chars.Count; //10
            var width = chars[0].Count; //10

            var leftMinIndex = wordToFind.Length - 1; // 3
            var rightMaxIndex = width - wordToFind.Length; // 6
            var heightMaxIndex = height - wordToFind.Length; // 6

            var total = 0;

            for (int heightIndex = 0; heightIndex < height; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < width; widthIndex++)
                {
                    var findsFromIndex = 0;

                    if (heightIndex <= heightMaxIndex)
                    {
                        if (HasVerticalDown(chars, heightIndex, widthIndex)) findsFromIndex++;

                        if (widthIndex >= leftMinIndex)
                        {
                            if (HasDiagonalDownLeft(chars, heightIndex, widthIndex)) findsFromIndex++;
                        }

                        if (widthIndex <= rightMaxIndex)
                        {
                            if (HasDiagonalDownRight(chars, heightIndex, widthIndex)) findsFromIndex++;
                        }
                    }

                    if (widthIndex <= rightMaxIndex)
                    {
                        if (HasHorizontalRight(chars, heightIndex, widthIndex)) findsFromIndex++;
                    }

                    total += findsFromIndex;
                }
            }

            Console.WriteLine(total);
        }

        public static bool HasVerticalDown(List<List<string>> chars, int heightIndex, int widthIndex)
        {
            var char1 = chars[heightIndex][widthIndex];
            var char2 = chars[heightIndex + 1][widthIndex];
            var char3 = chars[heightIndex + 2][widthIndex];
            var char4 = chars[heightIndex + 3][widthIndex];

            var value = char1 + char2 + char3 + char4;

            return value == "XMAS" || value == "SAMX";
        }

        public static bool HasHorizontalRight(List<List<string>> chars, int heightIndex, int widthIndex)
        {
            var char1 = chars[heightIndex][widthIndex];
            var char2 = chars[heightIndex][widthIndex + 1];
            var char3 = chars[heightIndex][widthIndex + 2];
            var char4 = chars[heightIndex][widthIndex + 3];

            var value = char1 + char2 + char3 + char4;

            return value == "XMAS" || value == "SAMX";
        }

        public static bool HasDiagonalDownRight(List<List<string>> chars, int heightIndex, int widthIndex)
        {
            var char1 = chars[heightIndex][widthIndex];
            var char2 = chars[heightIndex + 1][widthIndex + 1];
            var char3 = chars[heightIndex + 2][widthIndex + 2];
            var char4 = chars[heightIndex + 3][widthIndex + 3];

            var value = char1 + char2 + char3 + char4;

            return value == "XMAS" || value == "SAMX";
        }

        public static bool HasDiagonalDownLeft(List<List<string>> chars, int heightIndex, int widthIndex)
        {
            var char1 = chars[heightIndex][widthIndex];
            var char2 = chars[heightIndex + 1][widthIndex - 1];
            var char3 = chars[heightIndex + 2][widthIndex - 2];
            var char4 = chars[heightIndex + 3][widthIndex - 3];

            var value = char1 + char2 + char3 + char4;

            return value == "XMAS" || value == "SAMX";
        }

        public static void PartTwo()
        {
            var chars = new List<List<string>>();

            var lines = File.ReadAllLines("inputs/day4.txt");
            foreach (var line in lines)
            {
                chars.Add(line.Select(x => new string(x, 1)).ToList());
            }

            var height = chars.Count; //10
            var width = chars[0].Count; //10

            var total = 0;

            for (int heightIndex = 1; heightIndex < height - 1; heightIndex++)
            {
                for (int widthIndex = 1; widthIndex < width - 1; widthIndex++)
                {
                    if (chars[heightIndex][widthIndex] != "A")
                    {
                        continue;
                    }

                    var topLeft = chars[heightIndex - 1][widthIndex - 1];
                    var topRight = chars[heightIndex - 1][widthIndex + 1];
                    var bottomLeft = chars[heightIndex + 1][widthIndex - 1];
                    var bottomRight = chars[heightIndex + 1][widthIndex + 1];

                    var diagRight = (topLeft == "S" && bottomRight == "M") || (topLeft == "M" && bottomRight == "S");
                    var diagLeft = (topRight == "S" && bottomLeft == "M") || (topRight == "M" && bottomLeft == "S");

                    if (diagLeft && diagRight)
                    {
                        total++;
                    }
                }
            }

            Console.WriteLine(total);
        }
    }
}
