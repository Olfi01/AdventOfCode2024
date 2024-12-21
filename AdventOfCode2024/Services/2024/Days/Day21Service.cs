using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public partial class Day21Service(IInputService inputService) : SingleDayService(inputService, 2024, 21)
    {
        public async Task<long> Part1()
        {
            var codes = await InputService.GetInputAsList(year, day);
            return codes.Sum(CalculateComplexity);
        }

        public async Task<long> Part2()
        {
            var codes = await InputService.GetInputAsList(year, day);
            return codes.Sum(CalculateComplexitySmartly);
        }

        private static long CalculateComplexity(string code)
        {
            long length = ShortestSequenceForNumeric(code);
            return length * int.Parse(code[..^1]);
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

        private static long CalculateComplexitySmartly(string code)
        {
            long length = GetBestLength(code);
            return length * int.Parse(code[..^1]);
        }

        public static long GetBestLength(string code, int abstractionalLayers = 25)
        {
            List<string> possibleNumericCodes = [""];

            var position = numericKeypad['A'];
            foreach (char c in code)
            {
                var shortestNumericPaths = ShortestSequencesForNumeric(position, c);
                position = numericKeypad[c];
                List<string> newPossible = [];
                foreach (var path in shortestNumericPaths)
                {
                    newPossible.AddRange(possibleNumericCodes.Select(p => p + path));
                }
                possibleNumericCodes = newPossible;
            }

            long bestLength = long.MaxValue;
            foreach (var possibleCode in possibleNumericCodes)
            {
                DirectionalCode dCode = DirectionalCode.From(possibleCode);
                for (int i = 0; i < abstractionalLayers; i++)
                {
                    dCode = DirectionalCode.Expand(dCode);
                }
                long length = dCode.GetLength();
                if (length < bestLength) bestLength = length;
            }
            return bestLength;
        }

        private partial class DirectionalCode
        {
            public long AU { get; private set; } = 0;
            public long UA { get; private set; } = 0;
            public long AR { get; private set; } = 0;
            public long RA { get; private set; } = 0;
            public long UD { get; private set; } = 0;
            public long DU { get; private set; } = 0;
            public long DR { get; private set; } = 0;
            public long RD { get; private set; } = 0;
            public long LD { get; private set; } = 0;
            public long DL { get; private set; } = 0;
            public long RL { get; private set; } = 0;
            public long UL { get; private set; } = 0;
            public long LR { get; private set; } = 0;
            public long LU { get; private set; } = 0;
            public long LA { get; private set; } = 0;
            public long AL { get; private set; } = 0;
            public long AD { get; private set; } = 0;
            public long UR { get; private set; } = 0;
            public long RU { get; private set; } = 0;
            public long DA { get; private set; } = 0;
            public long AA { get; private set; } = 0;
            public long LL { get; private set; } = 0;
            public long RR { get; private set; } = 0;
            public long UU { get; private set; } = 0;
            public long DD { get; private set; } = 0;

            public long GetLength()
            {
                return AA + AD + AL + AR + AU + DA + DD + DL + DR + DU + LA + LD + LL + LR + LU + RA + RD + RL + RR + RU + UA + UD + UL + UR + UU;
            }

            public static DirectionalCode Expand(DirectionalCode code)
            {
                DirectionalCode result = new();
                // A to ^: <A
                result.AL += code.AU;
                result.LA += code.AU;
                // ^ to A: >A
                result.AR += code.UA;
                result.RA += code.UA;
                // A to >: vA
                result.AD += code.AR;
                result.DA += code.AR;
                // > to A: ^A
                result.AU += code.RA;
                result.UA += code.RA;
                // ^ to v: vA
                result.AD += code.UD;
                result.DA += code.UD;
                // v to ^: ^A
                result.AU += code.DU;
                result.UA += code.DU;
                // v to >: >A
                result.AR += code.DR;
                result.RA += code.DR;
                // > to v: <A
                result.AL += code.RD;
                result.LA += code.RD;
                // < to v: >A
                result.AR += code.LD;
                result.RA += code.LD;
                // v to <: <A
                result.AL += code.DL;
                result.LA += code.DL;
                // > to <: <<A
                result.AL += code.RL;
                result.LL += code.RL;
                result.LA += code.RL;
                // ^ to <: v<A
                result.AD += code.UL;
                result.DL += code.UL;
                result.LA += code.UL;
                // < to >: >>A
                result.AR += code.LR;
                result.RR += code.LR;
                result.RA += code.LR;
                // < to ^: >^A
                result.AR += code.LU;
                result.RU += code.LU;
                result.UA += code.LU;
                // < to A: >>^A
                result.AR += code.LA;
                result.RR += code.LA;
                result.RU += code.LA;
                result.UA += code.LA;
                // A to <: v<<A
                result.AD += code.AL;
                result.DL += code.AL;
                result.LL += code.AL;
                result.LA += code.AL;
                // A to v: <vA
                result.AL += code.AD;
                result.LD += code.AD;
                result.DA += code.AD;
                // ^ to >: v>A
                result.AD += code.UR;
                result.DR += code.UR;
                result.RA += code.UR;
                // > to ^: <^A
                result.AL += code.RU;
                result.LU += code.RU;
                result.UA += code.RU;
                // v to A: ^>A
                result.AU += code.DA;
                result.UR += code.DA;
                result.RA += code.DA;

                result.AA += code.AA + code.LL + code.RR + code.UU + code.DD;

                return result;
            }

            public static DirectionalCode From(string code)
            {
                string s = 'A' + code;
                return new()
                {
                    AA = rAA().Matches(s).Count,
                    AD = rAD().Matches(s).Count,
                    AL = rAL().Matches(s).Count,
                    AR = rAR().Matches(s).Count,
                    AU = rAU().Matches(s).Count,
                    DA = rDA().Matches(s).Count,
                    DD = rDD().Matches(s).Count,
                    DL = rDL().Matches(s).Count,
                    DR = rDR().Matches(s).Count,
                    DU = rDU().Matches(s).Count,
                    LA = rLA().Matches(s).Count,
                    LD = rLD().Matches(s).Count,
                    LL = rLL().Matches(s).Count,
                    LR = rLR().Matches(s).Count,
                    LU = rLU().Matches(s).Count,
                    RA = rRA().Matches(s).Count,
                    RD = rRD().Matches(s).Count,
                    RL = rRL().Matches(s).Count,
                    RR = rRR().Matches(s).Count,
                    RU = rRU().Matches(s).Count,
                    UA = rUA().Matches(s).Count,
                    UD = rUD().Matches(s).Count,
                    UL = rUL().Matches(s).Count,
                    UR = rUR().Matches(s).Count,
                    UU = rUU().Matches(s).Count
                };
            }

            [GeneratedRegex("(?<=A)A")]
            private static partial Regex rAA();
            [GeneratedRegex("(?<=A)v")]
            private static partial Regex rAD();
            [GeneratedRegex("(?<=A)<")]
            private static partial Regex rAL();
            [GeneratedRegex("(?<=A)>")]
            private static partial Regex rAR();
            [GeneratedRegex("(?<=A)\\^")]
            private static partial Regex rAU();
            [GeneratedRegex("(?<=v)A")]
            private static partial Regex rDA();
            [GeneratedRegex("(?<=v)v")]
            private static partial Regex rDD();
            [GeneratedRegex("(?<=v)<")]
            private static partial Regex rDL();
            [GeneratedRegex("(?<=v)>")]
            private static partial Regex rDR();
            [GeneratedRegex("(?<=v)\\^")]
            private static partial Regex rDU();
            [GeneratedRegex("(?<=<)A")]
            private static partial Regex rLA();
            [GeneratedRegex("(?<=<)v")]
            private static partial Regex rLD();
            [GeneratedRegex("(?<=<)<")]
            private static partial Regex rLL();
            [GeneratedRegex("(?<=<)>")]
            private static partial Regex rLR();
            [GeneratedRegex("(?<=<)\\^")]
            private static partial Regex rLU();
            [GeneratedRegex("(?<=>)A")]
            private static partial Regex rRA();
            [GeneratedRegex("(?<=>)v")]
            private static partial Regex rRD();
            [GeneratedRegex("(?<=>)<")]
            private static partial Regex rRL();
            [GeneratedRegex("(?<=>)>")]
            private static partial Regex rRR();
            [GeneratedRegex("(?<=>)\\^")]
            private static partial Regex rRU();
            [GeneratedRegex("(?<=\\^)A")]
            private static partial Regex rUA();
            [GeneratedRegex("(?<=\\^)v")]
            private static partial Regex rUD();
            [GeneratedRegex("(?<=\\^)<")]
            private static partial Regex rUL();
            [GeneratedRegex("(?<=\\^)>")]
            private static partial Regex rUR();
            [GeneratedRegex("(?<=\\^)\\^")]
            private static partial Regex rUU();
        }
    }
}
