using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day01Service(IInputService inputService) : SingleDayService(inputService, 2019, 1)
    {
        public async Task<int> Part1()
        {
            var modules = await InputService.GetInputAsIntList(year, day);
            return modules.Sum(CalculateFuelForMass);
        }

        public static int CalculateFuelForMass(int mass)
        {
            return Math.Max(0, mass / 3 - 2);
        }

        public async Task<int> Part2()
        {
            var modules = await InputService.GetInputAsIntList(year, day);
            return modules.Sum(CalculateFuelForMassRecursively);
        }

        public static int CalculateFuelForMassRecursively(int mass)
        {
            int fuel = CalculateFuelForMass(mass);
            if (fuel <= 0) return fuel;
            else return fuel + CalculateFuelForMassRecursively(fuel);
        }
    }
}
