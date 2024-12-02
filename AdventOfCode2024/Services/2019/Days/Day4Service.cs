using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day4Service(IInputService inputService) : SingleDayService(inputService, 2019, 4)
    {
        public async Task<int> Part1()
        {
            var split = await InputService.GetInputAsList(year, day, '-');
            (int min, int max) = (int.Parse(split.First()), int.Parse(split.Last()));
            return Enumerable.Range(min, max - min + 1).Count(MeetsPart1Criteria);
        }

        public static bool MeetsPart1Criteria(int number)
        {
            string str = number.ToString();
            if (str.Length != 6) return false;
            int lastDigit = -1;
            bool twoSame = false;
            foreach (char c in str)
            {
                int digit = c - '0';
                if (digit < lastDigit) return false;
                if (digit == lastDigit) twoSame = true;
                lastDigit = digit;
            }
            return twoSame;
        }

        public async Task<int> Part2()
        {
            var split = await InputService.GetInputAsList(year, day, '-');
            (int min, int max) = (int.Parse(split.First()), int.Parse(split.Last()));
            return Enumerable.Range(min, max - min + 1).Count(MeetsPart2Criteria);
        }

        public static bool MeetsPart2Criteria(int number)
        {
            string str = number.ToString();
            if (str.Length != 6) return false;
            int lastDigit = -1;
            bool exactlyTwo = false;
            for (int i = 0; i < 6; i++)
            {
                char c = str[i];
                int digit = c - '0';
                if (digit < lastDigit) return false;
                if (i > 0 && str[i] == str[i - 1] && (i < 2 || str[i - 2] != str[i]) && (i > 4 || str[i] != str[i + 1])) exactlyTwo = true;
                lastDigit = digit;
            }
            return exactlyTwo;
        }
    }
}
