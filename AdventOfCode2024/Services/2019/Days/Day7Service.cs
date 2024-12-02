using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day7Service(IInputService inputService) : SingleDayService(inputService, 2019, 7)
    {
        public async Task<int> Part1()
        {
            var program = await InputService.GetInputAsIntList(year, day, ',');
            IntcodeComputerService computer = new();
            int highestOut = 0;
            IEnumerable<IEnumerable<int>> permutations = ComputePermutations(Enumerable.Range(0, 5));
            foreach (var perm in permutations)
            {
                int input = 0;
                foreach (var phase in perm)
                {
                    await computer.InputStream.Writer.WriteAsync(phase);
                    await computer.InputStream.Writer.WriteAsync(input);
                    await computer.ExecuteProgram(program);
                    input = await computer.OutputStream.Reader.ReadAsync();
                }
                if (input > highestOut) highestOut = input;
            }
            return highestOut;
        }

        public static IEnumerable<IEnumerable<int>> ComputePermutations(IEnumerable<int> enumerable)
        {
            if (enumerable.Count() < 2)
            {
                yield return enumerable;
                yield break;
            }
            for (int i = 0; i < enumerable.Count(); i++)
            {
                foreach (var perm in ComputePermutations([.. enumerable.Take(i), .. enumerable.Skip(i + 1)]))
                {
                    yield return [enumerable.ElementAt(i), .. perm];
                }
            }
        }

        public async Task<int> Part2()
        {
            var program = await InputService.GetInputAsIntList(year, day, ',');
            int highestOut = 0;
            IEnumerable<IEnumerable<int>> permutations = ComputePermutations(Enumerable.Range(5, 5));
            foreach (var perm in permutations)
            {
                int input = 0;
                IntcodeComputerService[] amps = Enumerable.Range(0, perm.Count()).Select(x => new IntcodeComputerService()).ToArray();
                Task[] tasks = new Task[amps.Length];
                for (int i = 0; i < amps.Length; i++)
                {
                    await amps[i].InputStream.Writer.WriteAsync(perm.ElementAt(i));
                    tasks[i] = amps[i].ExecuteProgram(program);
                }

                int currentAmp = 0;
                while (!tasks[currentAmp].IsCompleted)
                {
                    await amps[currentAmp].InputStream.Writer.WriteAsync(input);
                    input = await amps[currentAmp].OutputStream.Reader.ReadAsync();
                    currentAmp = (currentAmp + 1) % 5;
                }

                if (input > highestOut) highestOut = input;
            }
            return highestOut;
        }
    }
}
