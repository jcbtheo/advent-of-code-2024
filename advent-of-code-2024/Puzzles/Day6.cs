namespace AdventOfCode2024.Puzzles
{
    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    public class Day6 : IPuzzle
    {
        public void Solve()
        {
            PartOne();
        }

        private static void PartOne()
        {
            var map = new List<List<string>>();

            (int, int) startPosition = (0,0); // y (higher is lower on the map), x (higher is more right on the map)
            var currentDirection = Direction.North;

            var lines = File.ReadAllLines("inputs/day6.txt");
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                for (var j = 0; j < line.Length; j++)
                {
                    if (line[j] == '^')
                    {
                        startPosition = (i, j);
                        break;
                    }
                }

                map.Add(line.Select(x => new string(x, 1)).ToList());
            }

            var disinctPositions = new HashSet<(int, int)>();

            var currentPosition = startPosition;
            while (true)
            {
                disinctPositions.Add(currentPosition);
                var nextPosition = GetNextPosition(currentPosition, currentDirection);
                if (NextPositionIsOutOfBounds(map, nextPosition))
                {
                    break;
                }

                if (NextPositionIsObstacle(map, nextPosition))
                {
                    currentDirection = TurnRight(currentDirection);
                    currentPosition = GetNextPosition(currentPosition, currentDirection);
                    continue;
                }

                currentPosition = nextPosition;
            }

            Console.WriteLine(disinctPositions.Count);

            

            var loops = 0;

            foreach(var pos in disinctPositions)
            {
                if (pos == startPosition) continue;

                var mapCopy = map.Select(x => x.ToList()).ToList();
                mapCopy[pos.Item1][pos.Item2] = "#";

                currentDirection = Direction.North;
                currentPosition = startPosition;

                var visitedObstacleAndDirection = new HashSet<(int, int, Direction)>();



                while (true)
                {
                    var nextPosition = GetNextPosition(currentPosition, currentDirection);
                    if (NextPositionIsOutOfBounds(mapCopy, nextPosition))
                    {
                        break;
                    }

                    if (NextPositionIsObstacle(mapCopy, nextPosition))
                    {
                        if (!visitedObstacleAndDirection.Add((nextPosition.Item1, nextPosition.Item2, currentDirection)))
                        {
                            loops++;
                            break;
                        }

                        currentDirection = TurnRight(currentDirection);
                        currentPosition = GetNextPosition(currentPosition, currentDirection);
                        continue;
                    }

                    currentPosition = nextPosition;
                }
            }
            
            Console.WriteLine(loops);

            //foreach (var position in disinctPositions)
            //{
            //    map[position.Item1][position.Item2] = "X";
            //}

            foreach (var line in map)
            {
                Console.WriteLine(string.Join(string.Empty, line));
            }
        }

        private static bool NextPositionIsOutOfBounds(List<List<string>> map, (int, int) nextPosition)
        {
            try
            {
               _ = map[nextPosition.Item1][nextPosition.Item2];
                return false;
            }
            catch
            {
                return true;
            }
        }

        private static bool NextPositionIsObstacle(List<List<string>> map, (int, int) nextPosition)
        {
            return map[nextPosition.Item1][nextPosition.Item2] == "#";
        }

        private static (int, int) GetNextPosition((int, int) currentPosition, Direction currentDirection)
        {
            return currentDirection switch
            {
                Direction.North => (currentPosition.Item1 - 1, currentPosition.Item2),
                Direction.South => (currentPosition.Item1 + 1, currentPosition.Item2),
                Direction.West => (currentPosition.Item1, currentPosition.Item2 - 1),
                Direction.East => (currentPosition.Item1, currentPosition.Item2 + 1),
                _ => throw new NotImplementedException(),
            };
        }

        private static Direction TurnRight(Direction currentDirection)
        {
            return currentDirection switch
            {
                Direction.North => Direction.East,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                Direction.East => Direction.South,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
