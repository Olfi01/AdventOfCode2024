using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day21Service(IInputService inputService) : SingleDayService(inputService, 2024, 21)
    {
        public async Task<long> Part1()
        {
            var codes = await InputService.GetInputAsList(year, day);
            return codes.Sum(CalculateComplexity);
        }

        public async Task<long> Part2()
        {
            var codes = await InputService.GetInputAsList(year, day);
            return codes.Sum(CalculatePart2Complexity);
        }

        private static long CalculateComplexity(string code)
        {
            long length = ShortestSequenceForNumeric(code);
            return length * int.Parse(code[..^1]);
        }

        private static long CalculatePart2Complexity(string code)
        {
            long length = ShortestSequenceForNumericPart2(code);
            return length * int.Parse(code[..^1]);
        }

        public static long ShortestSequenceForNumericPart2(string code)
        {
            var position = numericKeypad['A'];
            long length = 0;
            foreach (char c in code)
            {
                var shortestPaths = ShortestSequencesForNumeric(position, c);
                position = numericKeypad[c];
                for (int i = 0; i < 25; i++)
                {
                    shortestPaths = ShortestSequencesForDirectional(shortestPaths);
                }
                length += shortestPaths.First().Length;
            }

            return length;
        }

        public static long ShortestSequenceForNumeric(string code)
        {
            var position = numericKeypad['A'];
            long length = 0;
            foreach (char c in code)
            {
                var shortestNumericPaths = ShortestSequencesForNumeric(position, c);
                position = numericKeypad[c];
                var shortestDirectionalPaths = ShortestSequencesForDirectional(ShortestSequencesForDirectional(shortestNumericPaths));
                length += shortestDirectionalPaths.First().Length;
            }

            return length;
        }

        public static HashSet<string> ShortestSequencesForNumeric((int x, int y) start, char c)
        {
            var goal = numericKeypad[c];
            string result = "";
            if (goal.x == 0 && start.y == 3)
            {
                for (int y = start.y; y > goal.y; y--)
                {
                    result += '^';
                }
                for (int y = start.y; y < goal.y; y++)
                {
                    result += 'v';
                }
                for (int x = start.x; x > goal.x; x--)
                {
                    result += '<';
                }
                return [result + 'A'];
            }
            else if (goal.y == 3 && start.x == 0)
            {
                for (int x = start.x; x < goal.x; x++)
                {
                    result += '>';
                }
                for (int x = start.x; x > goal.x; x--)
                {
                    result += '<';
                }
                for (int y = start.y; y < goal.y; y++)
                {
                    result += 'v';
                }
                return [result + 'A'];
            }
            else
            {
                string part1 = "";
                string part2 = "";
                for (int x = start.x; x < goal.x; x++)
                {
                    part1 += '>';
                }
                for (int x = start.x; x > goal.x; x--)
                {
                    part1 += '<';
                }
                for (int y = start.y; y < goal.y; y++)
                {
                    part2 += 'v';
                }
                for (int y = start.y; y > goal.y; y--)
                {
                    part2 += '^';
                }
                return [part1 + part2 + 'A', part2 + part1 + 'A'];
            }
        }

        private static readonly Dictionary<string, string> directionalCache = [];

        public static HashSet<string> ShortestSequencesForDirectional(HashSet<string> possibleCodes)
        {
            List<string> results = [];
            foreach (var code in possibleCodes)
            {
                if (directionalCache.TryGetValue(code, out var value))
                {
                    results.Add(value);
                }
                else
                {
                    var position = directionalKeypad['A'];
                    string result = "";
                    foreach (char c in code)
                    {
                        result += ShortestSequencesForDirectional(position, c);
                        position = directionalKeypad[c];
                    }
                    directionalCache[code] = result;
                    results.Add(result);
                }
            }
            return results.Where(r => r.Length == results.Min(r0 => r0.Length)).ToHashSet();
        }


        private static string ShortestSequencesForDirectional((int x, int y) start, char c)
        {
            var goal = directionalKeypad[c];
            string result = "";
            if (goal.x == 0 && start.y == 0)
            {
                for (int y = start.y; y < goal.y; y++)
                {
                    result += 'v';
                }
                for (int x = start.x; x > goal.x; x--)
                {
                    result += '<';
                }
                return result + 'A';
            }
            else if (goal.y == 0 && start.x == 0)
            {
                for (int x = start.x; x < goal.x; x++)
                {
                    result += '>';
                }
                for (int x = start.x; x > goal.x; x--)
                {
                    result += '<';
                }
                for (int y = start.y; y > goal.y; y--)
                {
                    result += '^';
                }
                return result + 'A';
            }
            else
            {
                for (int x = start.x; x < goal.x; x++)
                {
                    result += '>';
                }
                for (int y = start.y; y < goal.y; y++)
                {
                    result += 'v';
                }
                for (int x = start.x; x > goal.x; x--)
                {
                    result += '<';
                }
                for (int y = start.y; y > goal.y; y--)
                {
                    result += '^';
                }
                return result + 'A';
            }
        }

        private static readonly Dictionary<char, (int x, int y)> numericKeypad = new()
        {
            { '7', (0, 0) }, { '8', (1, 0) }, { '9', (2, 0) },
            { '4', (0, 1) }, { '5', (1, 1) }, { '6', (2, 1) },
            { '1', (0, 2) }, { '2', (1, 2) }, { '3', (2, 2) },
                             { '0', (1, 3) }, { 'A', (2, 3) }
        };

        private static readonly Dictionary<char, (int x, int y)> directionalKeypad = new()
        {
                             { '^', (1, 0) }, { 'A', (2, 0) },
            { '<', (0, 1) }, { 'v', (1, 1) }, { '>', (2, 1) },
        };
    }
}
