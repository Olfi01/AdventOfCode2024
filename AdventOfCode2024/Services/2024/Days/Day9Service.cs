using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day9Service(IInputService inputService) : SingleDayService(inputService, 2024, 9)
    {
        public async Task<long> Part1()
        {
            var input = await InputService.GetInputAsString(year, day);
            int[] numbers = input.Where(char.IsDigit).Select(c => int.Parse(c + "")).ToArray();
            List<long> memory = [];
            for (int i = 0; i < numbers.Length; i+= 2)
            {
                int fileId = i / 2;
                for (int j = 0; j < numbers[i]; j++)
                {
                    memory.Add(fileId);
                }
                if (i + 1 < numbers.Length)
                {
                    for (int j = 0; j < numbers[i + 1]; j++)
                    {
                        memory.Add(-1);
                    }
                }
            }

            for (int i = memory.Count - 1; i >= 0; i--)
            {
                if (memory[i] != -1)
                {
                    int newIndex = memory.IndexOf(-1);
                    if (newIndex >= i) break;
                    memory[newIndex] = memory[i];
                    memory[i] = -1;
                }
            }

            long sum = 0;
            for (int i = 0; i < memory.Count; i++)
            {
                if (memory[i] != -1) sum += i * memory[i];
            }
            return sum;
        }

        public async Task<long> Part2()
        {
            var input = await InputService.GetInputAsString(year, day);
            int[] numbers = input.Where(char.IsDigit).Select(c => int.Parse(c + "")).ToArray();
            Dictionary<int, (int size, int index)> files = [];
            List<long> memory = [];
            for (int i = 0; i < numbers.Length; i += 2)
            {
                int fileId = i / 2;
                files.Add(fileId, (numbers[i], memory.Count));

                for (int j = 0; j < numbers[i]; j++)
                {
                    memory.Add(fileId);
                }
                if (i + 1 < numbers.Length)
                {
                    for (int j = 0; j < numbers[i + 1]; j++)
                    {
                        memory.Add(-1);
                    }
                }
            }

            foreach (var kvp in files.OrderByDescending(f => f.Key))
            {
                var (size, index) = kvp.Value;
                int consecutiveEmpty = 0;
                for (int i = 0; i < index; i++)
                {
                    if (memory[i] == -1) consecutiveEmpty++;
                    else consecutiveEmpty = 0;
                    if (consecutiveEmpty >= size)
                    {
                        for (int j = i - consecutiveEmpty + 1; j <= i; j++)
                        {
                            memory[j] = kvp.Key;
                        }
                        for (int j = index; j < index + size; j++)
                        {
                            memory[j] = -1;
                        }
                        break;
                    }
                }
            }

            long sum = 0;
            for (int i = 0; i < memory.Count; i++)
            {
                if (memory[i] != -1) sum += i * memory[i];
            }
            return sum;
        }
    }
}
