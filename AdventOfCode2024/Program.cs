using AdventOfCode2024.Services;
using AdventOfCode2024.Services._2024.Days;

namespace AdventOfCode2024
{
    internal class Program
    {
        private static InputService? inputService;

        static async Task Main(string[] args)
        {
            inputService = new InputService(args[0]);

            Day2Service day = new(inputService);
            int result = await day.Part2();
            Console.WriteLine(result);
        }
    }
}
