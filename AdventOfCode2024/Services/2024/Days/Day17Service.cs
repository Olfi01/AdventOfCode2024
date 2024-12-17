using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public partial class Day17Service(IInputService inputService) : SingleDayService(inputService, 2024, 17)
    {
        [GeneratedRegex(@"Register A: (?<regA>\d+)\nRegister B: (?<regB>\d+)\nRegister C: (?<regC>\d+)\n\nProgram: (?<program>[\d,]+)")]
        private static partial Regex InputRegex();

        public async Task<string> Part1()
        {
            var input = await InputService.GetInputAsString(year, day);
            var match = InputRegex().Match(input);
            var computer = new ThreeBitComputer(int.Parse(match.Groups["regA"].Value), int.Parse(match.Groups["regB"].Value), int.Parse(match.Groups["regC"].Value), match.Groups["program"].Value.Split(',').Select(int.Parse).ToArray());
            computer.Run();
            return string.Join(',', computer.Outputs);
        }

        public async Task<long> Part2()
        {
            var input = await InputService.GetInputAsString(year, day);
            var match = InputRegex().Match(input);
            var program = match.Groups["program"].Value.Split(',').Select(byte.Parse).ToArray();
            var result = FindCorrectCombination(program.Length - 1, program, [0b000, 0b000, 0b000]) ?? throw new Exception();
            long sum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                sum += (long)result[i] << (3 * i);
            }
            return sum;
        }

        public static byte[]? FindCorrectCombination(int i, byte[] program, byte[] previousParts)
        {
            if (i < 0) return [];
            byte expectedOut = program[i];
            var p3 = previousParts[0];
            var pp = previousParts[1];
            var p = previousParts[2];
            if ((p3 & 0b001) == (expectedOut >> 2) && ((pp >> 1) == (expectedOut & 0b011)) && i != program.Length - 1)
            {
                var result = FindCorrectCombination(i - 1, program, [pp, p, 0b000]);
                if (result != null) return [.. result, 0b000];
            }
            if ((pp ^ 0b001) == expectedOut)
            {
                var result = FindCorrectCombination(i - 1, program, [pp, p, 0b001]);
                if (result != null) return [.. result, 0b001];
            }
            if (((pp & 0b011) ^ 0b001) == (expectedOut >> 1) && (p >> 2) == (expectedOut & 0b001))
            {
                var result = FindCorrectCombination(i - 1, program, [pp, p, 0b010]);
                if (result != null) return [.. result, 0b010];
            }
            if ((pp & 0b001) == (expectedOut >> 2) && ((p >> 1) ^ 0b011) == (expectedOut & 0b011))
            {
                var result = FindCorrectCombination(i - 1, program, [pp, p, 0b011]);
                if (result != null) return [.. result, 0b011];
            }
            if ((p ^ 0b100) == expectedOut)
            {
                var result = FindCorrectCombination(i - 1, program, [pp, p, 0b100]);
                if (result != null) return [.. result, 0b100];
            }
            if (((p & 0b011) ^ 0b010) == (expectedOut >> 1) && (expectedOut & 0b001) == 0b000)
            {
                var result = FindCorrectCombination(i - 1, program, [pp, p, 0b101]);
                if (result != null) return [.. result, 0b101];
            }
            if (((p & 0b001) ^ 0b001) == (expectedOut >> 2) && (expectedOut & 0b011) == 0b001)
            {
                var result = FindCorrectCombination(i - 1, program, [pp, p, 0b110]);
                if (result != null) return [.. result, 0b110];
            }
            if (expectedOut == 0b000)
            {
                var result = FindCorrectCombination(i - 1, program, [pp, p, 0b111]);
                if (result != null) return [.. result, 0b111];
            }
            return null;
        }

        private class ThreeBitComputer(int registerA, int registerB, int registerC, int[] program)
        {
            private readonly int[] program = program;
            private int registerA = registerA;
            private int registerB = registerB;
            private int registerC = registerC;
            public List<int> Outputs { get; } = [];

            private int instructionPointer = 0;

            public HashSet<(int a, int b, int c, int pointer)> States { get; } = [];

            public void Run()
            {
                while (true)
                {
                    if (instructionPointer >= program.Length) return;
                    var operation = program[instructionPointer];
                    var operand = program[instructionPointer + 1];
                    switch (operation)
                    {
                        case 0:
                            Adv(operand);
                            break;
                        case 1:
                            Bxl(operand); 
                            break;
                        case 2:
                            Bst(operand);
                            break;
                        case 3:
                            Jnz(operand);
                            break;
                        case 4:
                            Bxc(operand);
                            break;
                        case 5:
                            Out(operand);
                            break;
                        case 6:
                            Bdv(operand);
                            break;
                        case 7:
                            Cdv(operand);
                            break;
                    }
                    instructionPointer += 2;
                }
            }

            public bool ProducesOutput(int[] expectedOutput, HashSet<(int a, int b, int c, int pointer)> failedStates)
            {
                var state = (registerA, registerB, registerC, instructionPointer);
                if (failedStates.Contains(state)) return false;
                States.Add(state);
                while (true)
                {
                    if (instructionPointer >= program.Length) return expectedOutput.SequenceEqual(Outputs);
                    var operation = program[instructionPointer];
                    var operand = program[instructionPointer + 1];
                    switch (operation)
                    {
                        case 0:
                            Adv(operand);
                            break;
                        case 1:
                            Bxl(operand);
                            break;
                        case 2:
                            Bst(operand);
                            break;
                        case 3:
                            Jnz(operand);
                            break;
                        case 4:
                            Bxc(operand);
                            break;
                        case 5:
                            Out(operand);
                            if (expectedOutput[Outputs.Count - 1] != Outputs.Last()) return false;
                            break;
                        case 6:
                            Bdv(operand);
                            break;
                        case 7:
                            Cdv(operand);
                            break;
                    }
                    instructionPointer += 2;
                }
            }

            private int GetComboOperand(int bitcode)
            {
                return bitcode switch
                {
                    0 => 0,
                    1 => 1,
                    2 => 2,
                    3 => 3,
                    4 => registerA,
                    5 => registerB,
                    6 => registerC,
                    _ => throw new Exception()
                };
            }

            private void Adv(int operand)
            {
                registerA >>= GetComboOperand(operand);
            }

            private void Bxl(int operand)
            {
                registerB ^= operand;
            }

            private void Bst(int operand)
            {
                registerB = GetComboOperand(operand) & 0b111;
            }

            private void Jnz(int operand)
            {
                if (registerA != 0)
                {
                    instructionPointer = operand - 2;
                }
            }

            private void Bxc(int operand)
            {
                registerB ^= registerC;
            }

            private void Out(int operand)
            {
                Outputs.Add(GetComboOperand(operand) & 0b111);
            }

            private void Bdv(int operand)
            {
                registerB = registerA >> GetComboOperand(operand);
            }

            private void Cdv(int operand)
            {
                registerC = registerA >> GetComboOperand(operand);
            }
        }
    }
}
