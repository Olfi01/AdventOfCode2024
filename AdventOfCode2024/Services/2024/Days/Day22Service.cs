using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day22Service(IInputService inputService) : SingleDayService(inputService, 2024, 22)
    {
        private const long prune = 0b111111111111111111111111;

        public async Task<long> Part1()
        {
            var numbers = await InputService.GetInputAsLongList(year, day);
            long sum = 0;
            foreach (var number in numbers)
            {
                var num = number;
                for (int i = 0; i < 2000; i++)
                {
                    num = NextSecretNumber(num);
                }
                sum += num;
            }
            return sum;
        }

        public async Task<long> Part2()
        {
            var numbers = await InputService.GetInputAsLongList(year, day);

            List<Dictionary<(int c1, int c2, int c3, int c4), byte>> buyers = [];
            HashSet<(int c1, int c2, int c3, int c4)> keys = [];

            foreach (var number in numbers)
            {
                Dictionary<(int c1, int c2, int c3, int c4), byte> prices = [];
                var num = number;
                var newNum = NextSecretNumber(num);
                var c1 = LastDigitOf(newNum) - LastDigitOf(num);
                num = newNum;
                newNum = NextSecretNumber(num);
                var c2 = LastDigitOf(newNum) - LastDigitOf(num);
                num = newNum;
                newNum = NextSecretNumber(num);
                var c3 = LastDigitOf(newNum) - LastDigitOf(num);
                num = newNum;
                for (int i = 3; i < 2000; i++)
                {
                    newNum = NextSecretNumber(num);
                    var c4 = LastDigitOf(newNum) - LastDigitOf(num);
                    prices.TryAdd((c1, c2, c3, c4), LastDigitOf(newNum));
                    keys.Add((c1, c2, c3, c4));
                    c1 = c2;
                    c2 = c3;
                    c3 = c4;
                    num = newNum;
                }
                buyers.Add(prices);
            }

            long bestSum = 0;

            foreach (var key in keys)
            {
                long sum = buyers.Sum(b => (long)b.GetValueOrDefault(key, (byte)0));
                if (sum > bestSum) bestSum = sum;
            }

            return bestSum;
        }

        public static long NextSecretNumber(long secretNumber)
        {
            secretNumber ^= secretNumber << 6;
            secretNumber &= prune;
            secretNumber ^= secretNumber >> 5;
            secretNumber &= prune;
            secretNumber ^= secretNumber << 11;
            secretNumber &= prune;
            return secretNumber;
        }

        private static byte LastDigitOf(long num)
        {
            return (byte)(num % 10);
        }
    }
}
