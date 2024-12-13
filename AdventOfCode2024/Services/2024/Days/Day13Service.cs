using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public partial class Day13Service(IInputService inputService) : SingleDayService(inputService, 2024, 13)
    {
        private static readonly Regex MachineInput = new(@"Button A: X\+(?<dxA>\d+), Y\+(?<dyA>\d+)\nButton B: X\+(?<dxB>\d+), Y\+(?<dyB>\d+)\nPrize: X=(?<priceX>\d+), Y=(?<priceY>\d+)");

        private record ClawMachine(long DxA, long DyA, long DxB, long DyB, long PriceX, long PriceY) { }

        public async Task<long> Part1()
        {
            var machines = (await InputService.GetInputAsList(year, day, "\n\n")).Select(ReadMachineInput);
            long sum = 0;
            foreach (var machine in machines)
            {
                /*
                 * This is my old, ugly-ass code that I used for part 1. I am ashamed of it but want to preserve it in the spirit of transparency.
                long? lowestCost = null;
                for (long nA = 0; nA <= 100; nA++)
                {
                    if (nA * machine.DxA > machine.PriceX || nA * machine.DyA > machine.PriceY) break;
                    for (long nB = 0; nB <= 100; nB++)
                    {
                        long x = nA * machine.DxA + nB * machine.DxB;
                        long y = nA * machine.DyA + nB * machine.DyB;
                        if (x > machine.PriceX ||  y > machine.PriceY)
                        {
                            break;
                        }
                        else if (x == machine.PriceX && y == machine.PriceY)
                        {
                            long cost = 3 * nA + nB;
                            if (!lowestCost.HasValue || lowestCost.Value > cost)
                            {
                                lowestCost = cost;
                            }
                            break;
                        }
                    }
                }
                if (lowestCost.HasValue) sum += lowestCost.Value;
                */

                // This is what I should have done instead.
                long[,] matrix = new long[2, 3]
                {
                    { machine.DxA, machine.DxB, machine.PriceX },
                    { machine.DyA, machine.DyB, machine.PriceY }
                };
                if (!GaussianSolve(matrix)) continue;
                sum += matrix[0, 2] * 3 + matrix[1, 2];
            }
            return sum;
        }

        private static ClawMachine ReadMachineInput(string input)
        {
            var match = MachineInput.Match(input);
            return new ClawMachine(
                long.Parse(match.Groups["dxA"].Value), 
                long.Parse(match.Groups["dyA"].Value), 
                long.Parse(match.Groups["dxB"].Value), 
                long.Parse(match.Groups["dyB"].Value), 
                long.Parse(match.Groups["priceX"].Value), 
                long.Parse(match.Groups["priceY"].Value)
            );
        }

        public async Task<long> Part2()
        {
            var machines = (await InputService.GetInputAsList(year, day, "\n\n")).Select(ReadMachineInputPart2);
            long sum = 0;
            foreach (var machine in machines)
            {
                long[,] matrix = new long[2, 3]
                {
                    { machine.DxA, machine.DxB, machine.PriceX },
                    { machine.DyA, machine.DyB, machine.PriceY }
                };
                if (!GaussianSolve(matrix)) continue;
                sum += matrix[0, 2] * 3 + matrix[1, 2];
            }
            return sum;
        }

        public static bool GaussianSolve(long[,] matrix)
        {
            if (matrix.GetLength(0) != 2 || matrix.GetLength(1) != 3) throw new Exception();
            long lcm = Lcm(matrix[0, 0], matrix[1, 0]);
            long f0 = lcm / matrix[0, 0];
            long f1 = lcm / matrix[1, 0];
            for (int i = 0; i < 3; i++)
            {
                matrix[0, i] *= f0;
                matrix[1, i] *= f1;
                matrix[1, i] -= matrix[0, i];
            }

            if (matrix[1, 2] % matrix[1, 1] != 0) return false;

            lcm = Lcm(matrix[0, 1], matrix[1, 1]);
            f0 = lcm / matrix[0, 1];
            f1 = lcm / matrix[1, 1];
            for (int i = 0; i < 3; i++)
            {
                matrix[0, i] *= f0;
                matrix[1, i] *= f1;
                matrix[0, i] -= matrix[1, i];
            }

            if (matrix[0, 2] % matrix[0, 0] != 0) return false;

            for (int i = 0; i < 2; i++)
            {
                matrix[i, 2] /= matrix[i, i];
                matrix[i, i] = 1;
            }
            return true;
        }

        // stolen from https://stackoverflow.com/questions/13569810/least-common-multiple
        private static long Gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long Lcm(long a, long b)
        {
            return (a / Gcf(a, b)) * b;
        }

        private static ClawMachine ReadMachineInputPart2(string input)
        {
            var match = MachineInput.Match(input);
            return new ClawMachine(
                long.Parse(match.Groups["dxA"].Value),
                long.Parse(match.Groups["dyA"].Value),
                long.Parse(match.Groups["dxB"].Value),
                long.Parse(match.Groups["dyB"].Value),
                long.Parse(match.Groups["priceX"].Value) + 10000000000000,
                long.Parse(match.Groups["priceY"].Value) + 10000000000000
            );
        }
    }
}
