using AdventOfCode2024.Services;

namespace AdventOfCode2024
{
    internal class Program
    {
        private static InputService inputService;

        static void Main(string[] args)
        {
            inputService = new InputService(args[0]);
        }
    }
}
