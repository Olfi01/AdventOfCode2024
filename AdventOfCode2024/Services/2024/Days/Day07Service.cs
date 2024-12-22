using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day07Service(IInputService inputService) : SingleDayService(inputService, 2024, 7)
    {
        public async Task<long> Part1()
        {
            var input = await InputService.GetInputAsList(year, day);
            long sum = 0;
            foreach (var line in input)
            {
                string[] split = line.Split(": ");
                long result = long.Parse(split[0]);
                long[] operands = split[1].Split(' ').Select(long.Parse).ToArray();
                if (CanProduceResult(result, operands, [])) sum += result;
            }
            return sum;
        }

        public async Task<long> Part2()
        {
            var input = await InputService.GetInputAsList(year, day);
            long sum = 0;
            foreach (var line in input)
            {
                string[] split = line.Split(": ");
                long result = long.Parse(split[0]);
                long[] operands = split[1].Split(' ').Select(long.Parse).ToArray();
                if (CanProduceResult(result, operands, [], true)) sum += result;
            }
            return sum;
        }

        private static bool CanProduceResult(long result, long[] operands, List<Operator> operators, bool allowConcat = false)
        {
            if (operators.Count == operands.Length - 1)
            {
                long subtotal = operands[0];
                for (int i = 0; i < operators.Count; i++)
                {
                    subtotal = operators[i] switch
                    {
                        Operator.Add => subtotal + operands[i + 1],
                        Operator.Multiply => subtotal * operands[i + 1],
                        Operator.Concat => TryConcat(subtotal, operands[i + 1]),
                        _ => throw new Exception()
                    };
                    if (subtotal > result) return false;
                }
                return subtotal == result;
            }
            else return CanProduceResult(result, operands, [.. operators, Operator.Add], allowConcat) 
                    || CanProduceResult(result, operands, [.. operators, Operator.Multiply], allowConcat)
                    || (allowConcat && CanProduceResult(result, operands, [.. operators, Operator.Concat], allowConcat));
        }

        private static long TryConcat(long left, long right)
        {
            string concatenated = left + "" + right;
            try
            {
                return long.Parse(concatenated);
            }
            catch (OverflowException)
            {
                return long.MaxValue;
            }
        }

        private enum Operator
        {
            Add,
            Multiply,
            Concat
        }
    }
}
