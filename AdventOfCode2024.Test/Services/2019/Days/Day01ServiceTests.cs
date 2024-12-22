using Xunit;
using AdventOfCode2024.Services._2019.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days.Tests
{
    public class Day01ServiceTests
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void CalculateFuelForMassTest(int mass, int fuel)
        {
            Assert.Equal(fuel, Day01Service.CalculateFuelForMass(mass));
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void CalculateFuelForMassRecursivelyTest(int mass, int fuel)
        {
            Assert.Equal(fuel, Day01Service.CalculateFuelForMassRecursively(mass));
        }
    }
}