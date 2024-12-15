using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day15Service(IInputService inputService) : SingleDayService(inputService, 2024, 15)
    {
        public async Task<long> Part1()
        {
            var input = (await InputService.GetInputAsList(year, day, "\n\n")).ToArray();
            var moves = input[1];
            var lines = input[0].Split('\n');
            char[,] map = new char[lines[0].Length, lines.Length];
            (int x, int y) robot = (-1, -1);
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '@')
                    {
                        robot = (x, y);
                        map[x, y] = '.';
                    }
                    else map[x, y] = line[x];
                }
            }
            foreach (char move in moves)
            {
                robot = TryMove(map, robot, move);
            }
            long sum = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == 'O') sum += 100 * y + x;
                }
            }
            return sum;
        }

        private static (int x, int y) TryMove(char[,] map, (int x, int y) robot, char move)
        {
            (int dx, int dy) direction;
            switch (move)
            {
                case '\n':
                    return robot;
                case '^':
                    direction = (0, -1);
                    break;
                case '<':
                    direction = (-1, 0);
                    break;
                case 'v':
                    direction = (0, 1);
                    break;
                case '>':
                    direction = (1, 0);
                    break;
                default:
                    throw new Exception();
            }
            int newX = robot.x + direction.dx;
            int newY = robot.y + direction.dy;
            var obstacle = map[newX, newY];
            if (obstacle == '.') return (newX, newY);
            else if (obstacle == '#') return robot;
            else
            {
                do
                {
                    newX += direction.dx;
                    newY += direction.dy;
                    obstacle = map[newX, newY];
                    if (obstacle == '#') return robot;
                    else if (obstacle == '.')
                    {
                        map[newX, newY] = 'O';
                        (int x, int y) newRobot = (robot.x + direction.dx, robot.y + direction.dy);
                        map[newRobot.x, newRobot.y] = '.';
                        return newRobot;
                    }
                } while (true);
            }
        }

        public async Task<long> Part2()
        {
            var input = (await InputService.GetInputAsList(year, day, "\n\n")).ToArray();
            var moves = input[1];
            var lines = input[0].Split('\n');
            char[,] map = new char[lines[0].Length * 2, lines.Length];
            (int x, int y) robot = (-1, -1);
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '@')
                    {
                        robot = (x * 2, y);
                        map[x * 2, y] = '.';
                        map[x * 2 + 1, y] = '.';
                    }
                    else if (line[x] == 'O')
                    {
                        map[x * 2, y] = '[';
                        map[x * 2 + 1, y] = ']';
                    }
                    else
                    {
                        map[x * 2, y] = line[x];
                        map[x * 2 + 1, y] = line[x];
                    }
                }
            }
            foreach (char move in moves)
            {
                robot = TryMoveWide(map, robot, move);
            }
            long sum = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == '[') sum += 100 * y + x;
                }
            }
            return sum;
        }

        private static (int x, int y) TryMoveWide(char[,] map, (int x, int y) robot, char move)
        {
            (int dx, int dy) direction;
            switch (move)
            {
                case '\n':
                    return robot;
                case '^':
                    direction = (0, -1);
                    break;
                case '<':
                    direction = (-1, 0);
                    break;
                case 'v':
                    direction = (0, 1);
                    break;
                case '>':
                    direction = (1, 0);
                    break;
                default:
                    throw new Exception();
            }
            int newX = robot.x + direction.dx;
            int newY = robot.y + direction.dy;
            var obstacle = map[newX, newY];
            if (obstacle == '.') return (newX, newY);
            else if (obstacle == '#') return robot;
            else
            {
                var fullObstacle = obstacle switch
                {
                    '[' => (newX, newY),
                    ']' => (newX - 1, newY),
                    _ => throw new Exception()
                };
                if (TryPush(map, fullObstacle, direction))
                {
                    Push(map, fullObstacle, direction);
                    return (newX, newY);
                }
                else return robot;
            }
        }

        private static void Push(char[,] map, (int x, int y) box, (int dx, int dy) direction)
        {
            if (direction.dy == 0)
            {
                if (direction.dx == -1)
                {
                    int newX = box.x - 1;
                    if (map[newX, box.y] == ']') Push(map, (newX - 1, box.y), direction);
                    map[newX, box.y] = '[';
                    map[box.x, box.y] = ']';
                    map[box.x + 1, box.y] = '.';
                }
                else
                {
                    int newX = box.x + 2;
                    if (map[newX, box.y] == '[') Push(map, (newX, box.y), direction);
                    map[newX, box.y] = ']';
                    map[box.x + 1, box.y] = '[';
                    map[box.x, box.y] = '.';
                }
            }
            else
            {
                int newY = box.y + direction.dy;
                var obstacle = map[box.x, newY];
                switch (obstacle)
                {
                    case '[':
                        Push(map, (box.x, newY), direction); 
                        break;
                    case ']':
                        Push(map, (box.x - 1, newY), direction);
                        break;
                }
                if (map[box.x + 1, newY] == '[')
                {
                    Push(map, (box.x + 1, newY), direction);
                }
                map[box.x, newY] = '[';
                map[box.x + 1, newY] = ']';
                map[box.x, box.y] = '.';
                map[box.x + 1, box.y] = '.';
            }
        }

        private static bool TryPush(char[,] map, (int x, int y) box, (int dx, int dy) direction)
        {
            if (direction.dy == 0)
            {
                if (direction.dx == -1)
                {
                    int newX = box.x - 1;
                    var obstacle = map[newX, box.y];
                    return obstacle switch
                    {
                        '.' => true,
                        '#' => false,
                        ']' => TryPush(map, (newX - 1, box.y), direction),
                        _ => throw new Exception()
                    };
                }
                else
                {
                    int newX = box.x + 2;
                    var obstacle = map[newX, box.y];
                    return obstacle switch
                    {
                        '.' => true,
                        '#' => false,
                        '[' => TryPush(map, (newX, box.y), direction),
                        _ => throw new Exception()
                    };
                }
            }
            else
            {
                int newY = box.y + direction.dy;
                var obstacle = map[box.x, newY];
                bool canPushLeft = obstacle switch
                {
                    '.' => true,
                    '#' => false,
                    '[' => TryPush(map, (box.x, newY), direction),
                    ']' => TryPush(map, (box.x - 1, newY), direction),
                    _ => throw new Exception()
                };

                obstacle = map[box.x + 1, newY];
                return canPushLeft && obstacle switch
                {
                    '.' => true,
                    '#' => false,
                    '[' => TryPush(map, (box.x + 1, newY), direction),
                    ']' => true,
                    _ => throw new Exception()
                };
            }
        }
    }
}
