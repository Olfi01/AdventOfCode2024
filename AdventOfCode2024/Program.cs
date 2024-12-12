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

            Day12Service day = new(inputService);
            DateTime before = DateTime.Now;
            var result = await day.Part2();
            DateTime after = DateTime.Now;
            Console.WriteLine(result);
            Console.WriteLine($"Finished in {after - before}");
        }
    }
}
