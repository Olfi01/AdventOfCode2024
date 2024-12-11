using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day11Service(IInputService inputService) : SingleDayService(inputService, 2024, 11)
    {
        public async Task<long> Part1()
        {
            var input = await InputService.GetInputAsLongList(year, day, ' ');
            long[] stones = input.ToArray();
            for (int i = 0; i < 25; i++)
            {
                stones = stones.SelectMany(Blink).ToArray();
            }
            return stones.Length;
        }

        private readonly Dictionary<long, long[]> blinkCache = [];

        private long[] Blink(long stone)
        {
            if (blinkCache.TryGetValue(stone, out long[]? value)) return value;
            if (stone == 0)
            {
                blinkCache[stone] = [1];
                return [1];
            }
            string str = stone.ToString();
            if (str.Length % 2 == 0)
            {
                long[] result = [long.Parse(str[..(str.Length / 2)]), long.Parse(str[(str.Length / 2)..])];
                blinkCache[stone] = result;
                return result;
            }
            else 
            {
                long[] result = [stone * 2024];
                blinkCache[stone] = result;
                return [stone * 2024];
            };
        }

        public async Task<long> Part2()
        {
            var input = await InputService.GetInputAsLongList(year, day, ' ');
            long[] stones = input.ToArray();
            long count = BlinkRecursively(stones, 75);
            return count;
        }

        private readonly Dictionary<(long stone, int times), long> recursiveCache = [];

        private long BlinkRecursively(long[] stones, int times)
        {
            if (times == 1) return stones.SelectMany(Blink).LongCount();
            else
            {
                long sum = 0;
                foreach (long stone in stones)
                {
                    if (recursiveCache.TryGetValue((stone, times), out long value))
                    {
                        sum += value;
                    }
                    else
                    {
                        long length = BlinkRecursively(Blink(stone), times - 1);
                        sum += length;
                        recursiveCache[(stone, times)] = length;
                    }
                }
                return sum;
            }
        }
    }
}
